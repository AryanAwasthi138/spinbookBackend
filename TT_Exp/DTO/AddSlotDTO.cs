using System.ComponentModel.DataAnnotations;

namespace TT_Exp.DTO
{
    public class AddSlotDTO
    {
        [Required]
        public int tableId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
