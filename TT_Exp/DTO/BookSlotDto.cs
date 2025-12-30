using System.ComponentModel.DataAnnotations;

namespace TT_Exp.DTO
{
    public class BookSlotDto
    {
        [Required] 
        public string UserName {  get; set; }
        public int tableId {  get; set; }
        public int slotId { get; set; }
    }
}
