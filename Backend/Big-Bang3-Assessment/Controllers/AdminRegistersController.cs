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
    public class AdminRegistersController : ControllerBase
    {
        private readonly TourismDbContext _context;

        public AdminRegistersController(TourismDbContext context)
        {
            _context = context;
        }

        // GET: api/AdminRegisters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdminRegister>>> GetadminRegisters()
        {
          if (_context.adminRegisters == null)
          {
              return NotFound();
          }
            return await _context.adminRegisters.ToListAsync();
        }

        // GET: api/AdminRegisters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AdminRegister>> GetAdminRegister(int id)
        {
          if (_context.adminRegisters == null)
          {
              return NotFound();
          }
            var adminRegister = await _context.adminRegisters.FindAsync(id);

            if (adminRegister == null)
            {
                return NotFound();
            }

            return adminRegister;
        }

        // PUT: api/AdminRegisters/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdminRegister(int id, AdminRegister adminRegister)
        {
            if (id != adminRegister.Admin_Id)
            {
                return BadRequest();
            }

            _context.Entry(adminRegister).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdminRegisterExists(id))
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

        // POST: api/AdminRegisters
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AdminRegister>> PostAdminRegister(AdminRegister adminRegister)
        {
          if (_context.adminRegisters == null)
          {
              return Problem("Entity set 'TourismDbContext.adminRegisters'  is null.");
          }
            _context.adminRegisters.Add(adminRegister);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAdminRegister", new { id = adminRegister.Admin_Id }, adminRegister);
        }

        // DELETE: api/AdminRegisters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdminRegister(int id)
        {
            if (_context.adminRegisters == null)
            {
                return NotFound();
            }
            var adminRegister = await _context.adminRegisters.FindAsync(id);
            if (adminRegister == null)
            {
                return NotFound();
            }

            _context.adminRegisters.Remove(adminRegister);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AdminRegisterExists(int id)
        {
            return (_context.adminRegisters?.Any(e => e.Admin_Id == id)).GetValueOrDefault();
        }

        [HttpGet("UnapprovedTravelAgents")]
        public async Task<ActionResult<IEnumerable<AgentRegister>>> GetUnapprovedTravelAgents()
        {
            var unapprovedTravelAgents = await _context.agentRegisters
                .Include(ta => ta.AdminRegister)
                .Where(ta => ta.status == "Pending")
                .ToListAsync();

            return unapprovedTravelAgents;
        }

        // PUT: api/Administrators/UpdateApprovalStatus/{id}
        [HttpPut("UpdateApprovalStatus/{id}")]
        public async Task<IActionResult> UpdateApprovalStatus(int id, [FromBody] string approvalStatus)
        {
            var travelAgent = await _context.agentRegisters.FindAsync(id);
            if (travelAgent == null)
            {
                return NotFound("Travel Agent not found");
            }

            if (approvalStatus != "Approved" && approvalStatus != "Declined")
            {
                return BadRequest("Invalid approval status. It should be either 'Approved' or 'Declined'.");
            }

            travelAgent.status = approvalStatus;

            await _context.SaveChangesAsync();

            return Ok("Approval status updated successfully");
        }
    }
}
