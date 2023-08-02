using Big_Bang3_Assessment.Data;
using Big_Bang3_Assessment.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Big_Bang3_Assessment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccommodationController : ControllerBase // Changed the controller name to AccommodationController
    {
        private readonly TourismDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public AccommodationController(TourismDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        [HttpPost]
        public async Task<ActionResult<AccommodationDetail>> Post([FromForm] AccommodationDetail accommodation, IFormFile hotelImageFile, IFormFile placeImageFile)
        {
            if (hotelImageFile != null && hotelImageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "uploads/images");
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(hotelImageFile.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await hotelImageFile.CopyToAsync(stream);
                }

                accommodation.HotelImagePath = fileName;
            }

            if (placeImageFile != null && placeImageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "uploads/images");
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(placeImageFile.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await placeImageFile.CopyToAsync(stream);
                }

                accommodation.PlaceImagePath = fileName;
            }

            var r = _context.agencies.Find(accommodation.agency.Agency_Id);
            accommodation.agency = r;

            _context.accommodations.Add(accommodation);
            await _context.SaveChangesAsync();

            return accommodation;
        }
    }
}
