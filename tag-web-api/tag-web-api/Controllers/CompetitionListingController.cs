// <copyright file="CompetitionListingController.cs" company="Twisted Artists Guild">
// Copyright © Twisted Artists Guild. All rights reserved
// </copyright>

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TAGWEBAPI.Data;
using TAGWEBAPI.Models;

namespace TAGWEBAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CompetitionListingController : ControllerBase
{
    private readonly TAGDBContext context;

    public CompetitionListingController(TAGDBContext context)
    {
        this.context = context;
    }

    [HttpGet(Name = "GetCompetitionListings")]
    public async Task<ActionResult<IEnumerable<CompetitionListing>>> Get()
    {
        return await this.context.Set<CompetitionListing>().ToListAsync().ConfigureAwait(false);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CompetitionListing>> Get(int id)
    {
        var competitionListing = await this.context.Set<CompetitionListing>().FindAsync(id).ConfigureAwait(false);
        if (competitionListing == null)
        {
            return this.NotFound();
        }

        return competitionListing;
    }

    [HttpPost]
    public async Task<ActionResult<CompetitionListing>> Create(CompetitionListing competitionListing)
    {
        this.context.Set<CompetitionListing>().Add(competitionListing);
        await this.context.SaveChangesAsync().ConfigureAwait(false);
        return this.CreatedAtAction(nameof(this.Get), new { id = competitionListing.CompetitionListingID }, competitionListing);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, CompetitionListing competitionListing)
    {
        if (id != competitionListing.CompetitionListingID)
        {
            return this.BadRequest();
        }

        this.context.Entry(competitionListing).State = EntityState.Modified;

        try
        {
            await this.context.SaveChangesAsync().ConfigureAwait(false);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!this.CompetitionListingExists(id))
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
        var competitionListing = await this.context.Set<CompetitionListing>().FindAsync(id).ConfigureAwait(false);
        if (competitionListing == null)
        {
            return this.NotFound();
        }

        this.context.Set<CompetitionListing>().Remove(competitionListing);
        await this.context.SaveChangesAsync().ConfigureAwait(false);

        return this.NoContent();
    }

    private bool CompetitionListingExists(int id)
    {
        return this.context.Set<CompetitionListing>().Any(e => e.CompetitionListingID == id);
    }
}
