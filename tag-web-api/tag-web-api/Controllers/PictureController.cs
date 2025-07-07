// <copyright file="PictureController.cs" company="Twisted Artists Guild">
// Copyright © Twisted Artists Guild. All rights reserved
// </copyright>

namespace TAGWEBAPI.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
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

        // POST: api/Picture
        [HttpPost]
        public async Task<ActionResult<Picture>> PostPicture(Picture picture)
        {
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
    }
}
