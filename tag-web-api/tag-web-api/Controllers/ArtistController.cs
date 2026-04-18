// <copyright file="ArtistController.cs" company="Twisted Artists Guild">
// Copyright © Twisted Artists Guild. All rights reserved
// </copyright>

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TAGWEBAPI.Data;
using TAGWEBAPI.Models;
using System.IO;
using System.Text.Json;
using System.Globalization;

namespace TAGWEBAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ArtistController : ControllerBase
{
    private readonly TAGDBContext _context;
    private readonly ILogger<ArtistController> _logger;
    private static readonly Regex ValidPathRegex = new(@"^[a-z0-9](?:[a-z0-9\-]*[a-z0-9])?$", RegexOptions.Compiled);

    public ArtistController(TAGDBContext context, ILogger<ArtistController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet(Name = "GetArtists")]
    public async Task<ActionResult<IEnumerable<Artist>>> Get()
    {
        return await _context.Set<Artist>()
            .Include(a => a.ProfilePic)
            .ToListAsync()
            .ConfigureAwait(false);
    }

    [HttpGet("byID/{id}")]
    public async Task<ActionResult<Artist>> GetByID(int id)
    {
        if (_context.Artists == null)
        {
            return NotFound();
        }
        var artist = await _context.Artists.FindAsync(id);

        if (artist == null)
        {
            return NotFound();
        }

        return artist;
    }

    [HttpGet("{slug}")]
    public async Task<ActionResult<Artist>> Get(string slug)
    {
        var artist = await _context.Set<Artist>()
            .Include(a => a.ProfilePic)
            .Include(a => a.CoverPic)
            .FirstOrDefaultAsync(a => a.Path.ToLower() == slug.ToLower())
            .ConfigureAwait(false);
        if (artist == null)
        {
            return this.NotFound();
        }

        return artist;
    }

    [HttpGet("{slug}/profile")]
    public async Task<ActionResult<object>> GetArtistProfile(string slug)
    {
        if (_context.Artists == null)
        {
            return NotFound();
        }

        // Get the artist with all the required navigation properties included
        var artist = await _context.Artists
            .Include(a => a.ProfilePic)
            .Include(a => a.CoverPic)
            .Include(a => a.Contacts)
                .ThenInclude(c => c.Address)
            .Include(a => a.Contacts)
                .ThenInclude(c => c.PhoneContact)
            .Include(a => a.Contacts)
                .ThenInclude(c => c.ExternalLink)
            .AsSplitQuery() // This optimizes the query when including multiple collections
            .FirstOrDefaultAsync(a => a.Path.ToLower() == slug.ToLower())
            .ConfigureAwait(false);

        if (artist == null)
        {
            _logger.LogWarning("Artist not found for slug: {Slug}", slug);
            return NotFound();
        }

        // Get all listings for this artist with required includes
        var listings = await _context.Set<Listing>()
            .Where(l => l.ArtistID == artist.ArtistID)
            .Include(l => l.ArtCategory)
            .Include(l => l.ProfilePic)
            .ToListAsync()
            .ConfigureAwait(false);

        if (!listings.Any())
        {
            _logger.LogInformation("No listings found for artist ID: {ArtistID}", artist.ArtistID);
        }

        // Filter the contacts to only get public ones
        var publicContacts = artist.Contacts?.Where(c => c.MakePublic).ToList();

        // Process contacts into appropriate structures for the API response
        var addresses = publicContacts?
            .Where(c => c.Address != null)
            .Select(c => new {
                c.Label,
                c.Address.AddressLine1,
                c.Address.AddressLine2,
                c.Address.City,
                c.Address.State,
                c.Address.ZipCode,
                c.Address.Country,
                c.Address.OperationHours
            }).ToList();

        var phoneContacts = publicContacts?
            .Where(c => c.PhoneContact != null)
            .Select(c => new {
                c.Label,
                Number = c.PhoneContact.PhoneNumber,
                Description = c.PhoneContact.Description
            }).ToList();

        var externalLinks = publicContacts?
            .Where(c => c.ExternalLink != null)
            .Select(c => new {
                c.Label,
                URL = c.ExternalLink.URL
            }).ToList();

        return new
        {
            artist = new { 
                artist.ArtistID,
                artist.Title,
                artist.Path,
                artist.Byline,
                artist.Statement,
                artist.Biography,
                artist.SEOTags,
                artist.Applied,
                artist.Since,
            },
            profilePic = artist.ProfilePic,
            coverPic = artist.CoverPic,
            listings,
            contactInfo = new
            {
                addresses,
                phones = phoneContacts,
                links = externalLinks
            }
        };
    }

    [HttpPost]
    public async Task<ActionResult<Artist>> Create(Artist artist)
    {
        if (_context.Artists == null)
        {
            return Problem("Entity set 'TAGDBContext.Artists' is null.");
        }

        // Ensure the path is valid and unique
        try
        {
            if (!ValidPathRegex.IsMatch(artist.Path))
            {
                artist.Path = await GenerateUniquePathAsync(artist.Title);
            }
            else if (!await IsPathUniqueAsync(artist.Path))
            {
                return BadRequest("Path is not unique.");
            }
        }
        catch (Exception ex)
        {
            return BadRequest($"Path validation error: {ex.Message}");
        }

        _context.Artists.Add(artist);
        await _context.SaveChangesAsync();

        var artistPermissions = new ArtistPermissions
        {
            ArtistID = artist.ArtistID,
            OwnerRole = false,
            POS_Authorized = false,
        };

        _context.Set<ArtistPermissions>().Add(artistPermissions);
        await _context.SaveChangesAsync().ConfigureAwait(false);

        return CreatedAtAction(nameof(Get), new { id = artist.ArtistID }, artist);
    }

    [HttpPost("reserve-slug")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<ArtistSlugReservationResponse>> ReserveArtistSlug([FromBody] ArtistSlugReservationRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Validate the slug format and availability
        if (!ValidPathRegex.IsMatch(request.Slug))
        {
            return BadRequest("Invalid slug format.");
        }

        if (!await IsPathUniqueAsync(request.Slug))
        {
            return Conflict("Slug is already in use.");
        }

        // Create a new artist with minimal required fields
        var artist = new Artist
        {
            Path = request.Slug,
            Title = request.Title ?? request.Slug, // Default to slug if title isn't provided
            Since = DateTime.UtcNow,
            Applied = DateTime.UtcNow,
            // Other fields will be filled in later during the full update
        };

        try
        {
            _context.Artists.Add(artist);
            await _context.SaveChangesAsync();

            // Return the reserved artist object with its ID
            var response = new ArtistSlugReservationResponse
            {
                ArtistID = artist.ArtistID,
                Path = artist.Path,
                Title = artist.Title,
            };

            return CreatedAtAction(nameof(Get), new { id = artist.ArtistID }, response);
        }
        catch (DbUpdateException ex)
        {
            // Log the exception
            _logger.LogError(ex, "Failed to reserve artist slug: {Slug}", request.Slug);
            
            // Handle unique constraint violations or other DB errors
            return StatusCode(StatusCodes.Status500InternalServerError, 
                new { message = "Failed to reserve slug", error = ex.InnerException?.Message ?? ex.Message });
        }
    }

    [HttpPatch("{id}/update-slug")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<ArtistSlugReservationResponse>> UpdateArtistSlug(int id, [FromBody] ArtistSlugUpdateRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var artist = await _context.Artists.FindAsync(id);
        if (artist == null)
        {
            return NotFound(new { message = $"Artist with ID {id} not found" });
        }

        // If no change, return success immediately
        if (artist.Path == request.Slug)
        {
            return Ok(new ArtistSlugReservationResponse
            {
                ArtistID = artist.ArtistID,
                Path = artist.Path,
                Title = artist.Title,
            });
        }

        // Validate the slug format and availability
        if (!ValidPathRegex.IsMatch(request.Slug))
        {
            return BadRequest("Invalid slug format.");
        }

        if (!await IsPathUniqueAsync(request.Slug, id))
        {
            return Conflict("Slug is already in use.");
        }

        // Update the slug
        artist.Path = request.Slug;

        try
        {
            await _context.SaveChangesAsync();

            // Return the updated artist
            var response = new ArtistSlugReservationResponse
            {
                ArtistID = artist.ArtistID,
                Path = artist.Path,
                Title = artist.Title,
            };

            return Ok(response);
        }
        catch (DbUpdateException ex)
        {
            // Log the exception
            _logger.LogError(ex, "Failed to update artist slug: {Slug}", request.Slug);
            
            // Handle unique constraint violations or other DB errors
            return StatusCode(StatusCodes.Status500InternalServerError, 
                new { message = "Failed to update slug", error = ex.InnerException?.Message ?? ex.Message });
        }
    }

    [HttpPut("byID/{id}")]
    public async Task<IActionResult> UpdateByID(int id)
    {
        if (_context.Artists == null)
        {
            return NotFound();
        }

        var existingArtist = await _context.Artists.FindAsync(id).ConfigureAwait(false);
        if (existingArtist == null)
        {
            return NotFound();
        }

        // Read raw body to avoid model binding and nested ArtistPermissions validation
        using var reader = new StreamReader(Request.Body);
        var bodyText = await reader.ReadToEndAsync();
        if (string.IsNullOrWhiteSpace(bodyText))
        {
            return BadRequest("Missing artist payload.");
        }

        JsonDocument doc;
        try
        {
            doc = JsonDocument.Parse(bodyText);
        }
        catch (JsonException)
        {
            return BadRequest("Invalid JSON payload.");
        }

        var root = doc.RootElement;
        var props = new Dictionary<string, JsonElement>(StringComparer.OrdinalIgnoreCase);
        foreach (var prop in root.EnumerateObject()) props[prop.Name] = prop.Value;

        static string GetString(JsonElement el) => el.ValueKind == JsonValueKind.Null ? null : el.GetString();

        if (props.TryGetValue("Applied", out var p))
        {
            DateTime parsedApplied;
            if (p.ValueKind == JsonValueKind.String)
            {
                parsedApplied = DateTime.Parse(p.GetString(), CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal);
            }
            else
            {
                parsedApplied = p.GetDateTime();
            }

            existingArtist.Applied = DateTime.SpecifyKind(parsedApplied.ToUniversalTime(), DateTimeKind.Utc);
        }
        if (props.TryGetValue("Biography", out p)) existingArtist.Biography = GetString(p) ?? existingArtist.Biography;
        if (props.TryGetValue("Byline", out p)) existingArtist.Byline = GetString(p) ?? existingArtist.Byline;
        if (props.TryGetValue("Path", out p))
        {
            var newPath = GetString(p);
            if (newPath != null && newPath != existingArtist.Path)
            {
                if (!ValidPathRegex.IsMatch(newPath))
                {
                    return BadRequest("Invalid slug format.");
                }
                if (!await IsPathUniqueAsync(newPath, existingArtist.ArtistID))
                {
                    return BadRequest("Path is not unique.");
                }
                existingArtist.Path = newPath;
            }
        }
        if (props.TryGetValue("SEOTags", out p)) existingArtist.SEOTags = GetString(p) ?? existingArtist.SEOTags;
        if (props.TryGetValue("Since", out p) && p.ValueKind == JsonValueKind.String)
        {
            var parsedSince = DateTime.Parse(p.GetString(), CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal);
            existingArtist.Since = DateTime.SpecifyKind(parsedSince.ToUniversalTime(), DateTimeKind.Utc);
        }
        if (props.TryGetValue("Statement", out p)) existingArtist.Statement = GetString(p) ?? existingArtist.Statement;
        if (props.TryGetValue("Title", out p)) existingArtist.Title = GetString(p) ?? existingArtist.Title;
        if (props.TryGetValue("CoverPicID", out p) && p.ValueKind == JsonValueKind.Number && p.TryGetInt32(out var cid)) existingArtist.CoverPicID = cid;
        if (props.TryGetValue("FocusCategoryID", out p) && p.ValueKind == JsonValueKind.Number && p.TryGetInt32(out var fcid)) existingArtist.FocusCategoryID = fcid;
        if (props.TryGetValue("ProfilePicID", out p) && p.ValueKind == JsonValueKind.Number && p.TryGetInt32(out var pid)) existingArtist.ProfilePicID = pid;

        try
        {
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ArtistExists(existingArtist.ArtistID))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    [HttpPut("{slug}")]
    public async Task<IActionResult> Update(string slug)
    {
        if (_context.Artists == null)
        {
            return NotFound();
        }

        var existingArtist = await _context.Artists.FirstOrDefaultAsync(a => a.Path == slug).ConfigureAwait(false);
        if (existingArtist == null)
        {
            return NotFound();
        }

        // Read raw body to avoid model binding and nested ArtistPermissions validation
        using var reader = new StreamReader(Request.Body);
        var bodyText = await reader.ReadToEndAsync();
        if (string.IsNullOrWhiteSpace(bodyText))
        {
            return BadRequest("Missing artist payload.");
        }

        JsonDocument doc;
        try
        {
            doc = JsonDocument.Parse(bodyText);
        }
        catch (JsonException)
        {
            return BadRequest("Invalid JSON payload.");
        }

        var root = doc.RootElement;
        var props = new Dictionary<string, JsonElement>(StringComparer.OrdinalIgnoreCase);
        foreach (var prop in root.EnumerateObject()) props[prop.Name] = prop.Value;

        static string GetString(JsonElement el) => el.ValueKind == JsonValueKind.Null ? null : el.GetString();

        if (props.TryGetValue("Applied", out var p))
        {
            DateTime parsedApplied;
            if (p.ValueKind == JsonValueKind.String)
            {
                parsedApplied = DateTime.Parse(p.GetString(), CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal);
            }
            else
            {
                parsedApplied = p.GetDateTime();
            }

            existingArtist.Applied = DateTime.SpecifyKind(parsedApplied.ToUniversalTime(), DateTimeKind.Utc);
        }
        if (props.TryGetValue("Biography", out p)) existingArtist.Biography = GetString(p) ?? existingArtist.Biography;
        if (props.TryGetValue("Byline", out p)) existingArtist.Byline = GetString(p) ?? existingArtist.Byline;
        if (props.TryGetValue("Path", out p))
        {
            var newPath = GetString(p);
            if (newPath != null && newPath != existingArtist.Path)
            {
                if (!await IsPathUniqueAsync(newPath, existingArtist.ArtistID))
                {
                    return BadRequest("Path is not unique.");
                }
                existingArtist.Path = newPath;
            }
        }
        if (props.TryGetValue("SEOTags", out p)) existingArtist.SEOTags = GetString(p) ?? existingArtist.SEOTags;
        if (props.TryGetValue("Since", out p) && p.ValueKind == JsonValueKind.String)
        {
            var parsedSince = DateTime.Parse(p.GetString(), CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal);
            existingArtist.Since = DateTime.SpecifyKind(parsedSince.ToUniversalTime(), DateTimeKind.Utc);
        }
        if (props.TryGetValue("Statement", out p)) existingArtist.Statement = GetString(p) ?? existingArtist.Statement;
        if (props.TryGetValue("Title", out p)) existingArtist.Title = GetString(p) ?? existingArtist.Title;
        if (props.TryGetValue("CoverPicID", out p) && p.ValueKind == JsonValueKind.Number && p.TryGetInt32(out var cid)) existingArtist.CoverPicID = cid;
        if (props.TryGetValue("FocusCategoryID", out p) && p.ValueKind == JsonValueKind.Number && p.TryGetInt32(out var fcid)) existingArtist.FocusCategoryID = fcid;
        if (props.TryGetValue("ProfilePicID", out p) && p.ValueKind == JsonValueKind.Number && p.TryGetInt32(out var pid)) existingArtist.ProfilePicID = pid;

        try
        {
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ArtistExists(existingArtist.ArtistID))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        if (_context.Artists == null)
        {
            return NotFound();
        }

        var artist = await _context.Artists.FindAsync(id);
        if (artist == null)
        {
            return NotFound();
        }

        _context.Artists.Remove(artist);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ArtistExists(int id)
    {
        return (_context.Artists?.Any(e => e.ArtistID == id)).GetValueOrDefault();
    }

    // Add a helper method to generate paths if needed
    private async Task<string> GenerateUniquePathAsync(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Title cannot be empty", nameof(title));
        }

        // Convert title to path format
        var path = title.ToLowerInvariant()
            .Replace(" ", "-")
            .Replace("_", "-")
            .Replace(".", "-")
            .Replace("&", "and");

        // Remove any characters not allowed in a path
        path = Regex.Replace(path, @"[^a-z0-9\-]", string.Empty);
        
        // Replace multiple hyphens with a single hyphen
        path = Regex.Replace(path, @"\-{2,}", "-");
        
        // Trim hyphens from start and end
        path = path.Trim('-');
        
        // If path is empty after processing, use a default
        if (string.IsNullOrWhiteSpace(path))
        {
            path = $"artist-{Guid.NewGuid().ToString().Substring(0, 8)}";
        }

        // Make sure it's unique
        var counter = 0;
        var originalPath = path;
        
        while (await _context.Artists.AnyAsync(a => a.Path == path))
        {
            counter++;
            path = $"{originalPath}-{counter}";
        }

        return path;
    }

    // Add a helper method to validate paths
    private async Task<bool> IsPathUniqueAsync(string path, int? artistId = null)
    {
        var query = _context.Artists.AsQueryable();
        
        if (artistId.HasValue)
        {
            query = query.Where(a => a.ArtistID != artistId.Value);
        }
        
        return !await query.AnyAsync(a => a.Path == path);
    }
}

// Request models for slug operations
public class ArtistSlugReservationRequest
{
    [Required]
    [StringLength(255, MinimumLength = 1)]
    public string Slug { get; set; }
    
    [StringLength(1000)]
    public string Title { get; set; }
}

public class ArtistSlugUpdateRequest
{
    [Required]
    [StringLength(255, MinimumLength = 1)]
    public string Slug { get; set; }
}

public class ArtistSlugReservationResponse
{
    public int ArtistID { get; set; }
    public string Path { get; set; }
    public string Title { get; set; }
}
