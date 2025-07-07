// <copyright file="CompetitionController.cs" company="Twisted Artists Guild">
// Copyright © Twisted Artists Guild. All rights reserved
// </copyright>

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TAGWEBAPI.Data;
using TAGWEBAPI.Models;

namespace TAGWEBAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CompetitionController : ControllerBase
{
    private readonly TAGDBContext context;

    public CompetitionController(TAGDBContext context)
    {
        this.context = context;
    }

    [HttpGet(Name = "GetCompetitions")]
    public async Task<ActionResult<IEnumerable<Competition>>> Get()
    {
        return await this.context.Set<Competition>().ToListAsync().ConfigureAwait(false);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Competition>> Get(int id)
    {
        var competition = await this.context.Set<Competition>().FindAsync(id).ConfigureAwait(false);
        if (competition == null)
        {
            return this.NotFound();
        }

        return competition;
    }

    [HttpPost]
    public async Task<ActionResult<Competition>> Create(Competition competition)
    {
        this.context.Set<Competition>().Add(competition);
        await this.context.SaveChangesAsync().ConfigureAwait(false);
        return this.CreatedAtAction(nameof(this.Get), new { id = competition.CompetitionID }, competition);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Competition competition)
    {
        if (id != competition.CompetitionID)
        {
            return this.BadRequest();
        }

        this.context.Entry(competition).State = EntityState.Modified;

        try
        {
            await this.context.SaveChangesAsync().ConfigureAwait(false);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!this.CompetitionExists(id))
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
        var competition = await this.context.Set<Competition>().FindAsync(id).ConfigureAwait(false);
        if (competition == null)
        {
            return this.NotFound();
        }

        this.context.Set<Competition>().Remove(competition);
        await this.context.SaveChangesAsync().ConfigureAwait(false);

        return this.NoContent();
    }

    private bool CompetitionExists(int id)
    {
        return this.context.Set<Competition>().Any(e => e.CompetitionID == id);
    }
}
