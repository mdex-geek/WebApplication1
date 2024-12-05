using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Data;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HotelBookingController : ControllerBase
    {
        private readonly ApiContext _context;

        public HotelBookingController(ApiContext context)
        {
            _context = context;
        }
        
        // create
        [HttpPost]
        public JsonResult Create(HotelBooking booking)
        {
            if (booking.Id != 0)
            {
                return new JsonResult(BadRequest(new { message = "Id should not be provided for creation" }));
            }

            _context.Bookings.Add(booking);
            _context.SaveChanges();

            return new JsonResult(CreatedAtAction(nameof(Get), new { id = booking.Id }, booking));
        }

        // Edit
        [HttpPost]
        public JsonResult Edit(HotelBooking booking)
        {
            if (booking.Id == 0)
            {
                return new JsonResult(BadRequest(new { message = "Id is required for editing" }));
            }

            var bookingInDb = _context.Bookings.Find(booking.Id);
            if (bookingInDb == null)
            {
                return new JsonResult(NotFound(new { message = "Booking not found" }));
            }

            // Update the properties of the existing booking
            bookingInDb.Name = booking.Name;
            bookingInDb.RoomId = booking.RoomId;

            _context.SaveChanges();
            return new JsonResult(Ok(bookingInDb));
        }


        // Get
        [HttpGet]
        public JsonResult Get(int id)
        {
            var result = _context.Bookings.Find(id);

            if(result == null)
            {
                return new JsonResult(NotFound());
            }
            return new JsonResult(Ok(result));
        }

        // delete
        [HttpDelete]
        public JsonResult Delete(int id)
        {
            var result = _context.Bookings.Find(id);

            if(result == null)
            {
                return new JsonResult(NotFound());

            }
            _context.Bookings.Remove(result);
            _context.SaveChanges();
            return new JsonResult(NoContent());
                
        }

        // get all
        [HttpGet()]
        public JsonResult GetAll()
        {
            var result = _context.Bookings.ToList();
            return new JsonResult(Ok(result));
        }

    }
}
