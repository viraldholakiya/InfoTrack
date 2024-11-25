namespace InfoTrack.Domain.Entities
{
    public class Booking
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public TimeSpan BookingStartTime { get; set; }
        public TimeSpan BookingEndTime { get; set; }
    }
}
