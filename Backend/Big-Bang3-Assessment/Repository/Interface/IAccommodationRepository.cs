using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Big_Bang3_Assessment.Model;
using Microsoft.AspNetCore.Http;

namespace Big_Bang3_Assessment.Repositories
{
    public interface IAccommodationRepository
    {
        Task<IEnumerable<AccommodationDetail>> GetAllAccommodationsAsync();
        Task<IEnumerable<AccommodationDetail>> GetAccommodationsByAgencyAsync(int agencyId);
        Task<AccommodationDetail> AddAccommodationAsync(AccommodationDetail accommodation, IFormFile hotelImageFile, IFormFile placeImageFile);
        Task<AccommodationDetail> DeleteAccommodationAsync(int id);
    }
}