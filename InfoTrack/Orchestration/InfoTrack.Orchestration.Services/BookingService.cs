using FluentValidation;
using InfoTrack.Common;
using InfoTrack.Domain.Entities;
using InfoTrack.Infrastructure.InMemory;
using InfoTrack.Orchestration.Entities.Requests;
using InfoTrack.Orchestration.Entities.Responses;
using InfoTrack.Orchestration.Interfaces;

namespace InfoTrack.Orchestration.Services
{
    public class BookingService(IValidator<Booking> bookingValidator,
                        Domain.Interfaces.IBookingService bookingService,
                        IBookingRepository bookingRepository) : IBookingService
    {
        public async Task<Response<BookingCreateResponse>> Book(BookingCreateRequest bookingCreateRequest)
        {
            if (!TimeSpan.TryParse(bookingCreateRequest.BookingTime, out TimeSpan bookingTime))
            {
                return new Response<BookingCreateResponse>
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Errors = new List<string> { "Invalid booking time." }
                };
            }

            if (bookingTime < OfficeHours.FirstBookingTime || bookingTime > OfficeHours.LastBookingTime)
            {
                return new Response<BookingCreateResponse>
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Errors = new List<string> { "Booking time is out of hours." }
                };
            }

            var booking = new Booking
            {
                Id = Guid.NewGuid(),
                Name = bookingCreateRequest.Name,
                BookingStartTime = bookingTime,
                BookingEndTime = bookingTime.Add(TimeSpan.FromHours(1))
            };

            var validationResult = bookingValidator.Validate(booking);
            if (!validationResult.IsValid)
            {
                return new Response<BookingCreateResponse>
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList()
                };
            }

            var isBookingAvailable = await bookingService.IsBookingAvailable(booking);
            if (!isBookingAvailable)
            {
                return new Response<BookingCreateResponse>
                {
                    StatusCode = System.Net.HttpStatusCode.Conflict,
                    Errors = new List<string> { "All settlements at a booking time are reserved." }
                };
            }

            await bookingRepository.Save(booking);
            
            return new Response<BookingCreateResponse>
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Data = new BookingCreateResponse { BookingId = booking.Id }
            };
        }
    }
}
