using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TableTennisBooking.Models
{

    public class Booking
    {
        [Key]
        public int BookingId { get; set; }
        public int SlotId { get; set; }
        public int TableId { get; set; }

        public string UserId { get; set; }   //* Identity UserId
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public Slot Slot { get; set; }
        public string UserName { get; set; }
        public DateTime BookingDate { get; set; }
    }
}

