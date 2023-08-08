using Big_Bang3_Assessment.Data;
using Big_Bang3_Assessment.Model;
using Big_Bang3_Assessment.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Big_Bang3_Assessment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccommodationController : ControllerBase
    {
        private readonly IAccommodationRepository _accommodationRepository;

        public AccommodationController(IAccommodationRepository accommodationRepository)
        {
            _accommodationRepository = accommodationRepository;
        }
        /*        [Authorize (Roles ="Users, Agent")]
        */
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccommodationDetail>>> GetAccommodations()
        {
            var accommodations = await _accommodationRepository.GetAllAccommodationsAsync();
            return Ok(accommodations);
        }

        [HttpGet("ByAgency/{agencyId}")]
        public async Task<ActionResult<IEnumerable<AccommodationDetail>>> GetAccommodationsByAgency(int agencyId)
        {
            var accommodations = await _accommodationRepository.GetAccommodationsByAgencyAsync(agencyId);

            if (accommodations == null || !accommodations.Any())
            {
                return NotFound();
            }

            return Ok(accommodations);
        }

        [HttpPost]
        public async Task<ActionResult<AccommodationDetail>> Post([FromForm] AccommodationDetail accommodation, IFormFile hotelImageFile, IFormFile placeImageFile)
        {
            var addedAccommodation = await _accommodationRepository.AddAccommodationAsync(accommodation, hotelImageFile, placeImageFile);
            return CreatedAtAction(nameof(GetAccommodations), new { id = addedAccommodation.AccommodationDetailId }, addedAccommodation);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<AccommodationDetail>> DeleteAccommodation(int id)
        {
            var deletedAccommodation = await _accommodationRepository.DeleteAccommodationAsync(id);
            if (deletedAccommodation == null)
            {
                return NotFound();
            }

            return deletedAccommodation;
        }
    }
}