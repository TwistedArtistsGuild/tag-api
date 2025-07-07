// <copyright file="CompetitionWinnerListController.cs" company="Twisted Artists Guild">
// Copyright © Twisted Artists Guild. All rights reserved
// </copyright>

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TAGWEBAPI.Data;
using TAGWEBAPI.Models;

namespace TAGWEBAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CompetitionWinnerListController : ControllerBase
{
    private readonly TAGDBContext context;

    public CompetitionWinnerListController(TAGDBContext context)
    {
        this.context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CompetitionWinnerList>>> Get()
    {
        return await this.context.Set<CompetitionWinnerList>().ToListAsync().ConfigureAwait(false);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CompetitionWinnerList>> Get(int id)
    {
        var competitionWinnerList = await this.context.Set<CompetitionWinnerList>().FindAsync(id).ConfigureAwait(false);
        if (competitionWinnerList == null)
        {
            return this.NotFound();
        }

        return competitionWinnerList;
    }

    [HttpPost]
    public async Task<ActionResult<CompetitionWinnerList>> Create(CompetitionWinnerList competitionWinnerList)
    {
        this.context.Set<CompetitionWinnerList>().Add(competitionWinnerList);
        await this.context.SaveChangesAsync().ConfigureAwait(false);
        return this.CreatedAtAction(nameof(this.Get), new { id = competitionWinnerList.CompetitionWinnerListID }, competitionWinnerList);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, CompetitionWinnerList competitionWinnerList)
    {
        if (id != competitionWinnerList.CompetitionWinnerListID)
        {
            return this.BadRequest();
        }

        this.context.Entry(competitionWinnerList).State = EntityState.Modified;

        try
        {
            await this.context.SaveChangesAsync().ConfigureAwait(false);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!this.CompetitionWinnerListExists(id))
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
        var competitionWinnerList = await this.context.Set<CompetitionWinnerList>().FindAsync(id).ConfigureAwait(false);
        if (competitionWinnerList == null)
        {
            return this.NotFound();
        }

        this.context.Set<CompetitionWinnerList>().Remove(competitionWinnerList);
        await this.context.SaveChangesAsync().ConfigureAwait(false);

        return this.NoContent();
    }

    private bool CompetitionWinnerListExists(int id)
    {
        return this.context.Set<CompetitionWinnerList>().Any(e => e.CompetitionWinnerListID == id);
    }
}
