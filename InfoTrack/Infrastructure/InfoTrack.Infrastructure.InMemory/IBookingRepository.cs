using InfoTrack.Domain.Entities;

namespace InfoTrack.Infrastructure.InMemory
{
    public interface IBookingRepository
    {
        Task Save(Booking booking);
        Task<IQueryable<Booking>> GetAllBookings();
    }
}
