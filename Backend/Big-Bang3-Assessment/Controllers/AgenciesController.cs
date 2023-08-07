using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Big_Bang3_Assessment.Data;
using Big_Bang3_Assessment.Model;
using System.Numerics;
using Microsoft.AspNetCore.Authorization;

namespace Big_Bang3_Assessment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgencyController : ControllerBase
    {
        private readonly TourismDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public AgencyController(TourismDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // GET: api/agency

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Agency>>> GetAgencies()
        {
            return await _context.agencies
                .Include(a => a.agentRegister)

                .Include(a => a.adminPost)
                .ToListAsync();
        }

        // GET: api/agency/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Agency>> GetAgency(int id)
        {
            var agency = await _context.agencies
                .Include(a => a.agentRegister)
                .Include(a => a.bookings)
                .Include(a => a.accommodationDetails)
                .FirstOrDefaultAsync(a => a.Agency_Id == id);

            if (agency == null)
            {
                return NotFound();
            }

            return agency;
        }

        // PUT: api/agency/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAgency(int id, Agency agency)
        {
            if (id != agency.Agency_Id)
            {
                return BadRequest();
            }

            _context.Entry(agency).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AgencyExists(id))
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

        // POST: api/agency
        [HttpPost]
        public async Task<Agency> CreateDoctor([FromForm] Agency doctor, IFormFile imageFile)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "uploads/images");
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                doctor.TourImagePath = fileName;
            }

            var r = _context.agentRegisters.Find(doctor.agentRegister.Agent_Id);
            doctor.agentRegister = r;
            var r1 = _context.AdminPost.Find(doctor.adminPost.id);
            doctor.adminPost = r1;

            _context.agencies.Add(doctor);
            await _context.SaveChangesAsync();

            return doctor;
        }


        // DELETE: api/agency/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAgency(int id)
        {
            var agency = await _context.agencies.FindAsync(id);
            if (agency == null)
            {
                return NotFound();
            }

            _context.agencies.Remove(agency);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/agency/adminpost/{adminPostId}
        [HttpGet("adminpost/{adminPostId}")]
        public async Task<ActionResult<IEnumerable<Agency>>> GetAgenciesByAdminPost(int adminPostId)
        {
            var agencies = await _context.agencies
                .Include(a => a.agentRegister)
                .Include(a => a.bookings)
                .Include(a => a.accommodationDetails)
                .Where(a => a.adminPost.id == adminPostId) // Filter agencies by the Admin Post ID
                .ToListAsync();

            if (agencies == null || agencies.Count == 0)
            {
                return NotFound();
            }

            return agencies;
        }
        // GET: api/agency/agentrecords/{agentId}
        [HttpGet("agentrecords/{agentId}")]
        public async Task<ActionResult<int>> GetNumberOfRecordsByAgentId(int agentId)
        {
            var numberOfRecords = await _context.agencies
                .CountAsync(a => a.agentRegister.Agent_Id == agentId);

            return numberOfRecords;
        }

        // GET: api/agency/agent/{agentId}
        [HttpGet("agent/{agentId}")]
        public async Task<ActionResult<IEnumerable<Agency>>> GetAgenciesByAgentId(int agentId)
        {
            var agencies = await _context.agencies
                .Where(a => a.agentRegister.Agent_Id == agentId) // Filter agencies by the Agent ID
                .ToListAsync();

            if (agencies == null || agencies.Count == 0)
            {
                return NotFound();
            }

            return agencies;
        }

        // GET: api/agency/days/{agencyId}
        [HttpGet("days/{agencyId}")]
        public async Task<ActionResult<int>> GetNumberOfDaysByAgencyId(int agencyId)
        {
            var agency = await _context.agencies
                .FirstOrDefaultAsync(a => a.Agency_Id == agencyId);

            if (agency == null)
            {
                return NotFound();
            }



            int numberOfDays = agency.Number_Of_Days; // Replace 'NumberOfDays' with the actual property name.

            return numberOfDays;
        }


        private bool AgencyExists(int id)
        {
            return _context.agencies.Any(a => a.Agency_Id == id);
        }
    }
}
