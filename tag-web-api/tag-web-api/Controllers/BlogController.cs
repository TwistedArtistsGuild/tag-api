// <copyright file="BlogController.cs" company="Twisted Artists Guild">
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
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace TAGWEBAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlogController : ControllerBase
    {
        private readonly TAGDBContext _context;
        private readonly ILogger<BlogController> _logger;
        private static readonly Regex ValidPathRegex = new(@"^[a-z0-9](?:[a-z0-9\-]*[a-z0-9])?$", RegexOptions.Compiled);

        public BlogController(TAGDBContext context, ILogger<BlogController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Blog>>> GetBlogs()
        {
            if (_context.Blogs == null)
            {
                return NotFound();
            }
            return await _context.Blogs
                .Include(b => b.User)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Blog>> GetBlog(int id)
        {
            if (_context.Blogs == null)
            {
                return NotFound();
            }
            var blog = await _context.Blogs
                .Include(b => b.User)
                .FirstOrDefaultAsync(b => b.BlogID == id);

            if (blog == null)
            {
                return NotFound();
            }

            return blog;
        }

        [HttpGet("path/{path}")]
        public async Task<ActionResult<Blog>> GetBlogByPath(string path)
        {
            if (_context.Blogs == null)
            {
                return NotFound();
            }
            var blog = await _context.Blogs
                .Include(b => b.User)
                .FirstOrDefaultAsync(b => b.Path == path);

            if (blog == null)
            {
                return NotFound();
            }

            return blog;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBlog(int id, Blog blog)
        {
            if (id != blog.BlogID)
            {
                return BadRequest();
            }

            // Set current timestamp for Modified field
            blog.Modified = DateTime.UtcNow;

            // Ensure the path is valid and unique
            if (!ValidPathRegex.IsMatch(blog.Path))
            {
                return BadRequest("Invalid path format.");
            }

            if (!await IsPathUniqueAsync(blog.Path, id))
            {
                return Conflict("Path is already in use.");
            }

            _context.Entry(blog).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BlogExists(id))
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
        public async Task<ActionResult<Blog>> PostBlog(Blog blog)
        {
            if (_context.Blogs == null)
            {
                return Problem("Entity set 'TAGDBContext.Blogs' is null.");
            }

            // Set creation timestamp
            blog.Created = DateTime.UtcNow;

            // Generate a unique path if not provided
            if (string.IsNullOrWhiteSpace(blog.Path))
            {
                blog.Path = await GenerateUniquePathAsync(blog.Title);
            }

            // Ensure the path is valid and unique
            if (!ValidPathRegex.IsMatch(blog.Path))
            {
                return BadRequest("Invalid path format.");
            }

            if (!await IsPathUniqueAsync(blog.Path))
            {
                return Conflict("Path is already in use.");
            }

            _context.Blogs.Add(blog);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBlog", new { id = blog.BlogID }, blog);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlog(int id)
        {
            if (_context.Blogs == null)
            {
                return NotFound();
            }
            var blog = await _context.Blogs.FindAsync(id);
            if (blog == null)
            {
                return NotFound();
            }

            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Reserves a slug for a new blog by creating a minimal blog record
        /// </summary>
        /// <param name="request">The blog slug reservation request</param>
        /// <returns>The created blog with ID and slug</returns>
        [HttpPost("reserve-slug")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<BlogSlugReservationResponse>> ReserveBlogSlug([FromBody] BlogSlugReservationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Validate the slug format
            if (!ValidPathRegex.IsMatch(request.Slug))
            {
                return BadRequest("Invalid slug format.");
            }

            // Ensure the slug is unique
            if (!await IsPathUniqueAsync(request.Slug))
            {
                return Conflict("Slug is already in use.");
            }

            // Create a new blog with minimal required fields
            var blog = new Blog
            {
                Path = request.Slug,
                Title = request.Title ?? request.Slug, // Default to slug if title isn't provided
                Byline = request.Byline ?? string.Empty, // Required field
                Body = request.Body ?? string.Empty,    // Required field
                UserID = request.UserID,
                Created = DateTime.UtcNow,
                Modified = DateTime.UtcNow
            };

            try
            {
                _context.Blogs.Add(blog);
                await _context.SaveChangesAsync();

                // Return the reserved blog object with its ID
                var response = new BlogSlugReservationResponse
                {
                    BlogID = blog.BlogID,
                    Path = blog.Path,
                    Title = blog.Title
                };

                return CreatedAtAction(nameof(GetBlog), new { id = blog.BlogID }, response);
            }
            catch (DbUpdateException ex)
            {
                // Log the exception
                _logger.LogError(ex, "Failed to reserve blog slug: {Slug}", request.Slug);

                // Handle unique constraint violations or other DB errors
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "Failed to reserve slug", error = ex.InnerException?.Message ?? ex.Message });
            }
        }

        /// <summary>
        /// Updates only the slug/path for an existing blog
        /// </summary>
        /// <param name="id">The blog ID</param>
        /// <param name="request">The slug update request</param>
        /// <returns>The updated blog</returns>
        [HttpPatch("{id}/update-slug")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<BlogSlugReservationResponse>> UpdateBlogSlug(int id, [FromBody] BlogSlugUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var blog = await _context.Blogs.FindAsync(id);
            if (blog == null)
            {
                return NotFound(new { message = $"Blog with ID {id} not found" });
            }

            // If no change, return success immediately
            if (blog.Path == request.Slug)
            {
                return Ok(new BlogSlugReservationResponse
                {
                    BlogID = blog.BlogID,
                    Path = blog.Path,
                    Title = blog.Title
                });
            }

            // Validate the slug format
            if (!ValidPathRegex.IsMatch(request.Slug))
            {
                return BadRequest("Invalid slug format.");
            }

            // Ensure the slug is unique
            if (!await IsPathUniqueAsync(request.Slug, id))
            {
                return Conflict("Slug is already in use.");
            }

            // Update the slug
            blog.Path = request.Slug;
            blog.Modified = DateTime.UtcNow;

            try
            {
                await _context.SaveChangesAsync();

                // Return the updated blog
                var response = new BlogSlugReservationResponse
                {
                    BlogID = blog.BlogID,
                    Path = blog.Path,
                    Title = blog.Title
                };

                return Ok(response);
            }
            catch (DbUpdateException ex)
            {
                // Log the exception
                _logger.LogError(ex, "Failed to update blog slug: {Slug}", request.Slug);

                // Handle unique constraint violations or other DB errors
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "Failed to update slug", error = ex.InnerException?.Message ?? ex.Message });
            }
        }

        private bool BlogExists(int id)
        {
            return (_context.Blogs?.Any(e => e.BlogID == id)).GetValueOrDefault();
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
                path = $"blog-{Guid.NewGuid().ToString().Substring(0, 8)}";
            }

            // Make sure it's unique
            var counter = 0;
            var originalPath = path;

            while (await _context.Blogs.AnyAsync(b => b.Path == path))
            {
                counter++;
                path = $"{originalPath}-{counter}";
            }

            return path;
        }

        // Add a helper method to validate paths
        private async Task<bool> IsPathUniqueAsync(string path, int? blogId = null)
        {
            var query = _context.Blogs.AsQueryable();

            if (blogId.HasValue)
            {
                query = query.Where(b => b.BlogID != blogId.Value);
            }

            return !await query.AnyAsync(b => b.Path == path);
        }
    }

    // Request models for slug operations
    public class BlogSlugReservationRequest
    {
        [Required]
        [StringLength(255, MinimumLength = 1)]
        public string Slug { get; set; }

        [StringLength(1000)]
        public string Title { get; set; }

        [StringLength(1000)]
        public string Byline { get; set; }

        public string Body { get; set; }

        [Required]
        public int UserID { get; set; }
    }

    public class BlogSlugUpdateRequest
    {
        [Required]
        [StringLength(255, MinimumLength = 1)]
        public string Slug { get; set; }
    }

    public class BlogSlugReservationResponse
    {
        public int BlogID { get; set; }
        public string Path { get; set; }
        public string Title { get; set; }
    }
}
