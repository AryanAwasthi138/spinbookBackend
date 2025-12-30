using Microsoft.AspNetCore.Mvc;
using TableTennisBooking.Data;
using TableTennisBooking.Models;
using TT_Exp.DTO;

namespace TableTennisBooking.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // [Authorize]
    // [Authorize(Roles = "Admin")] // Ensuring only admins can access these endpoints
    public class AdminController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AdminController(AppDbContext context)
        {
            _context = context;
        }




        //Get all Table
        [HttpGet("getalltable")]
        public ActionResult<List<Tables>> getalltable()
        {
            var tables = _context.Tables.ToList();

           if(tables==null || tables.Count == 0)
         {
              return NotFound(null);
        }
         foreach (var table in tables)
         {

           var updatetable =  GetAllSlots(table.TableId);

        }

         return Ok(tables);
        }


        // Add a new table
        [HttpPost("AddTable")]
        public IActionResult AddTable(AddTableDTO tableData)
        {
            var table = new Tables { TableName = tableData.TableName, TableDescription = tableData.TableDescription };
            _context.Tables.Add(table);
            _context.SaveChanges();

            return Ok(new { Message = "Table added successfully." });
        }

        // Remove an existing table
        [HttpDelete("RemoveTable/{tableId}")]
        public IActionResult RemoveTable(int tableId)
        {
            var table = _context.Tables.FirstOrDefault(t => t.TableId == tableId);
            if (table == null)
            {
                return NotFound(new { message = "Table not found." });
            }

            _context.Tables.Remove(table);
            _context.SaveChanges();

            return Ok(new { Message = "Table removed successfully." });
        }

        [HttpPut("UpdateTable")]
        public IActionResult UpdateTable(UpdateTable table)
        {

            var getTable = _context.Tables.FirstOrDefault(t => t.TableId == table.tableId);
            if (getTable == null)
            {
                return NotFound(new { Message = "Table not found." });
            }
            getTable.TableId = table.tableId;
            getTable.TableName = table.TableName;
            getTable.TableDescription = table.TableDescription;


            _context.Tables.Update(getTable);
            _context.SaveChanges();
            return Ok(new { Message = "Table has been Updated" });
        }

        [HttpGet("GetAllSlots")]
        public ActionResult<List<Slot>> GetAllSlots(int TableId)
        {
            var table = _context.Tables.FirstOrDefault(t => t.TableId == TableId);

            if (table == null)
            {
                return NotFound(null);
            }
            table.IsAvailable = false;



            var slots = _context.Slots.ToList().Where(s => s.TableId == table.TableId);


            foreach (var slot in slots)
            {
                if (slot.TodaysDate != DateTime.Today.ToString("dd/MM/yyyy"))
                {
                    slot.IsBooked = false;
                    slot.Id = null;
                }
                if (slot.IsBooked == false)
                {
                    table.IsAvailable = true;
                }
                else
                {
                    var user = _context.Users.FirstOrDefault(u => u.Id == slot.Id);
                    slot.Users = user;
                }

            }
            _context.Tables.Update(table);
            _context.SaveChanges();


            return Ok(slots);
        }

        // Add a slot to a specific table
        [HttpPost("AddSlot")]
        public IActionResult AddSlot(AddSlotDTO slotDetail)
        {
            var table = _context.Tables.FirstOrDefault(t => t.TableId == slotDetail.tableId);
            if (table == null)
            {
                return NotFound(new { Message = "Table not found." });
            }

            // Check if the slot already exists for this table and time
            var existingSlot = _context.Slots
                .FirstOrDefault(s => s.TableId == slotDetail.tableId && s.StartTime == slotDetail.StartTime.TimeOfDay && s.EndTime == slotDetail.EndTime.TimeOfDay);

            if (existingSlot != null)
            {
                return BadRequest(new { Message = "Slot already exists for the specified time." });
            }

            var slot = new Slot
            {
                TableId = slotDetail.tableId,
                StartTime = slotDetail.StartTime.TimeOfDay,
                EndTime = slotDetail.EndTime.TimeOfDay,
                IsBooked = false,
                TodaysDate = DateTime.Today.ToString("dd/MM/yyyy")
            };
            _context.Slots.Add(slot);
            _context.SaveChanges();

            return Ok(new { Message = "Slot added successfully.", SlotId = slot.SlotId });
        }

        // Remove an existing slot from a specific table
        [HttpDelete("RemoveSlot/{slotId}")]
        public IActionResult RemoveSlot(int slotId)
        {
            var slot = _context.Slots.FirstOrDefault(s => s.SlotId == slotId);
            if (slot == null)
            {
                return NotFound(new { Message = "Slot not found." });
            }
            if (slot.IsBooked)
            {
                return BadRequest(new {Message = "Slot is Booked , Can't be removed"});
            }
            _context.Slots.Remove(slot);
            _context.SaveChanges();

            return Ok(new { Message = "Slot removed successfully." });
        }

        [HttpPost("ResetSlots")]
        public IActionResult ResetSlots()
        {
            var slots = _context.Slots.Where(s => s.IsBooked).ToList();

            foreach (var slot in slots)
            {
                slot.IsBooked = false;
                slot.Id = null;
            }

            _context.SaveChanges();

            return Ok("Slots reset successfully.");
        }

        [HttpPut("ResetTableSlots")]
        public IActionResult ResetSlots([FromBody]int tableId)
        {
            var slots = _context.Slots.Where(s=>s.TableId == tableId).ToList();
            if (slots.Count == 0) {

                return NotFound(new {Message ="Table not Found"});
            }
            foreach (var slot in slots)
            {
                slot.IsBooked = false;
                slot.Id = null;
            }

            _context.SaveChanges();

            return Ok(new { Message = "Slots reset successfully." });

        }
    }
}
