// <copyright file="UserController.cs" company="Twisted Artists Guild">
// Copyright © Twisted Artists Guild. All rights reserved
// </copyright>

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using TAGWEBAPI.Data;
using TAGWEBAPI.Models;

namespace TAGWEBAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly TAGDBContext context;
    private static readonly Regex ValidUsernameRegex = new(@"^[a-z0-9](?:[a-z0-9\-]*[a-z0-9])?$", RegexOptions.Compiled);

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

        await this.CreateDefaultPreferenceRowsAsync(user.UserID).ConfigureAwait(false);

        return this.CreatedAtAction(nameof(this.Get), new { id = user.UserID }, user);
    }

    [HttpGet("check-slug/{slug}")]
    public async Task<ActionResult<object>> CheckUserSlug(string slug, [FromQuery] int? excludeId = null)
    {
        var normalizedSlug = NormalizeSlug(slug);

        if (!ValidUsernameRegex.IsMatch(normalizedSlug))
        {
            return this.BadRequest(new { available = false, message = "Invalid slug format." });
        }

        var available = await this.IsSlugUniqueAsync(normalizedSlug, excludeId).ConfigureAwait(false);
        if (!available)
        {
            return this.Conflict(new { available = false, message = "Slug is already in use." });
        }

        return this.Ok(new { available = true, slug = normalizedSlug });
    }

    [HttpPost("reserve-slug")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<UserSlugReservationResponse>> ReserveUserSlug([FromBody] UserSlugReservationRequest request)
    {
        if (!ModelState.IsValid)
        {
            return this.BadRequest(ModelState);
        }

        var normalizedSlug = NormalizeSlug(request.Slug);
        if (!ValidUsernameRegex.IsMatch(normalizedSlug))
        {
            return this.BadRequest("Invalid slug format.");
        }

        var normalizedEmail = NormalizeEmail(request.Email);
        if (string.IsNullOrWhiteSpace(normalizedEmail))
        {
            return this.BadRequest("Email is required.");
        }

        if (!await this.IsSlugUniqueAsync(normalizedSlug).ConfigureAwait(false))
        {
            return this.Conflict("Slug is already in use.");
        }

        var user = new User
        {
            Username = normalizedSlug,
            PreferredName = string.IsNullOrWhiteSpace(request.Title) ? normalizedSlug : request.Title.Trim(),
            EmailOne = normalizedEmail,
            Joined = DateTime.UtcNow,
        };

        this.context.Set<User>().Add(user);
        await this.context.SaveChangesAsync().ConfigureAwait(false);
        await this.CreateDefaultPreferenceRowsAsync(user.UserID).ConfigureAwait(false);

        var response = new UserSlugReservationResponse
        {
            UserID = user.UserID,
            Username = user.Username,
            Title = user.PreferredName,
            Email = user.EmailOne,
        };

        return this.CreatedAtAction(nameof(this.Get), new { id = user.UserID }, response);
    }

    [HttpPatch("{id}/update-slug")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<UserSlugReservationResponse>> UpdateUserSlug(int id, [FromBody] UserSlugUpdateRequest request)
    {
        if (!ModelState.IsValid)
        {
            return this.BadRequest(ModelState);
        }

        var user = await this.context.Set<User>().FindAsync(id).ConfigureAwait(false);
        if (user == null)
        {
            return this.NotFound(new { message = $"User with ID {id} not found" });
        }

        var normalizedSlug = NormalizeSlug(request.Slug);
        if (!ValidUsernameRegex.IsMatch(normalizedSlug))
        {
            return this.BadRequest("Invalid slug format.");
        }

        if (!await this.IsSlugUniqueAsync(normalizedSlug, id).ConfigureAwait(false))
        {
            return this.Conflict("Slug is already in use.");
        }

        user.Username = normalizedSlug;
        if (!string.IsNullOrWhiteSpace(request.Title))
        {
            user.PreferredName = request.Title.Trim();
        }

        await this.context.SaveChangesAsync().ConfigureAwait(false);

        return this.Ok(new UserSlugReservationResponse
        {
            UserID = user.UserID,
            Username = user.Username,
            Title = user.PreferredName,
            Email = user.EmailOne,
        });
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

    private static string NormalizeSlug(string slug)
    {
        return string.IsNullOrWhiteSpace(slug) ? string.Empty : slug.Trim().ToLowerInvariant();
    }

    private static string NormalizeEmail(string email)
    {
        return string.IsNullOrWhiteSpace(email) ? string.Empty : email.Trim().ToLowerInvariant();
    }

    private async Task<bool> IsSlugUniqueAsync(string slug, int? excludeUserId = null)
    {
        var query = this.context.Set<User>().AsQueryable();

        if (excludeUserId.HasValue)
        {
            query = query.Where(u => u.UserID != excludeUserId.Value);
        }

        return !await query.AnyAsync(u => u.Username.ToLower() == slug).ConfigureAwait(false);
    }

    private async Task CreateDefaultPreferenceRowsAsync(int userId)
    {
        var userSettings = new UserSettings { UserID = userId };
        var userPrivacy = new UserPrivacy { UserID = userId, HideProfileFromPublic = false, HidingFrom_NameAndDescription = "", HidingFromAbuser = false };
        var userPreference = new UserPreference { UserID = userId, MetricOrImperial = "Metric", ThemePreference = "Light" };

        this.context.Set<UserSettings>().Add(userSettings);
        this.context.Set<UserPrivacy>().Add(userPrivacy);
        this.context.Set<UserPreference>().Add(userPreference);

        await this.context.SaveChangesAsync().ConfigureAwait(false);
    }
}

public class UserSlugReservationRequest
{
    [Required]
    public string Slug { get; set; }

    public string? Title { get; set; }

    [Required]
    public string Email { get; set; }
}

public class UserSlugUpdateRequest
{
    [Required]
    public string Slug { get; set; }

    public string? Title { get; set; }
}

public class UserSlugReservationResponse
{
    public int UserID { get; set; }

    public string Username { get; set; }

    public string? Title { get; set; }

    public string Email { get; set; }
}
