using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TAGWEBAPI.Data;
using TAGWEBAPI.Models;

namespace TAGWEBAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ImpressionController : ControllerBase
{
    private readonly TAGDBContext _context;

    public ImpressionController(TAGDBContext context)
    {
        _context = context;
    }

    // Fetch all primary impressions (for the reaction picker UI)
    [HttpGet("primary")]
    public async Task<ActionResult<IEnumerable<PrimaryImpression>>> GetPrimaryImpressions()
    {
        return await _context.PrimaryImpressions
            .OrderBy(m => m.DisplayOrder)
            .ToListAsync();
    }

    // Save/Toggle an impression against a listing
    [HttpPost("react")]
    public async Task<IActionResult> ReactToListing([FromBody] ReactionRequest request)
    {
        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userIdString)) return Unauthorized();

        var userId = Guid.Parse(userIdString);

        // Check if reaction already exists (Toggle Logic)
        var existing = await _context.ListingImpressions
            .FirstOrDefaultAsync(li => li.ListingId == request.ListingId
                                    && li.UserId == userId
                                    && li.ImpressionId == request.ImpressionId);

        if (existing != null)
        {
            _context.ListingImpressions.Remove(existing);
            await _context.SaveChangesAsync();
            return Ok(new { action = "removed" });
        }

        var newImpression = new ListingImpression
        {
            ListingId = request.ListingId,
            ImpressionId = request.ImpressionId,
            UserId = userId,
            CreatedAt = DateTime.UtcNow
        };

        _context.ListingImpressions.Add(newImpression);
        await _context.SaveChangesAsync();

        return Ok(new { action = "added" });
    }
}

public record ReactionRequest(int ListingId, int ImpressionId);