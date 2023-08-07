using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Big_Bang3_Assessment.Data;
using Big_Bang3_Assessment.Model;
using System.Security.Cryptography;
using System.Text;
using Big_Bang3_Assessment.Dto;

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

        [HttpPost("register")]
        public async Task<ActionResult<AdminRegister>> Register(AdminDto request)
        {
            CreatePasswordHash(request.Admin_Password, out byte[] passwordHash, out byte[] passwordSalt);

            var adminRegister = new AdminRegister
            {
                Admin_Name = request.Admin_Name,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            _context.adminRegisters.Add(adminRegister);
            await _context.SaveChangesAsync();

            return Ok(adminRegister);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(AdminDto request)
        {
            var user = await _context.adminRegisters.FirstOrDefaultAsync(u => u.Admin_Name == request.Admin_Name);

            if (user == null)
            {
                return BadRequest("User not found.");
            }

            if (!VerifyPasswordHash(request.Admin_Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("Wrong password.");
            }

            return Ok();
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

    }
}
