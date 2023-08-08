using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Big_Bang3_Assessment.Data;
using Big_Bang3_Assessment.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Big_Bang3_Assessment.Repositories
{
    public class AccommodationRepository : IAccommodationRepository
    {
        private readonly TourismDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public AccommodationRepository(TourismDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        public async Task<IEnumerable<AccommodationDetail>> GetAllAccommodationsAsync()
        {
            return await _context.accommodations.Include(a => a.agency).ToListAsync();
        }

        public async Task<IEnumerable<AccommodationDetail>> GetAccommodationsByAgencyAsync(int agencyId)
        {
            return await _context.accommodations.Include(a => a.agency)
                                                 .Where(a => a.agency.Agency_Id == agencyId)
                                                 .ToListAsync();
        }

        public async Task<AccommodationDetail> AddAccommodationAsync(AccommodationDetail accommodation, IFormFile hotelImageFile, IFormFile placeImageFile)
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

            var agency = _context.agencies.Find(accommodation.agency.Agency_Id);
            accommodation.agency = agency;

            _context.accommodations.Add(accommodation);
            await _context.SaveChangesAsync();

            return accommodation;
        }

        public async Task<AccommodationDetail> DeleteAccommodationAsync(int id)
        {
            var accommodation = await _context.accommodations.FindAsync(id);
            if (accommodation == null)
            {
                return null;
            }

            _context.accommodations.Remove(accommodation);
            await _context.SaveChangesAsync();

            return accommodation;
        }
    }
}