using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Big_Bang3_Assessment.Data;
using Big_Bang3_Assessment.Model;
using Microsoft.Extensions.Hosting;

namespace Big_Bang3_Assessment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminPostsController : ControllerBase
    {
        private readonly TourismDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        public AdminPostsController(TourismDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // GET: api/AdminPosts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdminPost>>> GetAdminPost()
        {
            return await _context.AdminPost.ToListAsync();
        }

        // GET: api/AdminPosts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AdminPost>> GetAdminPost(int id)
        {
            var adminPost = await _context.AdminPost.FindAsync(id);

            if (adminPost == null)
            {
                return NotFound();
            }

            return adminPost;
        }

        // PUT: api/AdminPosts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdminPost(int id, AdminPost adminPost)
        {
            if (id != adminPost.id)
            {
                return BadRequest();
            }

            _context.Entry(adminPost).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdminPostExists(id))
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

        // POST: api/AdminPosts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<AdminPost> PostImages([FromForm] AdminPost doctor, IFormFile imageFile)
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

                doctor.PlaceImagePath = fileName;
            }

            var r = _context.adminRegisters.Find(doctor.adminRegister.Admin_Id);
            doctor.adminRegister = r;



            _context.AdminPost.Add(doctor);
            await _context.SaveChangesAsync();

            return doctor;
        }

        // DELETE: api/AdminPosts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdminPost(int id)
        {
            var adminPost = await _context.AdminPost.FindAsync(id);
            if (adminPost == null)
            {
                return NotFound();
            }

            _context.AdminPost.Remove(adminPost);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AdminPostExists(int id)
        {
            return _context.AdminPost.Any(e => e.id == id);
        }
    }
}
