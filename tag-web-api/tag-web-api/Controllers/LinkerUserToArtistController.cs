// <copyright file="UserToArtistController.cs" company="Twisted Artists Guild">
// Copyright © Twisted Artists Guild. All rights reserved
// </copyright>

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TAGWEBAPI.Models;

namespace TAGWEBAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class Linker_UserToArtistController : ControllerBase
{
    private readonly DbContext context;

    public Linker_UserToArtistController(DbContext context) // Updated constructor name
    {
        this.context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Linker_UserToArtist>>> GetUserToArtists()
    {
        return await this.context.Set<Linker_UserToArtist>().ToListAsync().ConfigureAwait(false);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Linker_UserToArtist>> GetUserToArtist(int id)
    {
        var userToArtist = await this.context.Set<Linker_UserToArtist>().FindAsync(id).ConfigureAwait(false);

        if (userToArtist == null)
        {
            return this.NotFound();
        }

        return userToArtist;
    }

    [HttpPost]
    public async Task<ActionResult<Linker_UserToArtist>> PostUserToArtist(Linker_UserToArtist userToArtist) // Updated parameter type
    {
        this.context.Set<Linker_UserToArtist>().Add(userToArtist);
        await this.context.SaveChangesAsync().ConfigureAwait(false);

        return this.CreatedAtAction(nameof(this.GetUserToArtist), new { id = userToArtist.Linker_UserToArtistID }, userToArtist);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutUserToArtist(int id, Linker_UserToArtist userToArtist) // Updated parameter type
    {
        if (id != userToArtist.Linker_UserToArtistID)
        {
            return this.BadRequest();
        }

        this.context.Entry(userToArtist).State = EntityState.Modified;

        try
        {
            await this.context.SaveChangesAsync().ConfigureAwait(false);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!this.UserToArtistExists(id))
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
    public async Task<IActionResult> DeleteUserToArtist(int id)
    {
        var userToArtist = await this.context.Set<Linker_UserToArtist>().FindAsync(id).ConfigureAwait(false);
        if (userToArtist == null)
        {
            return this.NotFound();
        }

        this.context.Set<Linker_UserToArtist>().Remove(userToArtist);
        await this.context.SaveChangesAsync().ConfigureAwait(false);

        return this.NoContent();
    }

    private bool UserToArtistExists(int id)
    {
        return this.context.Set<Linker_UserToArtist>().Any(e => e.Linker_UserToArtistID == id);
    }
}
