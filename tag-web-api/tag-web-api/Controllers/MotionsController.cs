using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TAGWEBAPI.Data;
using TAGWEBAPI.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TAGWEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MotionsController : ControllerBase
    {
        private readonly TAGDBContext _context;

        public MotionsController(TAGDBContext context)
        {
            _context = context;
        }

        // GET: api/Motions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Motion>>> GetMotions()
        {
            return await _context.Motions
                .Include(m => m.ProposedBy)
                .Include(m => m.SecondedBy)
                .OrderByDescending(m => m.ProposedOn)
                .ToListAsync();
        }

        // GET: api/Motions/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMotion(int id)
        {
            var motion = await _context.Motions
                .Include(m => m.ProposedBy)
                .Include(m => m.SecondedBy)
                .Include(m => m.Votes)
                    .ThenInclude(v => v.Voter)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (motion == null) return NotFound();
            return Ok(motion);
        }

        // POST: api/Motions
        // DynaFormDB will POST directly to this endpoint based on your apiurlpostfix="Motions"
        [HttpPost]
        public async Task<IActionResult> CreateMotion([FromBody] Motion motion)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            motion.ProposedOn = DateTime.UtcNow;
            motion.Status = "Proposed";
            
            _context.Motions.Add(motion);
            await _context.SaveChangesAsync();

            return Ok(motion);
        }

        // POST: api/Motions/5/second
        [HttpPost("{id}/second")]
        public async Task<IActionResult> SecondMotion(int id, [FromBody] int userId)
        {
            var motion = await _context.Motions.FindAsync(id);
            if (motion == null) return NotFound();

            if (motion.Status != "Proposed") 
                return BadRequest("Motion is already seconded or closed.");

            // RESTORED PROPOSER RESTRICTION
            if (motion.ProposedById == userId) 
                return BadRequest("Proposer cannot second their own motion.");

            motion.SecondedById = userId;
            motion.SecondedOn = DateTime.UtcNow;
            motion.Status = "Seconded"; // Opens voting

            await _context.SaveChangesAsync();
            return Ok(motion);
        }

        public class VoteRequest { public int UserId { get; set; } public string VoteValue { get; set; } }

        // POST: api/Motions/5/vote
        [HttpPost("{id}/vote")]
        public async Task<IActionResult> VoteMotion(int id, [FromBody] VoteRequest request)
        {
            var motion = await _context.Motions.FindAsync(id);
            if (motion == null || motion.Status != "Seconded") 
                return BadRequest("Motion is not open for voting.");

            var existingVote = await _context.MotionVotes
                .FirstOrDefaultAsync(v => v.MotionId == id && v.VoterId == request.UserId);

            if (existingVote != null) return BadRequest("User has already voted.");

            var vote = new MotionVote
            {
                MotionId = id,
                VoterId = request.UserId,
                VoteValue = request.VoteValue,
                VotedOn = DateTime.UtcNow
            };

            _context.MotionVotes.Add(vote);
            await _context.SaveChangesAsync();
            return Ok(vote);
        }
    }
}