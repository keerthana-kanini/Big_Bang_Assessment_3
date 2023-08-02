using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Big_Bang3_Assessment.Data;
using Big_Bang3_Assessment.Model;
using Microsoft.AspNetCore.Authorization;

namespace Big_Bang3_Assessment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedBacksController : ControllerBase
    {
        private readonly TourismDbContext _context;

        public FeedBacksController(TourismDbContext context)
        {
            _context = context;
        }

        // GET: api/FeedBacks
        [Authorize(Roles ="Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FeedBack>>> GetfeedBacks()
        {
            return await _context.feedBacks.ToListAsync();
        }

        // GET: api/FeedBacks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FeedBack>> GetFeedBack(int id)
        {
            var feedBack = await _context.feedBacks.FindAsync(id);

            if (feedBack == null)
            {
                return NotFound();
            }

            return feedBack;
        }

        // PUT: api/FeedBacks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFeedBack(int id, FeedBack feedBack)
        {
            if (id != feedBack.FeedBack_id)
            {
                return BadRequest();
            }

            _context.Entry(feedBack).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FeedBackExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/FeedBacks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FeedBack>> PostFeedBack(FeedBack feedBack)
        {
            var feedback = await _context.users.FindAsync(feedBack.user.User_Id);
            if (feedback == null)
            {
                return BadRequest("invalid assessment id");
            }
            feedBack.user = feedback;

            _context.feedBacks.Add(feedBack);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFeedBack", new { id = feedBack.FeedBack_id }, feedBack);
        }

        // DELETE: api/FeedBacks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFeedBack(int id)
        {
            var feedBack = await _context.feedBacks.FindAsync(id);
            if (feedBack == null)
            {
                return NotFound();
            }

            _context.feedBacks.Remove(feedBack);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FeedBackExists(int id)
        {
            return _context.feedBacks.Any(e => e.FeedBack_id == id);
        }
    }
}
