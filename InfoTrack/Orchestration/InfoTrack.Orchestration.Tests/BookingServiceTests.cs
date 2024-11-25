using FluentValidation;
using InfoTrack.Domain.Entities;
using InfoTrack.Infrastructure.InMemory;
using InfoTrack.Orchestration.Entities.Requests;
using InfoTrack.Orchestration.Entities.Responses;
using InfoTrack.Orchestration.Services;
using Moq;

namespace InfoTrack.Orchestration.Tests
{
    public class BookingServiceTests
    {
        [Fact]
        public async Task GivenBooking_WhenInvalidBookingTime_ThenBadRequestExpected()
        {
            // Arrange
            var expected = new Response<BookingCreateResponse>
            {
                StatusCode = System.Net.HttpStatusCode.BadRequest,
                Errors = new List<string> { "Invalid booking time." }
            };

            var validator = new Mock<IValidator<Booking>>();
            var service = new Mock<Domain.Interfaces.IBookingService>();
            var repository = new Mock<IBookingRepository>();

            var bookingService = new BookingService(validator.Object, service.Object, repository.Object);

            var request = new BookingCreateRequest { Name = "Name 1", BookingTime = "AA:BB" };

            // Act
            var actual = await bookingService.Book(request);

            // Assert
            Assert.Equal(expected.StatusCode, actual.StatusCode);
            Assert.Contains(actual.Errors, error => error == expected.Errors.First());
        }

        [Theory]
        [InlineData("08:00")]
        [InlineData("16:30")]
        public async Task GivenBooking_WhenBookingTimeIsOutOfHours_ThenBadRequestExpected(string bookingTime)
        {
            // Arrange
            var expected = new Response<BookingCreateResponse>
            {
                StatusCode = System.Net.HttpStatusCode.BadRequest,
                Errors = new List<string> { "Booking time is out of hours." }
            };

            var validator = new Mock<IValidator<Booking>>();
            var service = new Mock<Domain.Interfaces.IBookingService>();
            var repository = new Mock<IBookingRepository>();

            var bookingService = new BookingService(validator.Object, service.Object, repository.Object);

            var request = new BookingCreateRequest { Name = "Name 1", BookingTime = bookingTime };

            // Act
            var actual = await bookingService.Book(request);

            // Assert
            Assert.Equal(expected.StatusCode, actual.StatusCode);
            Assert.Contains(actual.Errors, error => error == expected.Errors.First());
        }

        [Fact]
        public async Task GivenBooking_WhenNoBookingTimeAvailable_ThenConflictExpected()
        {
            // Arrange
            var expected = new Response<BookingCreateResponse>
            {
                StatusCode = System.Net.HttpStatusCode.Conflict,
                Errors = new List<string> { "All settlements at a booking time are reserved." }
            };

            var validator = new Mock<IValidator<Booking>>();
            var service = new Mock<Domain.Interfaces.IBookingService>();
            var repository = new Mock<IBookingRepository>();

            validator.Setup(x => x.Validate(It.IsAny<Booking>())).Returns(new FluentValidation.Results.ValidationResult());

            service.Setup(x => x.IsBookingAvailable(It.IsAny<Booking>())).Returns(Task.FromResult(false));

            var bookingService = new BookingService(validator.Object, service.Object, repository.Object);

            var request = new BookingCreateRequest { Name = "Name 1", BookingTime = "09:00" };

            // Act
            var actual = await bookingService.Book(request);

            // Assert
            Assert.Equal(expected.StatusCode, actual.StatusCode);
            Assert.Contains(actual.Errors, error => error == expected.Errors.First());
        }
    }
}