using InfoTrack.Domain.Entities;

namespace InfoTrack.Domain.Interfaces
{
    public interface IBookingService
    {
        Task<bool> IsBookingAvailable(Booking booking);
    }
}
