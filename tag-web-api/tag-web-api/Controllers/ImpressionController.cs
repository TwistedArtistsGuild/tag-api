using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;
using TAGWEBAPI.Data;
using TAGWEBAPI.Models;
using TAGWEBAPI.Hubs;

namespace TAGWEBAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ImpressionController : ControllerBase
{
    private readonly TAGDBContext _context;
    private readonly ILogger<ImpressionController> _logger;
    private readonly IHubContext<MessagingHub> _hubContext;

    public ImpressionController(
        TAGDBContext context, 
        ILogger<ImpressionController> logger,
        IHubContext<MessagingHub> hubContext)
    {
        _context = context;
        _logger = logger;
        _hubContext = hubContext;
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
                else if (targetType == TargetType.Comment)
                {
                    count = await _context.CommentImpressions
                        .Where(ci => ci.CommentId == targetId && ci.ImpressionId == impression.Id)
                        .CountAsync();
                }
                else if (targetType == TargetType.Blog)
                {
                    count = await _context.BlogImpressions
                        .Where(bi => bi.BlogId == targetId && bi.ImpressionId == impression.Id)
                        .CountAsync();
                }
                else if (targetType == TargetType.Message)
                {
                    count = await _context.MessageImpressions
                        .Where(mi => mi.MessageId == targetId && mi.ImpressionId == impression.Id)
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
    /// React to a listing, artist, or comment with an impression
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
            else if (request.TargetType == TargetType.Comment)
            {
                // Check if user already reacted with this impression
                var existingReaction = await _context.CommentImpressions
                    .FirstOrDefaultAsync(ci => ci.CommentId == request.TargetId 
                        && ci.UserId == request.UserId 
                        && ci.ImpressionId == request.ImpressionId);

                if (existingReaction != null)
                {
                    // Remove the reaction (toggle off)
                    _context.CommentImpressions.Remove(existingReaction);
                    await _context.SaveChangesAsync();
                    return Ok(new { message = "Reaction removed", removed = true });
                }
                else
                {
                    // Add new reaction
                    var newReaction = new CommentImpression
                    {
                        CommentId = request.TargetId,
                        ImpressionId = request.ImpressionId,
                        UserId = request.UserId,
                        CreatedAt = DateTime.UtcNow
                    };

                    _context.CommentImpressions.Add(newReaction);
                    await _context.SaveChangesAsync();
                    return Ok(new { message = "Reaction added", removed = false });
                }
            }
            else if (request.TargetType == TargetType.Blog)
            {
                // Check if user already reacted with this impression
                var existingReaction = await _context.BlogImpressions
                    .FirstOrDefaultAsync(bi => bi.BlogId == request.TargetId 
                        && bi.UserId == request.UserId 
                        && bi.ImpressionId == request.ImpressionId);

                if (existingReaction != null)
                {
                    // Remove the reaction (toggle off)
                    _context.BlogImpressions.Remove(existingReaction);
                    await _context.SaveChangesAsync();
                    return Ok(new { message = "Reaction removed", removed = true });
                }
                else
                {
                    // Add new reaction
                    var newReaction = new BlogImpression
                    {
                        BlogId = request.TargetId,
                        ImpressionId = request.ImpressionId,
                        UserId = request.UserId,
                        CreatedAt = DateTime.UtcNow
                    };

                    _context.BlogImpressions.Add(newReaction);
                    await _context.SaveChangesAsync();
                    return Ok(new { message = "Reaction added", removed = false });
                }
            }
            else if (request.TargetType == TargetType.Message)
            {
                // Check if user already reacted with this impression
                var existingReaction = await _context.MessageImpressions
                    .FirstOrDefaultAsync(mi => mi.MessageId == request.TargetId
                        && mi.UserId == request.UserId
                        && mi.ImpressionId == request.ImpressionId);

                bool wasRemoved = false;

                if (existingReaction != null)
                {
                    // Remove the reaction (toggle off)
                    _context.MessageImpressions.Remove(existingReaction);
                    await _context.SaveChangesAsync();
                    wasRemoved = true;
                }
                else
                {
                    // Add new reaction
                    var newReaction = new MessageImpression
                    {
                        MessageId = request.TargetId,
                        ImpressionId = request.ImpressionId,
                        UserId = request.UserId,
                        CreatedAt = DateTime.UtcNow
                    };

                    _context.MessageImpressions.Add(newReaction);
                    await _context.SaveChangesAsync();
                }

                // 🔴 SIGNALR BROADCAST - Only for Message type
                try
                {
                    // Get the message to find its conversation
                    var message = await _context.Messages
                        .AsNoTracking()
                        .FirstOrDefaultAsync(m => m.MessageID == request.TargetId);

                    if (message?.ConversationId != null)
                    {
                        // Get updated impression counts for this message
                        var impressionCounts = await _context.MessageImpressions
                            .Where(mi => mi.MessageId == request.TargetId)
                            .Include(mi => mi.Impression)
                            .GroupBy(mi => mi.ImpressionId)
                            .Select(g => new
                            {
                                impressionId = g.Key,
                                emoji = g.First().Impression.Emoji,
                                name = g.First().Impression.Name,
                                count = g.Count()
                            })
                            .ToListAsync();

                        // Get impression details
                        var impression = await _context.PrimaryImpressions
                            .FirstOrDefaultAsync(p => p.Id == request.ImpressionId);

                        // Broadcast to all participants in the conversation
                        await _hubContext.Clients.Group($"conversation-{message.ConversationId}")
                            .SendAsync("MessageImpressionUpdated", new
                            {
                                messageId = request.TargetId,
                                userId = request.UserId,
                                impressionId = request.ImpressionId,
                                emoji = impression?.Emoji,
                                name = impression?.Name,
                                removed = wasRemoved,
                                impressions = impressionCounts,
                                timestamp = DateTime.UtcNow
                            });

                        _logger.LogInformation(
                            "Broadcasted message impression update for Message {MessageId} in Conversation {ConversationId}", 
                            request.TargetId, 
                            message.ConversationId);
                    }
                }
                catch (Exception signalREx)
                {
                    // Log but don't fail the request if SignalR broadcast fails
                    _logger.LogError(signalREx, "Failed to broadcast message impression via SignalR");
                }

                return Ok(new { 
                    message = wasRemoved ? "Reaction removed" : "Reaction added", 
                    removed = wasRemoved 
                });
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