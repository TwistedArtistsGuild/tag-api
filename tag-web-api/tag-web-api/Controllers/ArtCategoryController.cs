// <copyright file="ArtCategoryController.cs" company="Twisted Artists Guild">
// Copyright © Twisted Artists Guild. All rights reserved
// </copyright>

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TAGWEBAPI.Data;
using TAGWEBAPI.Models;
using System.Linq;

namespace TAGWEBAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ArtCategoryController : ControllerBase
{
    private readonly TAGDBContext context;

    public ArtCategoryController(TAGDBContext context)
    {
        this.context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ArtCategory>>> Get()
    {
        return await this.context.Set<ArtCategory>().ToListAsync().ConfigureAwait(false);
    }

    [HttpGet("getByID/{id}")]
    public async Task<ActionResult<ArtCategory>> Get(int id)
    {
        var artCategory = await this.context.Set<ArtCategory>().FindAsync(id).ConfigureAwait(false);
        if (artCategory == null)
        {
            return this.NotFound();
        }

        return artCategory;
    }

    [HttpGet("{category}")]
    public IActionResult GetArtCategoryByCategory(string category)
    {
        var artCategory = context.Set<ArtCategory>().FirstOrDefault(ac => ac.CategoryKey == category);
        if (artCategory == null)
        {
            return NotFound();
        }
        return Ok(artCategory);
    }

    [HttpPost]
    public async Task<ActionResult<ArtCategory>> Create(ArtCategory artCategory)
    {
        this.context.Set<ArtCategory>().Add(artCategory);
        await this.context.SaveChangesAsync().ConfigureAwait(false);
        return this.CreatedAtAction(nameof(this.Get), new { id = artCategory.ArtCategoryID }, artCategory);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ArtCategory artCategory)
    {
        if (id != artCategory.ArtCategoryID)
        {
            return this.BadRequest();
        }

        this.context.Entry(artCategory).State = EntityState.Modified;

        try
        {
            await this.context.SaveChangesAsync().ConfigureAwait(false);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!this.ArtCategoryExists(id))
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
        var artCategory = await this.context.Set<ArtCategory>().FindAsync(id).ConfigureAwait(false);
        if (artCategory == null)
        {
            return this.NotFound();
        }

        this.context.Set<ArtCategory>().Remove(artCategory);
        await this.context.SaveChangesAsync().ConfigureAwait(false);

        return this.NoContent();
    }

    private bool ArtCategoryExists(int id)
    {
        return this.context.Set<ArtCategory>().Any(e => e.ArtCategoryID == id);
    }
}
