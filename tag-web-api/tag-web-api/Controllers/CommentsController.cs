using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TAGWEBAPI.Data;
using TAGWEBAPI.Models;

namespace TAGWEBAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommentsController : ControllerBase
{
    private readonly TAGDBContext _context;
    private readonly ILogger<CommentsController> _logger;

    public CommentsController(TAGDBContext context, ILogger<CommentsController> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// Get comments for a specific target (Artist, Listing, Blog, News)
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<CommentsResponse>> GetComments(
        [FromQuery] CommentTargetType targetType,
        [FromQuery] int targetId,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20,
        [FromQuery] bool includeReplies = true)
    {
        try
        {
            // Get only top-level comments (no parent)
            var query = _context.Comments
                .Where(c => c.TargetType == targetType 
                    && c.TargetId == targetId 
                    && c.ParentCommentId == null
                    && !c.IsDeleted)
                .OrderByDescending(c => c.CreatedAt);

            var totalCount = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            var comments = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var commentDtos = new List<CommentDto>();

            foreach (var comment in comments)
            {
                var dto = await MapToCommentDto(comment, includeReplies);
                commentDtos.Add(dto);
            }

            return Ok(new CommentsResponse
            {
                Comments = commentDtos,
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize,
                TotalPages = totalPages
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching comments for {TargetType} with ID {TargetId}", targetType, targetId);
            return StatusCode(500, "An error occurred while fetching comments");
        }
    }

    /// <summary>
    /// Get replies for a specific comment
    /// </summary>
    [HttpGet("{commentId}/replies")]
    public async Task<ActionResult<CommentsResponse>> GetReplies(
        long commentId,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        try
        {
            var query = _context.Comments
                .Where(c => c.ParentCommentId == commentId && !c.IsDeleted)
                .OrderBy(c => c.CreatedAt);

            var totalCount = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            var replies = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var replyDtos = new List<CommentDto>();

            foreach (var reply in replies)
            {
                var dto = await MapToCommentDto(reply, false);
                replyDtos.Add(dto);
            }

            return Ok(new CommentsResponse
            {
                Comments = replyDtos,
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize,
                TotalPages = totalPages
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching replies for comment ID {CommentId}", commentId);
            return StatusCode(500, "An error occurred while fetching replies");
        }
    }

    /// <summary>
    /// Create a new comment or reply
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<CommentDto>> CreateComment([FromBody] CreateCommentRequest request)
    {
        try
        {
            // Validate content
            if (string.IsNullOrWhiteSpace(request.Content) || request.Content.Length > 2000)
            {
                return BadRequest("Comment content must be between 1 and 2000 characters");
            }

            // Validate user exists
            var userExists = await _context.Set<NextAuthUser>()
                .AnyAsync(u => u.Id == request.UserId);

            if (!userExists)
            {
                return BadRequest("User not found");
            }

            // If it's a reply, validate parent comment exists
            if (request.ParentCommentId.HasValue)
            {
                var parentExists = await _context.Comments
                    .AnyAsync(c => c.Id == request.ParentCommentId.Value && !c.IsDeleted);

                if (!parentExists)
                {
                    return BadRequest("Parent comment not found");
                }
            }

            var comment = new Comment
            {
                TargetType = request.TargetType,
                TargetId = request.TargetId,
                UserId = request.UserId,
                Content = request.Content.Trim(),
                ParentCommentId = request.ParentCommentId,
                CreatedAt = DateTime.UtcNow
            };

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            var commentDto = await MapToCommentDto(comment, false);

            return CreatedAtAction(nameof(GetCommentById), new { id = comment.Id }, commentDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating comment");
            return StatusCode(500, "An error occurred while creating the comment");
        }
    }

    /// <summary>
    /// Update an existing comment
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<CommentDto>> UpdateComment(long id, [FromBody] UpdateCommentRequest request)
    {
        try
        {
            if (id != request.CommentId)
            {
                return BadRequest("Comment ID mismatch");
            }

            // Validate content
            if (string.IsNullOrWhiteSpace(request.Content) || request.Content.Length > 2000)
            {
                return BadRequest("Comment content must be between 1 and 2000 characters");
            }

            var comment = await _context.Comments.FindAsync(id);

            if (comment == null)
            {
                return NotFound("Comment not found");
            }

            if (comment.IsDeleted)
            {
                return BadRequest("Cannot update deleted comment");
            }

            // Verify user owns the comment
            if (comment.UserId != request.UserId)
            {
                return Forbid();
            }

            comment.Content = request.Content.Trim();
            comment.IsEdited = true;
            comment.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            var commentDto = await MapToCommentDto(comment, false);

            return Ok(commentDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating comment ID {CommentId}", id);
            return StatusCode(500, "An error occurred while updating the comment");
        }
    }

    /// <summary>
    /// Delete a comment (soft delete)
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteComment(long id, [FromQuery] int userId)
    {
        try
        {
            var comment = await _context.Comments.FindAsync(id);

            if (comment == null)
            {
                return NotFound("Comment not found");
            }

            if (comment.IsDeleted)
            {
                return BadRequest("Comment already deleted");
            }

            // Verify user owns the comment
            if (comment.UserId != userId)
            {
                return Forbid();
            }

            comment.IsDeleted = true;
            comment.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return Ok(new { message = "Comment deleted successfully" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting comment ID {CommentId}", id);
            return StatusCode(500, "An error occurred while deleting the comment");
        }
    }

    /// <summary>
    /// Get a single comment by ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<CommentDto>> GetCommentById(long id)
    {
        try
        {
            var comment = await _context.Comments.FindAsync(id);

            if (comment == null || comment.IsDeleted)
            {
                return NotFound("Comment not found");
            }

            var commentDto = await MapToCommentDto(comment, true);

            return Ok(commentDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching comment ID {CommentId}", id);
            return StatusCode(500, "An error occurred while fetching the comment");
        }
    }

    /// <summary>
    /// Get comment count for a specific target
    /// </summary>
    [HttpGet("count")]
    public async Task<ActionResult<int>> GetCommentCount(
        [FromQuery] CommentTargetType targetType,
        [FromQuery] int targetId)
    {
        try
        {
            var count = await _context.Comments
                .Where(c => c.TargetType == targetType 
                    && c.TargetId == targetId 
                    && !c.IsDeleted)
                .CountAsync();

            return Ok(new { count });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching comment count for {TargetType} with ID {TargetId}", targetType, targetId);
            return StatusCode(500, "An error occurred while fetching comment count");
        }
    }

    private async Task<CommentDto> MapToCommentDto(Comment comment, bool includeReplies)
    {
        // Fetch user details
        var user = await _context.Set<NextAuthUser>()
            .Where(u => u.Id == comment.UserId)
            .Select(u => new UserInfoDto
            {
                Id = u.Id,
                Name = u.Name ?? "Unknown User",
                Email = u.Email,
                Image = u.Image
            })
            .FirstOrDefaultAsync();

        // Fallback if user not found
        if (user == null)
        {
            user = new UserInfoDto
            {
                Id = comment.UserId,
                Name = "Unknown User",
                Email = null,
                Image = null
            };
        }

        // Get reply count
        var replyCount = await _context.Comments
            .Where(c => c.ParentCommentId == comment.Id && !c.IsDeleted)
            .CountAsync();

        var dto = new CommentDto
        {
            Id = comment.Id,
            TargetType = comment.TargetType,
            TargetId = comment.TargetId,
            UserId = comment.UserId,
            User = user,
            Content = comment.Content,
            ParentCommentId = comment.ParentCommentId,
            IsEdited = comment.IsEdited,
            IsDeleted = comment.IsDeleted,
            CreatedAt = comment.CreatedAt,
            UpdatedAt = comment.UpdatedAt,
            ReplyCount = replyCount
        };

        // Load replies if requested
        if (includeReplies && replyCount > 0)
        {
            var replies = await _context.Comments
                .Where(c => c.ParentCommentId == comment.Id && !c.IsDeleted)
                .OrderBy(c => c.CreatedAt)
                .Take(3) // Load only first 3 replies initially
                .ToListAsync();

            dto.Replies = new List<CommentDto>();
            foreach (var reply in replies)
            {
                var replyDto = await MapToCommentDto(reply, false);
                dto.Replies.Add(replyDto);
            }
        }

        return dto;
    }
}