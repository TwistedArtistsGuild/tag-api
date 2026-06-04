// <copyright file="LinkerEntityToContactController.cs" company="Twisted Artists Guild">
// Copyright © Twisted Artists Guild. All rights reserved
// </copyright>

namespace TAGWEBAPI.Controllers.Contact
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using TAGWEBAPI.Data;
    using TAGWEBAPI.Models;

    [Route("api/[controller]")]
    [ApiController]
    public class LinkerEntityToContactController : ControllerBase
    {
        private readonly TAGDBContext context;

        public LinkerEntityToContactController(TAGDBContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Linker_EntityToContact>>> GetLinks()
        {
            return await this.context.Set<Linker_EntityToContact>()
                .Include(l => l.Contact)
                .OrderBy(l => l.DisplayOrder)
                .ThenBy(l => l.Linker_EntityToContactID)
                .ToListAsync()
                .ConfigureAwait(false);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Linker_EntityToContact>> GetLink(int id)
        {
            var link = await this.context.Set<Linker_EntityToContact>()
                .Include(l => l.Contact)
                .FirstOrDefaultAsync(l => l.Linker_EntityToContactID == id)
                .ConfigureAwait(false);

            if (link == null)
            {
                return this.NotFound();
            }

            return link;
        }

        [HttpPost]
        public async Task<ActionResult<Linker_EntityToContact>> PostLink(LinkerCreateRequest request)
        {
            var contact = await this.context.Set<Contact>()
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.ContactID == request.ContactID)
                .ConfigureAwait(false);

            if (contact == null)
            {
                return this.BadRequest($"Contact {request.ContactID} was not found.");
            }

            var link = new Linker_EntityToContact
            {
                ContactID = request.ContactID,
                UserID = request.UserID,
                ArtistID = request.ArtistID,
                VenueID = request.VenueID,
                VendorID = request.VendorID,
                DisplayOrder = request.DisplayOrder,
                Scope = ContactScope.Secondary,
            };

            var validationError = ValidateEntityTarget(link);
            if (validationError != null)
            {
                return this.BadRequest(validationError);
            }

            this.context.Set<Linker_EntityToContact>().Add(link);
            await this.context.SaveChangesAsync().ConfigureAwait(false);

            return this.CreatedAtAction(nameof(this.GetLink), new { id = link.Linker_EntityToContactID }, link);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutLink(int id, Linker_EntityToContact link)
        {
            if (id != link.Linker_EntityToContactID)
            {
                return this.BadRequest();
            }

            var validationError = ValidateEntityTarget(link);
            if (validationError != null)
            {
                return this.BadRequest(validationError);
            }

            var contact = await this.context.Set<Contact>()
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.ContactID == link.ContactID)
                .ConfigureAwait(false);

            if (contact == null)
            {
                return this.BadRequest($"Contact {link.ContactID} was not found.");
            }

            if (!Enum.IsDefined(typeof(ContactScope), link.Scope))
            {
                link.Scope = ContactScope.Secondary;
            }

            this.context.Entry(link).State = EntityState.Modified;

            try
            {
                await this.context.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!this.LinkExists(id))
                {
                    return this.NotFound();
                }

                throw;
            }

            return this.NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLink(int id)
        {
            var link = await this.context.Set<Linker_EntityToContact>().FindAsync(id).ConfigureAwait(false);
            if (link == null)
            {
                return this.NotFound();
            }

            this.context.Set<Linker_EntityToContact>().Remove(link);
            await this.context.SaveChangesAsync().ConfigureAwait(false);

            return this.NoContent();
        }

        private static string? ValidateEntityTarget(Linker_EntityToContact link)
        {
            var ownerCount = 0;
            if (link.UserID.HasValue)
            {
                ownerCount++;
            }

            if (link.ArtistID.HasValue)
            {
                ownerCount++;
            }

            if (link.VenueID.HasValue)
            {
                ownerCount++;
            }

            if (link.VendorID.HasValue)
            {
                ownerCount++;
            }

            return ownerCount == 1 ? null : "Exactly one owner must be set: UserID, ArtistID, VenueID, or VendorID.";
        }

        private bool LinkExists(int id)
        {
            return this.context.Set<Linker_EntityToContact>().Any(e => e.Linker_EntityToContactID == id);
        }
    }

    /// <summary>
    /// Request DTO for creating a new entity-to-contact link.
    /// </summary>
    public sealed class LinkerCreateRequest
    {
        public int ContactID { get; set; }

        public int? UserID { get; set; }

        public int? ArtistID { get; set; }

        public int? VenueID { get; set; }

        public int? VendorID { get; set; }

        public int DisplayOrder { get; set; }
    }
}