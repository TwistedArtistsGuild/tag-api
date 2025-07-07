// <copyright file="UserController.cs" company="Twisted Artists Guild">
// Copyright © Twisted Artists Guild. All rights reserved
// </copyright>

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TAGWEBAPI.Data;
using TAGWEBAPI.Models;

namespace TAGWEBAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly TAGDBContext context;

    public UserController(TAGDBContext context)
    {
        this.context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> Get()
    {
        return await this.context.Set<User>().ToListAsync().ConfigureAwait(false);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> Get(int id)
    {
        var user = await this.context.Set<User>().FindAsync(id).ConfigureAwait(false);
        if (user == null)
        {
            return this.NotFound();
        }

        return user;
    }

    [HttpPost]
    public async Task<ActionResult<User>> Create(User user)
    {
        this.context.Set<User>().Add(user);
        await this.context.SaveChangesAsync().ConfigureAwait(false);

        var userSettings = new UserSettings { UserID = user.UserID };
        var userPrivacy = new UserPrivacy { UserID = user.UserID, HideProfileFromPublic = false, HidingFrom_NameAndDescription = "", HidingFromAbuser = false };
        var userPreference = new UserPreference { UserID = user.UserID, MetricOrImperial = "Metric", ThemePreference = "Light" };

        this.context.Set<UserSettings>().Add(userSettings);
        this.context.Set<UserPrivacy>().Add(userPrivacy);
        this.context.Set<UserPreference>().Add(userPreference);

        await this.context.SaveChangesAsync().ConfigureAwait(false);

        return this.CreatedAtAction(nameof(this.Get), new { id = user.UserID }, user);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, User user)
    {
        if (id != user.UserID)
        {
            return this.BadRequest();
        }

        this.context.Entry(user).State = EntityState.Modified;

        try
        {
            await this.context.SaveChangesAsync().ConfigureAwait(false);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!this.UserExists(id))
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
    public async Task<IActionResult> Delete(int id)
    {
        var user = await this.context.Set<User>().FindAsync(id).ConfigureAwait(false);
        if (user == null)
        {
            return this.NotFound();
        }

        this.context.Set<User>().Remove(user);
        await this.context.SaveChangesAsync().ConfigureAwait(false);

        return this.NoContent();
    }

    private bool UserExists(int id)
    {
        return this.context.Set<User>().Any(e => e.UserID == id);
    }
}
