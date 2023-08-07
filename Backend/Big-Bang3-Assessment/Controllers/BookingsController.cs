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
    public class BookingsController : ControllerBase
    {
        private readonly TourismDbContext _context;

        public BookingsController(TourismDbContext context)
        {
            _context = context;
        }

        // GET: api/Bookings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Booking>>> GetBooking()
        {
            return await _context.Booking.ToListAsync();
        }

        // GET: api/Bookings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Booking>> GetBooking(int id)
        {
            var booking = await _context.Booking.FindAsync(id);

            if (booking == null)
            {
                return NotFound();
            }

            return booking;
        }

        // PUT: api/Bookings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBooking(int id, Booking booking)
        {
            if (id != booking.Booking_Id)
            {
                return BadRequest();
            }

            _context.Entry(booking).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingExists(id))
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

        // POST: api/Bookings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Booking>> PostBooking(Booking booking)
        {
            var user = await _context.users.FindAsync(booking.user.User_Id);

            if (user == null)
            {
                return BadRequest("Invalid user ID");
            }

            booking.user = user;

            var agency = await _context.agencies.FindAsync(booking.agency.Agency_Id);
            if (agency == null)
            {
                return BadRequest("Invalid agency ID");
            }
            booking.agency = agency;



            // Calculate amount_for_person and amount_for_childer
            int numberOfPersons = booking.no_of_perons;
            int numberOfChildren = booking.no_of_childer;
            int rateForDay = booking.agency.rate_for_day;

            booking.amount_for_person = numberOfPersons * rateForDay;
            booking.amount_for_childer = numberOfChildren * rateForDay;

            // Calculate booking_amount (total booking amount)
            booking.booking_amount = booking.amount_for_person + booking.amount_for_childer;

            _context.Booking.Add(booking);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBooking", new { id = booking.Booking_Id }, booking);
        }


        // DELETE: api/Bookings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var booking = await _context.Booking.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }

            _context.Booking.Remove(booking);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/Bookings/ByUser/{userId}
        [HttpGet("ByUser/{userId}")]
        public async Task<ActionResult<IEnumerable<Booking>>> GetBookingsByUser(int userId)
        {
            var bookings = await _context.Booking
                .Include(b => b.user)
                .Include(b => b.agency)
                .Where(b => b.user.User_Id == userId)
                .ToListAsync();

            if (bookings == null)
            {
                return NotFound();
            }

            return bookings;
        }


        // GET: api/Bookings/ByAgency/{agencyId}
        [HttpGet("ByAgency/{agencyId}")]
        public async Task<ActionResult<int>> GetNumberOfBookingsByAgency(int agencyId)
        {
            var numberOfBookings = await _context.Booking
                .CountAsync(b => b.agency.Agency_Id == agencyId);

            return numberOfBookings;
        }



        private bool BookingExists(int id)
        {
            return _context.Booking.Any(e => e.Booking_Id == id);
        }
    }
}
