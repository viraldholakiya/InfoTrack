using InfoTrack.Domain.Entities;

namespace InfoTrack.Infrastructure.InMemory
{
    public class BookingRepository : IBookingRepository
    {
        public BookingContext BookingContext { get; set; }

        public BookingRepository(BookingContext bookingContext)
        {
            BookingContext = bookingContext;
        }

        public async Task Save(Booking booking)
        {
            BookingContext.Bookings.Add(booking);
        }

        public async Task<IQueryable<Booking>> GetAllBookings()
        {
            return BookingContext.Bookings.AsQueryable();
        }
    }
}