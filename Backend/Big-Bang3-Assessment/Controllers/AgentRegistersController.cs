using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Big_Bang3_Assessment.Data;
using Big_Bang3_Assessment.Model;

namespace Big_Bang3_Assessment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentRegistersController : ControllerBase
    {
        private readonly TourismDbContext _context;

        public AgentRegistersController(TourismDbContext context)
        {
            _context = context;
        }

        // GET: api/AgentRegisters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AgentRegister>>> GetagentRegisters()
        {
          if (_context.agentRegisters == null)
          {
              return NotFound();
          }
            return await _context.agentRegisters.ToListAsync();
        }

        // GET: api/AgentRegisters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AgentRegister>> GetAgentRegister(int id)
        {
          if (_context.agentRegisters == null)
          {
              return NotFound();
          }
            var agentRegister = await _context.agentRegisters.FindAsync(id);

            if (agentRegister == null)
            {
                return NotFound();
            }

            return agentRegister;
        }

        // PUT: api/AgentRegisters/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAgentRegister(int id, AgentRegister agentRegister)
        {
            if (id != agentRegister.Agent_Id)
            {
                return BadRequest();
            }

            _context.Entry(agentRegister).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AgentRegisterExists(id))
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

        // POST: api/AgentRegisters
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AgentRegister>> PostAgentRegister(AgentRegister agentRegister)
        {
          if (_context.agentRegisters == null)
          {
              return Problem("Entity set 'TourismDbContext.agentRegisters'  is null.");
          }
            _context.agentRegisters.Add(agentRegister);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAgentRegister", new { id = agentRegister.Agent_Id }, agentRegister);
        }

        // DELETE: api/AgentRegisters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAgentRegister(int id)
        {
            if (_context.agentRegisters == null)
            {
                return NotFound();
            }
            var agentRegister = await _context.agentRegisters.FindAsync(id);
            if (agentRegister == null)
            {
                return NotFound();
            }

            _context.agentRegisters.Remove(agentRegister);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("Approved")]
        public async Task<ActionResult<IEnumerable<AgentRegister>>> GetApprovedTravelAgents()
        {
            var approvedTravelAgents = await _context.agentRegisters
                .Include(ta => ta.AdminRegister)
                .Where(ta => ta.status == "Approved")
                .ToListAsync();

            return approvedTravelAgents;
        }

        private bool AgentRegisterExists(int id)
        {
            return (_context.agentRegisters?.Any(e => e.Agent_Id == id)).GetValueOrDefault();
        }
    }
}
