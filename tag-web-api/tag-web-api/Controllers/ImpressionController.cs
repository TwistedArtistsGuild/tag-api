using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TAGWEBAPI.Data;
using TAGWEBAPI.Models;

namespace TAGWEBAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ImpressionController : ControllerBase
{
    private readonly TAGDBContext _context;
    private readonly ILogger<ImpressionController> _logger;

    public ImpressionController(TAGDBContext context, ILogger<ImpressionController> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// Gets all primary impressions with counts for a specific target
    /// </summary>
    [HttpGet("primary")]
    public async Task<ActionResult<IEnumerable<PrimaryImpressionDto>>> GetPrimaryImpressions(
        [FromQuery] TargetType targetType,
        [FromQuery] int targetId)
    {
        try
        {
            // Get all primary impressions
            var primaryImpressions = await _context.PrimaryImpressions
                .OrderBy(p => p.DisplayOrder)
                .ToListAsync();

            var result = new List<PrimaryImpressionDto>();

            foreach (var impression in primaryImpressions)
            {
                int count = 0;

                // Fetch count based on target type
                if (targetType == TargetType.Listing)
                {
                    count = await _context.ListingImpressions
                        .Where(li => li.ListingId == targetId && li.ImpressionId == impression.Id)
                        .CountAsync();
                }
                else if (targetType == TargetType.Artist)
                {
                    count = await _context.ArtistImpressions
                        .Where(ai => ai.ArtistId == targetId && ai.ImpressionId == impression.Id)
                        .CountAsync();
                }

                result.Add(new PrimaryImpressionDto
                {
                    Id = impression.Id,
                    Emoji = impression.Emoji,
                    Name = impression.Name,
                    Label = impression.Label,
                    DisplayOrder = impression.DisplayOrder,
                    Count = count
                });
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching primary impressions for {TargetType} with ID {TargetId}", targetType, targetId);
            return StatusCode(500, "An error occurred while fetching impressions");
        }
    }

    /// <summary>
    /// React to a listing or artist with an impression
    /// </summary>
    [HttpPost("react")]
    public async Task<ActionResult> ReactToTarget([FromBody] ReactImpressionRequest request)
    {
        try
        {
            // Validate impression exists
            var impressionExists = await _context.PrimaryImpressions
                .AnyAsync(p => p.Id == request.ImpressionId);

            if (!impressionExists)
            {
                return BadRequest("Invalid impression ID");
            }

            if (request.TargetType == TargetType.Listing)
            {
                // Check if user already reacted with this impression
                var existingReaction = await _context.ListingImpressions
                    .FirstOrDefaultAsync(li => li.ListingId == request.TargetId
                        && li.UserId == request.UserId
                        && li.ImpressionId == request.ImpressionId);

                if (existingReaction != null)
                {
                    // Remove the reaction (toggle off)
                    _context.ListingImpressions.Remove(existingReaction);
                    await _context.SaveChangesAsync();
                    return Ok(new { message = "Reaction removed", removed = true });
                }
                else
                {
                    // Add new reaction
                    var newReaction = new ListingImpression
                    {
                        ListingId = request.TargetId,
                        ImpressionId = request.ImpressionId,
                        UserId = request.UserId,
                        CreatedAt = DateTime.UtcNow
                    };

                    _context.ListingImpressions.Add(newReaction);
                    await _context.SaveChangesAsync();
                    return Ok(new { message = "Reaction added", removed = false });
                }
            }
            else if (request.TargetType == TargetType.Artist)
            {
                // Check if user already reacted with this impression
                var existingReaction = await _context.ArtistImpressions
                    .FirstOrDefaultAsync(ai => ai.ArtistId == request.TargetId
                        && ai.UserId == request.UserId
                        && ai.ImpressionId == request.ImpressionId);

                if (existingReaction != null)
                {
                    // Remove the reaction (toggle off)
                    _context.ArtistImpressions.Remove(existingReaction);
                    await _context.SaveChangesAsync();
                    return Ok(new { message = "Reaction removed", removed = true });
                }
                else
                {
                    // Add new reaction
                    var newReaction = new ArtistImpression
                    {
                        ArtistId = request.TargetId,
                        ImpressionId = request.ImpressionId,
                        UserId = request.UserId,
                        CreatedAt = DateTime.UtcNow
                    };

                    _context.ArtistImpressions.Add(newReaction);
                    await _context.SaveChangesAsync();
                    return Ok(new { message = "Reaction added", removed = false });
                }
            }
            else
            {
                return BadRequest("Invalid target type");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error reacting to {TargetType} with ID {TargetId}", request.TargetType, request.TargetId);
            return StatusCode(500, "An error occurred while processing the reaction");
        }
    }
}