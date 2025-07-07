// <copyright file="VenueController.cs" company="Twisted Artists Guild">
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
    public class VenueController : ControllerBase
    {
        private readonly TAGDBContext context;

        public VenueController(TAGDBContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Venue>>> GetVenues()
        {
            return await this.context.Set<Venue>().ToListAsync().ConfigureAwait(false);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Venue>> GetVenue(int id)
        {
            var venue = await this.context.Set<Venue>().FindAsync(id).ConfigureAwait(false);

            if (venue == null)
            {
                return this.NotFound();
            }

            return venue;
        }

        [HttpPost]
        public async Task<ActionResult<Venue>> PostVenue(Venue venue)
        {
            this.context.Set<Venue>().Add(venue);
            await this.context.SaveChangesAsync().ConfigureAwait(false);

            return this.CreatedAtAction(nameof(this.GetVenue), new { id = venue.VenueID }, venue);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutVenue(int id, Venue venue)
        {
            if (id != venue.VenueID)
            {
                return this.BadRequest();
            }

            this.context.Entry(venue).State = EntityState.Modified;

            try
            {
                await this.context.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!this.VenueExists(id))
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVenue(int id)
        {
            var venue = await this.context.Set<Venue>().FindAsync(id).ConfigureAwait(false);
            if (venue == null)
            {
                return this.NotFound();
            }

            this.context.Set<Venue>().Remove(venue);
            await this.context.SaveChangesAsync().ConfigureAwait(false);

            return this.NoContent();
        }

        private bool VenueExists(int id)
        {
            return this.context.Set<Venue>().Any(e => e.VenueID == id);
        }
    }
}
