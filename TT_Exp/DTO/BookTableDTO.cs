using System.ComponentModel.DataAnnotations;

namespace TT_Exp.DTO
{
    public class BookTableDTO
    {
        [Required]
        public int TableId { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}

