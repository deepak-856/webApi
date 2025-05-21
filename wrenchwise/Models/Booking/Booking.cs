namespace wrenchwise.Models.Booking
{
    public class Booking
    {
        public string? ServiceType { get; set; }
        public string? PickupDrop { get; set; }
        public DateTime PreferredDate { get; set; }
        public string? PreferredTime { get; set; }
        public string? Notes { get; set; }
    }
}
