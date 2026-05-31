// <copyright file="ArtistController.cs" company="Twisted Artists Guild">
// Copyright © Twisted Artists Guild. All rights reserved
// </copyright>

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;
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
        var artists = await _context.Set<Artist>()
            .Include(a => a.ProfilePic)
            .Include(a => a.CoverPic)
            .Include(a => a.Listings)
                .ThenInclude(l => l.CoverPic)
            .ToListAsync()
            .ConfigureAwait(false);

        // Limit each artist's listings to top 3 for gallery preview
        foreach (var artist in artists)
        {
            if (artist.Listings != null && artist.Listings.Count > 3)
            {
                artist.Listings = artist.Listings.Take(3).ToList();
            }
        }

        return artists;
    }

    [HttpGet("byID/{id}")]
    public async Task<ActionResult<Artist>> GetByID(int id)
    {
        if (_context.Artists == null)
        {
            return NotFound();
        }
        var artist = await _context.Artists
            .Include(a => a.ProfilePic)
            .Include(a => a.CoverPic)
            .Include(a => a.Gallery!)
                .ThenInclude(g => g.GalleryItems)
                .ThenInclude(gi => gi.Picture)
            .Include(a => a.Gallery!)
                .ThenInclude(g => g.GalleryItems)
                .ThenInclude(gi => gi.Video)
            .FirstOrDefaultAsync(a => a.ArtistID == id)
            .ConfigureAwait(false);

        if (artist == null)
        {
            return NotFound();
        }

        return artist;
    }

    [HttpGet("{slug}")]
    public async Task<ActionResult<Artist>> Get(string slug)
    {
        var normalizedSlug = NormalizeSlug(slug);

        var artist = await _context.Set<Artist>()
            .AsNoTracking()
            .Include(a => a.ProfilePic)
            .Include(a => a.CoverPic)
            .Include(a => a.Gallery!)
                .ThenInclude(g => g.GalleryItems)
                .ThenInclude(gi => gi.Picture)
            .Include(a => a.Gallery!)
                .ThenInclude(g => g.GalleryItems)
                .ThenInclude(gi => gi.Video)
            .FirstOrDefaultAsync(a => a.Path.ToLower() == normalizedSlug)
            .ConfigureAwait(false);
        if (artist == null)
        {
            return this.NotFound();
        }

        return artist;
    }

    [HttpGet("check-slug/{slug}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<object>> CheckArtistSlug(string slug, [FromQuery] int? excludeId = null)
    {
        var normalizedSlug = NormalizeSlug(slug);

        if (!ValidPathRegex.IsMatch(normalizedSlug))
        {
            return BadRequest(new { available = false, message = "Invalid slug format." });
        }

        var isAvailable = await IsPathUniqueAsync(normalizedSlug, excludeId);
        if (!isAvailable)
        {
            return Conflict(new { available = false, message = "Slug is already in use." });
        }

        return Ok(new { available = true, slug = normalizedSlug });
    }

    [HttpGet("{slug}/contacts")]
    [HttpGet("/api/artists/{slug}/contacts")]
    public async Task<ActionResult<object>> GetArtistContacts(string slug)
    {
        if (_context.Artists == null)
        {
            return NotFound();
        }

        var normalizedSlug = NormalizeSlug(slug);

        var artist = await _context.Artists
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Path.ToLower() == normalizedSlug)
            .ConfigureAwait(false);

        if (artist == null)
        {
            return NotFound();
        }

        var contactLinks = await _context.Set<Linker_EntityToContact>()
            .AsNoTracking()
            .Where(l => l.ArtistID == artist.ArtistID && l.MakePublic)
            .Include(l => l.Contact)
                .ThenInclude(c => c.Address)
            .Include(l => l.Contact)
                .ThenInclude(c => c.PhoneContact)
            .OrderBy(l => l.DisplayOrder)
            .ToListAsync()
            .ConfigureAwait(false);

        var projected = contactLinks
            .Select(l => new
            {
                linkId = l.Linker_EntityToContactID,
                contactId = l.ContactID,
                contactType = l.Contact.ContactType,
                label = l.Contact.Label,
                category = l.Contact.Category,
                value = l.Contact.ContactType == "phone"
                    ? l.Contact.PhoneContact != null ? l.Contact.PhoneContact.PhoneNumber : null
                    : l.Contact.Value,
                handle = l.Contact.Handle,
                description = l.Contact.Description,
                address = l.Contact.ContactType == "address"
                    ? new
                    {
                        line1 = l.Contact.Address != null ? l.Contact.Address.AddressLine1 : null,
                        line2 = l.Contact.Address != null ? l.Contact.Address.AddressLine2 : null,
                        city = l.Contact.Address != null ? l.Contact.Address.City : null,
                        region = l.Contact.Address != null ? l.Contact.Address.Region ?? l.Contact.Address.State : null,
                        postalCode = l.Contact.Address != null ? l.Contact.Address.ZipCode : null,
                        country = l.Contact.Address != null ? l.Contact.Address.Country : null,
                        operationHours = l.Contact.Address != null ? l.Contact.Address.OperationHours : null,
                    }
                    : null,
                displayOrder = l.DisplayOrder,
            })
            .ToList();

        var links = projected
            .Where(c => c.contactType == "url" && !string.IsNullOrWhiteSpace(c.value))
            .Select(c => new
            {
                label = c.label ?? c.handle ?? "Website",
                url = c.value,
                purpose = c.description,
                category = c.category,
            })
            .ToList();

        return new
        {
            artistId = artist.ArtistID,
            artistPath = artist.Path,
            links,
            contacts = projected,
            byType = new
            {
                addresses = projected.Where(c => c.contactType == "address").ToList(),
                phones = projected.Where(c => c.contactType == "phone").ToList(),
                emails = projected.Where(c => c.contactType == "email").ToList(),
                urls = projected.Where(c => c.contactType == "url").ToList(),
            },
        };
    }

    [HttpGet("{slug}/profile")]
    public async Task<ActionResult<object>> GetArtistProfile(string slug)
    {
        if (_context.Artists == null)
        {
            return NotFound();
        }

        var normalizedSlug = NormalizeSlug(slug);

        // Get the artist with all the required navigation properties included
        var artist = await _context.Artists
            .AsNoTracking()
            .Include(a => a.ProfilePic)
            .Include(a => a.CoverPic)
            .Include(a => a.Contacts)
                .ThenInclude(c => c.Address)
            .Include(a => a.Contacts)
                .ThenInclude(c => c.PhoneContact)
            .Include(a => a.Contacts)
                .ThenInclude(c => c.ExternalLink)
            .AsSplitQuery() // This optimizes the query when including multiple collections
            .FirstOrDefaultAsync(a => a.Path.ToLower() == normalizedSlug)
            .ConfigureAwait(false);

        if (artist == null)
        {
            _logger.LogWarning("Artist not found for slug: {Slug}", slug);
            return NotFound();
        }

        // Transitional fallback: tolerate DBs that do not yet have ArtCategory hierarchy columns.
        List<Listing> listings;
        try
        {
            listings = await _context.Set<Listing>()
                .Where(l => l.ArtistID == artist.ArtistID)
                .Include(l => l.ArtCategory)
                .Include(l => l.CoverPic)
                .ToListAsync()
                .ConfigureAwait(false);
        }
        catch (PostgresException ex)
            when (ex.SqlState == PostgresErrorCodes.UndefinedColumn && ex.MessageText.Contains("ParentArtCategoryID", StringComparison.OrdinalIgnoreCase))
        {
            _logger.LogWarning(ex, "Falling back to listings without ArtCategory include because ParentArtCategoryID is missing in the current DB schema.");

            listings = await _context.Set<Listing>()
                .Where(l => l.ArtistID == artist.ArtistID)
                .Include(l => l.CoverPic)
                .ToListAsync()
                .ConfigureAwait(false);
        }

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

    private static string NormalizeSlug(string slug)
    {
        return string.IsNullOrWhiteSpace(slug)
            ? string.Empty
            : slug.Trim().ToLowerInvariant();
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
        var normalizedSlug = NormalizeSlug(request.Slug);

        if (!ValidPathRegex.IsMatch(normalizedSlug))
        {
            return BadRequest("Invalid slug format.");
        }

        if (!await IsPathUniqueAsync(normalizedSlug))
        {
            return Conflict("Slug is already in use.");
        }

        // Create a new artist with minimal required fields
        var artist = new Artist
        {
            Path = normalizedSlug,
            Title = request.Title ?? normalizedSlug,
            Country = string.IsNullOrWhiteSpace(request.Country) ? null : request.Country.Trim(),
            StateOrProvince = string.IsNullOrWhiteSpace(request.StateOrProvince) ? null : request.StateOrProvince.Trim(),
            BusinessEntityType = string.IsNullOrWhiteSpace(request.BusinessEntityType) ? null : request.BusinessEntityType.Trim(),
            IsFormallyIncorporated = request.IsFormallyIncorporated,
            IncorporatedYear = request.IncorporatedYear,
            Since = DateTime.UtcNow,
            Applied = DateTime.UtcNow,
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
                Country = artist.Country,
                StateOrProvince = artist.StateOrProvince,
                BusinessEntityType = artist.BusinessEntityType,
                IsFormallyIncorporated = artist.IsFormallyIncorporated,
                IncorporatedYear = artist.IncorporatedYear,
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
        var normalizedSlug = NormalizeSlug(request.Slug);

        if (NormalizeSlug(artist.Path) == normalizedSlug)
        {
            return Ok(new ArtistSlugReservationResponse
            {
                ArtistID = artist.ArtistID,
                Path = artist.Path,
                Title = artist.Title,
            });
        }

        // Validate the slug format and availability
        if (!ValidPathRegex.IsMatch(normalizedSlug))
        {
            return BadRequest("Invalid slug format.");
        }

        if (!await IsPathUniqueAsync(normalizedSlug, id))
        {
            return Conflict("Slug is already in use.");
        }

        // Update the slug
        artist.Path = normalizedSlug;

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
        if (props.TryGetValue("Country", out p)) existingArtist.Country = GetString(p);
        if (props.TryGetValue("StateOrProvince", out p)) existingArtist.StateOrProvince = GetString(p);
        if (props.TryGetValue("BusinessEntityType", out p)) existingArtist.BusinessEntityType = GetString(p);
        if (props.TryGetValue("IsFormallyIncorporated", out p) && p.ValueKind is JsonValueKind.True or JsonValueKind.False)
        {
            existingArtist.IsFormallyIncorporated = p.GetBoolean();
        }
        if (props.TryGetValue("IncorporatedYear", out p))
        {
            if (p.ValueKind == JsonValueKind.Number && p.TryGetInt32(out var incYear)) existingArtist.IncorporatedYear = incYear;
            else if (p.ValueKind == JsonValueKind.Null) existingArtist.IncorporatedYear = null;
        }
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
        if (props.TryGetValue("Country", out p)) existingArtist.Country = GetString(p);
        if (props.TryGetValue("StateOrProvince", out p)) existingArtist.StateOrProvince = GetString(p);
        if (props.TryGetValue("BusinessEntityType", out p)) existingArtist.BusinessEntityType = GetString(p);
        if (props.TryGetValue("IsFormallyIncorporated", out p) && p.ValueKind is JsonValueKind.True or JsonValueKind.False)
        {
            existingArtist.IsFormallyIncorporated = p.GetBoolean();
        }
        if (props.TryGetValue("IncorporatedYear", out p))
        {
            if (p.ValueKind == JsonValueKind.Number && p.TryGetInt32(out var incYear)) existingArtist.IncorporatedYear = incYear;
            else if (p.ValueKind == JsonValueKind.Null) existingArtist.IncorporatedYear = null;
        }
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
        var normalizedPath = NormalizeSlug(path);

        var query = _context.Artists.AsQueryable();
        
        if (artistId.HasValue)
        {
            query = query.Where(a => a.ArtistID != artistId.Value);
        }
        
        return !await query.AnyAsync(a => (a.Path ?? string.Empty).Trim().ToLower() == normalizedPath);
    }

    private static string NormalizeResourceUrl(string? value)
    {
        return string.IsNullOrWhiteSpace(value)
            ? string.Empty
            : value.Trim().ToLowerInvariant();
    }

    private async Task<Gallery> EnsureArtistGalleryAsync(Artist artist)
    {
        if (artist.GalleryID.HasValue)
        {
            var existingGallery = await _context.Galleries
                .FirstOrDefaultAsync(g => g.GalleryID == artist.GalleryID.Value)
                .ConfigureAwait(false);

            if (existingGallery != null)
            {
                return existingGallery;
            }
        }

        var now = DateTime.UtcNow;
        var gallery = new Gallery
        {
            ScopeType = "artist",
            ScopeEntityID = artist.ArtistID,
            OwnerArtistID = artist.ArtistID,
            IsPrimary = true,
            Title = $"Artist {artist.ArtistID} Gallery",
            Created = now,
            Updated = now,
        };

        _context.Galleries.Add(gallery);
        await _context.SaveChangesAsync().ConfigureAwait(false);

        artist.GalleryID = gallery.GalleryID;
        await _context.SaveChangesAsync().ConfigureAwait(false);

        return gallery;
    }

    [HttpPost("{id}/gallery/video")]
    public async Task<ActionResult<GalleryItem>> PostArtistGalleryVideo(int id, [FromBody] BlogVideoUpsertRequest request)
    {
        var artist = await _context.Artists
            .FirstOrDefaultAsync(a => a.ArtistID == id)
            .ConfigureAwait(false);

        if (artist == null)
        {
            return NotFound();
        }

        var normalizedEmbed = NormalizeResourceUrl(request?.EmbedURL);
        if (string.IsNullOrWhiteSpace(normalizedEmbed))
        {
            return BadRequest("EmbedURL is required.");
        }

        var normalizedSourceUrl = NormalizeResourceUrl(request?.URL);
        var now = DateTime.UtcNow;

        var video = await _context.Videos
            .FirstOrDefaultAsync(v =>
                v.NormalizedEmbedURL == normalizedEmbed ||
                v.EmbedURL == request!.EmbedURL ||
                (!string.IsNullOrWhiteSpace(normalizedSourceUrl) && v.URL != null && v.URL.ToLower() == normalizedSourceUrl))
            .ConfigureAwait(false);

        if (video == null)
        {
            video = new Video
            {
                EmbedURL = request!.EmbedURL!.Trim(),
                URL = request.URL,
                ThumbnailURL = request.ThumbnailURL,
                Title = request.Title,
                Byline = request.Byline,
                Description = request.Description,
                Provider = string.IsNullOrWhiteSpace(request.Provider) ? "vimeo" : request.Provider.Trim().ToLowerInvariant(),
                ProviderVideoID = request.ProviderVideoID,
                NormalizedEmbedURL = normalizedEmbed,
                ArtistID = artist.ArtistID,
                Created = now,
                Updated = now,
            };

            _context.Videos.Add(video);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }
        else
        {
            video.URL = string.IsNullOrWhiteSpace(request?.URL) ? video.URL : request!.URL;
            video.ThumbnailURL = string.IsNullOrWhiteSpace(request?.ThumbnailURL) ? video.ThumbnailURL : request!.ThumbnailURL;
            video.Title = string.IsNullOrWhiteSpace(request?.Title) ? video.Title : request!.Title;
            video.Byline = string.IsNullOrWhiteSpace(request?.Byline) ? video.Byline : request!.Byline;
            video.Description = string.IsNullOrWhiteSpace(request?.Description) ? video.Description : request!.Description;
            video.Provider = string.IsNullOrWhiteSpace(request?.Provider) ? video.Provider : request!.Provider!.Trim().ToLowerInvariant();
            video.ProviderVideoID = string.IsNullOrWhiteSpace(request?.ProviderVideoID) ? video.ProviderVideoID : request!.ProviderVideoID;
            video.NormalizedEmbedURL = normalizedEmbed;
            video.Updated = now;
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        var gallery = await EnsureArtistGalleryAsync(artist).ConfigureAwait(false);

        var existingGalleryItem = await _context.GalleryItems
            .FirstOrDefaultAsync(item => item.GalleryID == gallery.GalleryID && item.VideoID == video.VideoID)
            .ConfigureAwait(false);

        if (existingGalleryItem == null)
        {
            var nextSortOrder = await _context.GalleryItems
                .Where(item => item.GalleryID == gallery.GalleryID)
                .Select(item => (int?)item.SortOrder)
                .MaxAsync()
                .ConfigureAwait(false) ?? -1;

            existingGalleryItem = new GalleryItem
            {
                GalleryID = gallery.GalleryID,
                VideoID = video.VideoID,
                SortOrder = nextSortOrder + 1,
                Created = now,
            };

            _context.GalleryItems.Add(existingGalleryItem);
            gallery.Updated = now;
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        var reloaded = await _context.GalleryItems
            .Include(gi => gi.Video)
            .FirstAsync(gi => gi.GalleryItemID == existingGalleryItem.GalleryItemID)
            .ConfigureAwait(false);

        return Ok(reloaded);
    }

    [HttpPut("{id}/gallery/order")]
    public async Task<ActionResult<IEnumerable<GalleryItem>>> PutArtistGalleryOrder(int id, [FromBody] BlogGalleryOrderRequest request)
    {
        var artist = await _context.Artists
            .FirstOrDefaultAsync(a => a.ArtistID == id)
            .ConfigureAwait(false);

        if (artist == null)
        {
            return NotFound();
        }

        var orderedItems = request?.Items ?? new List<BlogGalleryOrderItemDto>();
        var gallery = await EnsureArtistGalleryAsync(artist).ConfigureAwait(false);
        var existingGalleryItems = await _context.GalleryItems
            .Where(item => item.GalleryID == gallery.GalleryID)
            .ToListAsync()
            .ConfigureAwait(false);

        var captionByPictureId = existingGalleryItems
            .Where(item => item.PictureID.HasValue)
            .GroupBy(item => item.PictureID!.Value)
            .ToDictionary(group => group.Key, group => group.First().CaptionOverride);

        var captionByVideoId = existingGalleryItems
            .Where(item => item.VideoID.HasValue)
            .GroupBy(item => item.VideoID!.Value)
            .ToDictionary(group => group.Key, group => group.First().CaptionOverride);

        var resolvedItems = new List<(int? PictureID, int? VideoID, int SortOrder)>();

        for (var index = 0; index < orderedItems.Count; index++)
        {
            var requestedItem = orderedItems[index];
            var mediaType = (requestedItem.MediaType ?? string.Empty).Trim().ToLowerInvariant();

            if (mediaType == "video")
            {
                Video? resolvedVideo = null;
                if (requestedItem.VideoID.HasValue)
                {
                    resolvedVideo = await _context.Videos
                        .FirstOrDefaultAsync(v => v.VideoID == requestedItem.VideoID.Value)
                        .ConfigureAwait(false);
                }

                if (resolvedVideo == null)
                {
                    var normalizedVideoUrl = NormalizeResourceUrl(requestedItem.Url);
                    var normalizedEmbedUrl = NormalizeResourceUrl(requestedItem.EmbedURL);

                    resolvedVideo = await _context.Videos
                        .FirstOrDefaultAsync(v =>
                            (!string.IsNullOrWhiteSpace(normalizedEmbedUrl) && v.NormalizedEmbedURL == normalizedEmbedUrl) ||
                            (!string.IsNullOrWhiteSpace(normalizedVideoUrl) && v.URL != null && v.URL.ToLower() == normalizedVideoUrl))
                        .ConfigureAwait(false);
                }

                if (resolvedVideo == null)
                {
                    return BadRequest($"Unable to resolve video at gallery index {index}.");
                }

                resolvedItems.Add((null, resolvedVideo.VideoID, index));
                continue;
            }

            Picture? resolvedPicture = null;
            if (requestedItem.PictureID.HasValue)
            {
                resolvedPicture = await _context.Pictures
                    .FirstOrDefaultAsync(p => p.PictureID == requestedItem.PictureID.Value)
                    .ConfigureAwait(false);
            }

            if (resolvedPicture == null)
            {
                var normalizedPictureUrl = NormalizeResourceUrl(requestedItem.Url);
                resolvedPicture = await _context.Pictures
                    .FirstOrDefaultAsync(p =>
                        p.NormalizedURL == normalizedPictureUrl ||
                        p.URL == requestedItem.Url)
                    .ConfigureAwait(false);
            }

            if (resolvedPicture == null)
            {
                return BadRequest($"Unable to resolve picture at gallery index {index}.");
            }

            resolvedItems.Add((resolvedPicture.PictureID, null, index));
        }

        _context.GalleryItems.RemoveRange(existingGalleryItems);
        await _context.SaveChangesAsync().ConfigureAwait(false);

        var now = DateTime.UtcNow;
        var replacementItems = resolvedItems.Select(item => new GalleryItem
        {
            GalleryID = gallery.GalleryID,
            PictureID = item.PictureID,
            VideoID = item.VideoID,
            SortOrder = item.SortOrder,
            CaptionOverride = item.PictureID.HasValue
                ? captionByPictureId.GetValueOrDefault(item.PictureID.Value)
                : (item.VideoID.HasValue ? captionByVideoId.GetValueOrDefault(item.VideoID.Value) : null),
            Created = now,
        }).ToList();

        _context.GalleryItems.AddRange(replacementItems);
        gallery.Updated = now;
        await _context.SaveChangesAsync().ConfigureAwait(false);

        var refreshedItems = await _context.GalleryItems
            .Where(gi => gi.GalleryID == gallery.GalleryID)
            .Include(gi => gi.Picture)
            .Include(gi => gi.Video)
            .OrderBy(gi => gi.SortOrder)
            .ToListAsync()
            .ConfigureAwait(false);

        return Ok(refreshedItems);
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

    [StringLength(120)]
    public string? Country { get; set; }

    [StringLength(120)]
    public string? StateOrProvince { get; set; }

    [StringLength(80)]
    public string? BusinessEntityType { get; set; }

    public bool? IsFormallyIncorporated { get; set; }

    [Range(1800, 2100)]
    public int? IncorporatedYear { get; set; }
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
    public string? Country { get; set; }
    public string? StateOrProvince { get; set; }
    public string? BusinessEntityType { get; set; }
    public bool? IsFormallyIncorporated { get; set; }
    public int? IncorporatedYear { get; set; }
}
