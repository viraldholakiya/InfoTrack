using InfoTrack.Orchestration.Entities.Requests;
using InfoTrack.Orchestration.Entities.Responses;

namespace InfoTrack.Orchestration.Interfaces
{
    public interface IBookingService
    {
        Task<Response<BookingCreateResponse>> Book(BookingCreateRequest bookingCreateRequest);
    }
}
