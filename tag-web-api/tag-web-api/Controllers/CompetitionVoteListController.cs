// <copyright file="CompetitionVoteListController.cs" company="Twisted Artists Guild">
// Copyright © Twisted Artists Guild. All rights reserved
// </copyright>

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TAGWEBAPI.Data;
using TAGWEBAPI.Models;

namespace TAGWEBAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CompetitionVoteListController : ControllerBase
{
    private readonly TAGDBContext context;

    public CompetitionVoteListController(TAGDBContext context)
    {
        this.context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CompetitionVoteList>>> Get()
    {
        return await this.context.Set<CompetitionVoteList>().ToListAsync().ConfigureAwait(false);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CompetitionVoteList>> Get(int id)
    {
        var competitionVoteList = await this.context.Set<CompetitionVoteList>().FindAsync(id).ConfigureAwait(false);
        if (competitionVoteList == null)
        {
            return this.NotFound();
        }

        return competitionVoteList;
    }

    [HttpPost]
    public async Task<ActionResult<CompetitionVoteList>> Create(CompetitionVoteList competitionVoteList)
    {
        this.context.Set<CompetitionVoteList>().Add(competitionVoteList);
        await this.context.SaveChangesAsync().ConfigureAwait(false);
        return this.CreatedAtAction(nameof(this.Get), new { id = competitionVoteList.CompetitionVoteListID }, competitionVoteList);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, CompetitionVoteList competitionVoteList)
    {
        if (id != competitionVoteList.CompetitionVoteListID)
        {
            return this.BadRequest();
        }

        this.context.Entry(competitionVoteList).State = EntityState.Modified;

        try
        {
            await this.context.SaveChangesAsync().ConfigureAwait(false);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!this.CompetitionVoteListExists(id))
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
        var competitionVoteList = await this.context.Set<CompetitionVoteList>().FindAsync(id).ConfigureAwait(false);
        if (competitionVoteList == null)
        {
            return this.NotFound();
        }

        this.context.Set<CompetitionVoteList>().Remove(competitionVoteList);
        await this.context.SaveChangesAsync().ConfigureAwait(false);

        return this.NoContent();
    }

    private bool CompetitionVoteListExists(int id)
    {
        return this.context.Set<CompetitionVoteList>().Any(e => e.CompetitionVoteListID == id);
    }
}
