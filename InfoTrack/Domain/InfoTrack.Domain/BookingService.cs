using InfoTrack.Domain.Entities;
using InfoTrack.Domain.Interfaces;
using InfoTrack.Infrastructure.InMemory;

namespace InfoTrack.Domain
{
    public class BookingService(IBookingRepository bookingRepository) : IBookingService
    {
        public async Task<bool> IsBookingAvailable(Booking booking)
        {
            var bookings = await bookingRepository.GetAllBookings();

            var overlappingCount = bookings.Where(b => (booking.BookingStartTime >= b.BookingStartTime &&
                                                            booking.BookingStartTime < b.BookingEndTime) ||
                                                          (booking.BookingEndTime >= b.BookingStartTime &&
                                                            booking.BookingEndTime < b.BookingEndTime)).Count();

            return overlappingCount < Common.Constants.SimultaneousSettlementsPossible;
        }
    }
}
