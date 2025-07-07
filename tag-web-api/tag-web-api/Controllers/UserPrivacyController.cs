// <copyright file="UserPrivacyController.cs" company="Twisted Artists Guild">
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
    public class UserPrivacyController : ControllerBase
    {
        private readonly TAGDBContext context;

        public UserPrivacyController(TAGDBContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserPrivacy>>> GetUserPrivacies()
        {
            return await this.context.Set<UserPrivacy>().ToListAsync().ConfigureAwait(false);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserPrivacy>> GetUserPrivacy(int id)
        {
            var userPrivacy = await this.context.Set<UserPrivacy>().FindAsync(id).ConfigureAwait(false);

            if (userPrivacy == null)
            {
                return this.NotFound();
            }

            return userPrivacy;
        }

        [HttpPost]
        public async Task<ActionResult<UserPrivacy>> PostUserPrivacy(UserPrivacy userPrivacy)
        {
            this.context.Set<UserPrivacy>().Add(userPrivacy);
            await this.context.SaveChangesAsync().ConfigureAwait(false);

            return this.CreatedAtAction(nameof(this.GetUserPrivacy), new { id = userPrivacy.UserPrivacyID }, userPrivacy);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserPrivacy(int id, UserPrivacy userPrivacy)
        {
            if (id != userPrivacy.UserPrivacyID)
            {
                return this.BadRequest();
            }

            this.context.Entry(userPrivacy).State = EntityState.Modified;

            try
            {
                await this.context.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!this.UserPrivacyExists(id))
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
        public async Task<IActionResult> DeleteUserPrivacy(int id)
        {
            var userPrivacy = await this.context.Set<UserPrivacy>().FindAsync(id).ConfigureAwait(false);
            if (userPrivacy == null)
            {
                return this.NotFound();
            }

            this.context.Set<UserPrivacy>().Remove(userPrivacy);
            await this.context.SaveChangesAsync().ConfigureAwait(false);

            return this.NoContent();
        }

        private bool UserPrivacyExists(int id)
        {
            return this.context.Set<UserPrivacy>().Any(e => e.UserPrivacyID == id);
        }
    }
}
