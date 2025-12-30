using System;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TableTennisBooking.Data;
using TableTennisBooking.Models;
using TT_Exp.DTO;

namespace TableTennisBooking.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BookingController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPut("BookSlot")]
        public ActionResult BookSlot(BookSlotDto slotDetails)
        {
            var user = _context.Users.FirstOrDefault(u=>u.UserName== slotDetails.UserName);
            if (user == null)
            {
                return NotFound(new { Message = "User not found." });
            }


            var table = _context.Tables.FirstOrDefault(t => t.TableId == slotDetails.tableId);
            if (table == null)
            {
                return NotFound(new { Message = "Table not found." });
            }

            var slot = _context.Slots
                .ToList().Where(s=>s.TableId== table.TableId)
                .FirstOrDefault(t => t.SlotId == slotDetails.slotId);

            if(slot == null)
            {
                return NotFound(new { Message = "Slot Not Found" });
            }

            slot.IsBooked = true;
            slot.TodaysDate = DateTime.Today.ToString("dd/MM/yyyy");
            slot.Id = user.Id;
            _context.Update(slot);
            _context.SaveChanges();
            return Ok(new { Message = "Slot Booked Successfully" });
        }

        [HttpGet("ViewMyBooking")]
        public ActionResult ViewMyBooking(string UserName)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName == UserName);
            if (user == null)
            {
                return NotFound("User Not Found");
            }

            var slots = _context.Slots.ToList().Where(s=>s.Id== user.Id);
            List<ViewMyBookingDto> bookinglist = new List<ViewMyBookingDto>();
            foreach (var slot in slots) {
                var table = _context.Tables.FirstOrDefault(t => t.TableId == slot.TableId);
                bookinglist.Add(new ViewMyBookingDto
                {
                    TableId = slot.TableId,
                    slotId = slot.SlotId,
                    StartTime=slot.StartTime,
                    EndTime=slot.EndTime,
                    TodaysDate=slot.TodaysDate,
                    TableName = table.TableName
                });
            }

            return Ok(bookinglist);
        }
        //[HttpPost("BookSlot")]
        //public IActionResult BookSlot(int tableId, DateTime StartTime, DateTime EndTime,string username)
        //{
        //    var slot = _context.Slots
        //        .FirstOrDefault(s => s.TableId == tableId && s.StartTime == StartTime.TimeOfDay && s.EndTime == EndTime.TimeOfDay);

        //    if (slot == null)
        //    {
        //        return NotFound("Slot not found.");
        //    }

        //    if (slot.IsBooked)
        //    {
        //        return BadRequest("Slot is already booked.");
        //    }

        //    slot.IsBooked = true;
        //    _context.Bookings.Add(new Booking
        //    {
        //        SlotId = slot.SlotId,
        //        BookingDate = DateTime.Now,
        //        UserName = username
        //    });

        //    _context.SaveChanges();

        //    return Ok("Slot booked successfully.");
        //}
        //[HttpPost("BookTable")]

        //public IActionResult BookTable(int tableId, DateTime StartTime, DateTime EndTime)
        //{
        //    var Table = _context.Tables
        //        .FirstOrDefault(t => t.TableId == tableId);
        //    if (Table == null)
        //    {
        //        return NotFound("This TT Table not Found");
        //    }

        //    var existingBooking = _context.Bookings
        //        .FirstOrDefault(b => b.TableId == tableId &&
        //        ((b.StartTime <= StartTime && b.EndTime > StartTime) ||
        //                      (b.StartTime < EndTime && b.EndTime >= EndTime)));
        //    if (existingBooking != null)
        //    {
        //        return BadRequest("Table is already booked for the requested Time Period.");
        //    }
        //    else
        //    {
        //        var Booking = new Booking
        //        {
        //            TableId = tableId,
        //            StartTime = StartTime,
        //            EndTime = EndTime,

        //        };

        //        _context.Bookings.Add(Booking);
        //        return Ok("Table Booked Successfully.");
        //    }

        //}



    }
}






