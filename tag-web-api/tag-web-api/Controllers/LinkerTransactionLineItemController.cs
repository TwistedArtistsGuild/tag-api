// <copyright file="LinkerTransactionLineItemController.cs" company="Twisted Artists Guild">
// Copyright © Twisted Artists Guild. All rights reserved
// </copyright>

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TAGWEBAPI.Data;
using TAGWEBAPI.Models;

namespace TAGWEBAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class Linker_TransactionLineItemController : ControllerBase
{
    private readonly TAGDBContext context;

    public Linker_TransactionLineItemController(TAGDBContext context)
    {
        this.context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Linker_TransactionLineItem>>> Get()
    {
        return await this.context.Set<Linker_TransactionLineItem>().ToListAsync().ConfigureAwait(false);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Linker_TransactionLineItem>> Get(int id)
    {
        var linker_TransactionLineItem = await this.context.Set<Linker_TransactionLineItem>().FindAsync(id).ConfigureAwait(false);
        if (linker_TransactionLineItem == null)
        {
            return this.NotFound();
        }

        return linker_TransactionLineItem;
    }

    [HttpPost]
    public async Task<ActionResult<Linker_TransactionLineItem>> Create(Linker_TransactionLineItem linker_TransactionLineItem)
    {
        this.context.Set<Linker_TransactionLineItem>().Add(linker_TransactionLineItem);
        await this.context.SaveChangesAsync().ConfigureAwait(false);
        return this.CreatedAtAction(nameof(this.Get), new { id = linker_TransactionLineItem.Linker_TransactionLineItemID }, linker_TransactionLineItem);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Linker_TransactionLineItem linker_TransactionLineItem)
    {
        if (id != linker_TransactionLineItem.Linker_TransactionLineItemID)
        {
            return this.BadRequest();
        }

        this.context.Entry(linker_TransactionLineItem).State = EntityState.Modified;

        try
        {
            await this.context.SaveChangesAsync().ConfigureAwait(false);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!this.Linker_TransactionLineItemExists(id))
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
        var linker_TransactionLineItem = await this.context.Set<Linker_TransactionLineItem>().FindAsync(id).ConfigureAwait(false);
        if (linker_TransactionLineItem == null)
        {
            return this.NotFound();
        }

        this.context.Set<Linker_TransactionLineItem>().Remove(linker_TransactionLineItem);
        await this.context.SaveChangesAsync().ConfigureAwait(false);

        return this.NoContent();
    }

    private bool Linker_TransactionLineItemExists(int id)
    {
        return this.context.Set<Linker_TransactionLineItem>().Any(e => e.Linker_TransactionLineItemID == id);
    }
}
