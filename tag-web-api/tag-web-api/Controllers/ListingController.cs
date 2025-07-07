// <copyright file="ListingController.cs" company="Twisted Artists Guild">
// Copyright © Twisted Artists Guild. All rights reserved
// </copyright>

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TAGWEBAPI.Data;
using TAGWEBAPI.Models;

namespace TAGWEBAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ListingController : ControllerBase
    {
        private readonly TAGDBContext _context;
        private readonly ILogger<ListingController> _logger;
        private static readonly Regex ValidPathRegex = new(@"^[a-z0-9](?:[a-z0-9\-]*[a-z0-9])?$", RegexOptions.Compiled);

        public ListingController(TAGDBContext context, ILogger<ListingController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Listing>>> GetListings()
        {
            if (_context.Listings == null)
            {
                return NotFound();
            }
            return await _context.Listings
                .Include(l => l.Artist)
                .Include(l => l.ArtCategory)
                .Include(l => l.ProfilePic)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Listing>> GetListing(int id)
        {
            if (_context.Listings == null)
            {
                return NotFound();
            }
            var listing = await _context.Listings
                .Include(l => l.Artist)
                .Include(l => l.ArtCategory)
                .Include(l => l.ProfilePic)
                .FirstOrDefaultAsync(l => l.ListingID == id);

            if (listing == null)
            {
                return NotFound();
            }

            return listing;
        }

        [HttpGet("path/{path}")]
        public async Task<ActionResult<Listing>> GetListingByPath(string path)
        {
            if (_context.Listings == null)
            {
                return NotFound();
            }
            var listing = await _context.Listings
                .Include(l => l.Artist)
                .Include(l => l.ArtCategory)
                .Include(l => l.ProfilePic)
                .FirstOrDefaultAsync(l => l.Path == path);

            if (listing == null)
            {
                return NotFound();
            }

            return listing;
        }

        [HttpGet("artist/{artistPath}/listing/{listingPath}")]
        public async Task<ActionResult<Listing>> GetListingByArtistAndPath(string artistPath, string listingPath)
        {
            if (_context.Listings == null)
            {
                return NotFound();
            }
            
            var listing = await _context.Listings
                .Include(l => l.Artist)
                .Include(l => l.ArtCategory)
                .Include(l => l.ProfilePic)
                .FirstOrDefaultAsync(l => l.Artist.Path == artistPath && l.Path == listingPath);

            if (listing == null)
            {
                return NotFound("Listing not found for the specified artist and path.");
            }

            return listing;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutListing(int id, Listing listing)
        {
            if (id != listing.ListingID)
            {
                return BadRequest("ID mismatch");
            }

            // Ensure the path is valid and unique for this artist
            if (!ValidPathRegex.IsMatch(listing.Path))
            {
                return BadRequest("Invalid path format.");
            }

            if (!await IsPathUniqueForArtistAsync(listing.Path, listing.ArtistID, id))
            {
                return BadRequest("Path is not unique for this artist.");
            }

            _context.Entry(listing).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ListingExists(id))
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

        [HttpPost]
        public async Task<ActionResult<Listing>> PostListing(Listing listing)
        {
            if (_context.Listings == null)
            {
                return Problem("Entity set 'TAGDBContext.Listings' is null.");
            }

            // Generate a unique path if not provided
            if (string.IsNullOrWhiteSpace(listing.Path))
            {
                listing.Path = await GenerateUniquePathForArtistAsync(listing.Title, listing.ArtistID);
            }

            // Ensure the path is valid and unique for this artist
            if (!ValidPathRegex.IsMatch(listing.Path))
            {
                return BadRequest("Invalid path format.");
            }

            if (!await IsPathUniqueForArtistAsync(listing.Path, listing.ArtistID))
            {
                return BadRequest("Path is not unique for this artist.");
            }

            _context.Listings.Add(listing);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetListing", new { id = listing.ListingID }, listing);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteListing(int id)
        {
            if (_context.Listings == null)
            {
                return NotFound();
            }
            var listing = await _context.Listings.FindAsync(id);
            if (listing == null)
            {
                return NotFound();
            }

            _context.Listings.Remove(listing);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ListingExists(int id)
        {
            return (_context.Listings?.Any(e => e.ListingID == id)).GetValueOrDefault();
        }

        /// <summary>
        /// Generates a unique path for a listing based on its title within an artist's domain
        /// </summary>
        /// <param name="title">The listing title to convert into a path</param>
        /// <param name="artistId">The artist ID to scope the uniqueness check</param>
        /// <returns>A unique SEO-friendly path within the artist's domain</returns>
        private async Task<string> GenerateUniquePathForArtistAsync(string title, int artistId)
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
                path = $"listing-{Guid.NewGuid().ToString().Substring(0, 8)}";
            }

            // Make sure it's unique for this artist
            var counter = 0;
            var originalPath = path;
            
            while (await _context.Listings.AnyAsync(l => l.Path == path && l.ArtistID == artistId))
            {
                counter++;
                path = $"{originalPath}-{counter}";
            }

            _logger.LogInformation("Generated unique path {Path} for artist {ArtistId} and listing title {Title}", path, artistId, title);
            return path;
        }

        /// <summary>
        /// Validates that a path is unique among listings for a specific artist
        /// </summary>
        /// <param name="path">The path to validate</param>
        /// <param name="artistId">The artist ID to scope the uniqueness check</param>
        /// <param name="listingId">Optional listing ID to exclude from uniqueness check</param>
        /// <returns>True if the path is unique for the artist, false otherwise</returns>
        private async Task<bool> IsPathUniqueForArtistAsync(string path, int artistId, int? listingId = null)
        {
            var query = _context.Listings
                .Where(l => l.ArtistID == artistId && l.Path == path);
            
            if (listingId.HasValue)
            {
                query = query.Where(l => l.ListingID != listingId.Value);
            }
            
            return !await query.AnyAsync();
        }

        // Deprecated but kept for backward compatibility
        private async Task<string> GenerateUniquePathAsync(string title)
        {
            _logger.LogWarning("Deprecated GenerateUniquePathAsync method called. Use GenerateUniquePathForArtistAsync instead.");
            return await GenerateUniquePathForArtistAsync(title, 1); // Default to artist 1
        }

        // Deprecated but kept for backward compatibility
        private async Task<bool> IsPathUniqueAsync(string path, int? listingId = null)
        {
            _logger.LogWarning("Deprecated IsPathUniqueAsync method called. Use IsPathUniqueForArtistAsync instead.");
            
            var query = _context.Listings.AsQueryable();
            
            if (listingId.HasValue)
            {
                query = query.Where(l => l.ListingID != listingId.Value);
            }
            
            return !await query.AnyAsync(l => l.Path == path);
        }
    }
}
