// <copyright file="PictureController.cs" company="Twisted Artists Guild">
// Copyright © Twisted Artists Guild. All rights reserved
// </copyright>

namespace TAGWEBAPI.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System;
    using TAGWEBAPI.Data;
    using TAGWEBAPI.Models;

    [Route("api/[controller]")]
    [ApiController]
    public class PictureController : ControllerBase
    {
        private readonly TAGDBContext context;

        public PictureController(TAGDBContext context)
        {
            this.context = context;
        }

        // GET: api/Picture
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Picture>>> GetPictures()
        {
            return await this.context.Set<Picture>().ToListAsync().ConfigureAwait(false);
        }

        // GET: api/Picture/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Picture>> GetPicture(int id)
        {
            var picture = await this.context.Set<Picture>().FindAsync(id).ConfigureAwait(false);

            if (picture == null)
            {
                return this.NotFound();
            }

            return picture;
        }

        // GET: api/Picture/by-url?url=https://...
        [HttpGet("by-url")]
        public async Task<ActionResult<Picture>> GetPictureByUrl([FromQuery] string url)
        {
            var normalizedUrl = NormalizeUrl(url);
            if (string.IsNullOrWhiteSpace(normalizedUrl))
            {
                return this.BadRequest(new { message = "url is required" });
            }

            var picture = await this.context.Set<Picture>()
                .AsNoTracking()
                .FirstOrDefaultAsync(p => (p.NormalizedURL ?? p.URL ?? string.Empty).ToLower() == normalizedUrl)
                .ConfigureAwait(false);

            if (picture == null)
            {
                return this.NotFound();
            }

            return picture;
        }

        // POST: api/Picture
        [HttpPost]
        public async Task<ActionResult<Picture>> PostPicture(Picture picture)
        {
            var normalizedUrl = NormalizeUrl(picture.NormalizedURL ?? picture.URL);
            if (!string.IsNullOrWhiteSpace(normalizedUrl))
            {
                picture.NormalizedURL = normalizedUrl;

                var existing = await this.context.Set<Picture>()
                    .FirstOrDefaultAsync(p => (p.NormalizedURL ?? p.URL ?? string.Empty).ToLower() == normalizedUrl)
                    .ConfigureAwait(false);

                if (existing != null)
                {
                    existing.AltText = picture.AltText ?? existing.AltText;
                    existing.ArtistID = picture.ArtistID ?? existing.ArtistID;
                    existing.Context = picture.Context ?? existing.Context;
                    existing.Description = picture.Description ?? existing.Description;
                    existing.EmbedURL = picture.EmbedURL ?? existing.EmbedURL;
                    existing.Height = picture.Height ?? existing.Height;
                    existing.Path = picture.Path ?? existing.Path;
                    existing.ThumbnailHeight = picture.ThumbnailHeight ?? existing.ThumbnailHeight;
                    existing.ThumbnailURL = picture.ThumbnailURL ?? existing.ThumbnailURL;
                    existing.ThumbnailWidth = picture.ThumbnailWidth ?? existing.ThumbnailWidth;
                    existing.Title = picture.Title ?? existing.Title;
                    existing.URL = picture.URL ?? existing.URL;
                    existing.Byline = picture.Byline ?? existing.Byline;
                    existing.UserID = picture.UserID ?? existing.UserID;
                    existing.Width = picture.Width ?? existing.Width;
                    existing.Updated = picture.Updated == default ? DateTime.UtcNow : picture.Updated;

                    await this.context.SaveChangesAsync().ConfigureAwait(false);
                    return this.Ok(existing);
                }
            }

            this.context.Set<Picture>().Add(picture);
            await this.context.SaveChangesAsync().ConfigureAwait(false);

            return this.CreatedAtAction("GetPicture", new { id = picture.PictureID }, picture);
        }

        // PUT: api/Picture/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPicture(int id, Picture picture)
        {
            if (id != picture.PictureID)
            {
                return this.BadRequest();
            }

            this.context.Entry(picture).State = EntityState.Modified;

            try
            {
                await this.context.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!this.PictureExists(id))
                {
                    return this.NotFound();
                }
                else
                {
                    throw;
                }
            }

            return this.NoContent();
        }

        // DELETE: api/Picture/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePicture(int id)
        {
            var picture = await this.context.Set<Picture>().FindAsync(id).ConfigureAwait(false);
            if (picture == null)
            {
                return this.NotFound();
            }

            this.context.Set<Picture>().Remove(picture);
            await this.context.SaveChangesAsync().ConfigureAwait(false);

            return this.NoContent();
        }

        private bool PictureExists(int id)
        {
            return this.context.Set<Picture>().Any(e => e.PictureID == id);
        }

        private static string NormalizeUrl(string? value)
        {
            return string.IsNullOrWhiteSpace(value)
                ? string.Empty
                : value.Trim().ToLowerInvariant();
        }
    }
}
