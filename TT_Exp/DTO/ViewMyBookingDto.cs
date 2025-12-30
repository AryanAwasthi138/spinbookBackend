namespace TT_Exp.DTO
{
    public class ViewMyBookingDto
    {
        public int TableId { get; set; }
        public int slotId { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string TableName { get; set; }
        public string TodaysDate { get; set; }
        
    }
}