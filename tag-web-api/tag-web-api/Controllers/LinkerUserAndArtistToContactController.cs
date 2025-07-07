// <copyright file="LinkerUserAndArtistToContactController.cs" company="Twisted Artists Guild">
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
    public class Linker_UserAndArtistToContactController : ControllerBase
    {
        private readonly TAGDBContext context;

        public Linker_UserAndArtistToContactController(TAGDBContext context)
        {
            this.context = context;
        }

        // GET: api/Linker_UserAndArtistToContact
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Linker_UserAndArtistToContact>>> GetLinker_UserAndArtistToContacts()
        {
            return await this.context.Set<Linker_UserAndArtistToContact>().ToListAsync().ConfigureAwait(false);
        }

        // GET: api/Linker_UserAndArtistToContact/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Linker_UserAndArtistToContact>> GetLinker_UserAndArtistToContact(int id)
        {
            var linker_UserAndArtistToContact = await this.context.Set<Linker_UserAndArtistToContact>().FindAsync(id).ConfigureAwait(false);

            if (linker_UserAndArtistToContact == null)
            {
                return this.NotFound();
            }

            return linker_UserAndArtistToContact;
        }

        // POST: api/Linker_UserAndArtistToContact
        [HttpPost]
        public async Task<ActionResult<Linker_UserAndArtistToContact>> PostLinker_UserAndArtistToContact(Linker_UserAndArtistToContact linker_UserAndArtistToContact)
        {
            this.context.Set<Linker_UserAndArtistToContact>().Add(linker_UserAndArtistToContact);
            await this.context.SaveChangesAsync().ConfigureAwait(false);

            return this.CreatedAtAction(nameof(this.GetLinker_UserAndArtistToContact), new { id = linker_UserAndArtistToContact.Linker_UserAndArtistToContactID }, linker_UserAndArtistToContact);
        }

        // PUT: api/Linker_UserAndArtistToContact/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLinker_UserAndArtistToContact(int id, Linker_UserAndArtistToContact linker_UserAndArtistToContact)
        {
            if (id != linker_UserAndArtistToContact.Linker_UserAndArtistToContactID)
            {
                return this.BadRequest();
            }

            this.context.Entry(linker_UserAndArtistToContact).State = EntityState.Modified;

            try
            {
                await this.context.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!this.Linker_UserAndArtistToContactExists(id))
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

        // DELETE: api/Linker_UserAndArtistToContact/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLinker_UserAndArtistToContact(int id)
        {
            var linker_UserAndArtistToContact = await this.context.Set<Linker_UserAndArtistToContact>().FindAsync(id).ConfigureAwait(false);
            if (linker_UserAndArtistToContact == null)
            {
                return this.NotFound();
            }

            this.context.Set<Linker_UserAndArtistToContact>().Remove(linker_UserAndArtistToContact);
            await this.context.SaveChangesAsync().ConfigureAwait(false);

            return this.NoContent();
        }

        private bool Linker_UserAndArtistToContactExists(int id)
        {
            return this.context.Set<Linker_UserAndArtistToContact>().Any(e => e.Linker_UserAndArtistToContactID == id);
        }
    }
}
