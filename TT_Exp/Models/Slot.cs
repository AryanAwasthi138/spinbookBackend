using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TableTennisBooking.Models
{
    public class Slot
    {
        [Key]
        public int SlotId { get; set; }
        public int TableId { get; set; }
        public string Id { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public string TodaysDate { get; set; }
        public bool IsBooked { get; set; }


        [ForeignKey("Id")]
        public User Users { get; set; }

        [ForeignKey("TableId")]
        public Tables Table { get; set; }
    }

}