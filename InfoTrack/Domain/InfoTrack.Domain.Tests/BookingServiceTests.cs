using InfoTrack.Domain.Entities;
using InfoTrack.Infrastructure.InMemory;
using Moq;

namespace InfoTrack.Domain.Tests
{
    public class BookingServiceTests
    {
        [Fact]
        public async Task GivenBooking_WhenNoReservationPossible_ThenShouldReturnFalse()
        {
            // Arrange
            var expected = false;
            var bookingRepository = new Mock<IBookingRepository>();
            var bookingService = new BookingService(bookingRepository.Object);

            var newBooking = new Booking { Id = Guid.NewGuid(), Name = "New Booking", BookingStartTime = new TimeSpan(9, 20, 0), BookingEndTime = new TimeSpan(10, 20, 0) };

            var existingBookings = new List<Booking>
            {
                new Booking{ Id = Guid.NewGuid(), Name="Name 1", BookingStartTime = new TimeSpan(9,0,0), BookingEndTime = new TimeSpan(10,0,0) },
                new Booking{ Id = Guid.NewGuid(), Name="Name 2", BookingStartTime = new TimeSpan(9,15,0), BookingEndTime = new TimeSpan(10,15,0) },
                new Booking{ Id = Guid.NewGuid(), Name="Name 3", BookingStartTime = new TimeSpan(9,30,0), BookingEndTime = new TimeSpan(10,30,0) },
                new Booking{ Id = Guid.NewGuid(), Name="Name 4", BookingStartTime = new TimeSpan(9,45,0), BookingEndTime = new TimeSpan(10,45,0) }
            };

            bookingRepository.Setup(x => x.GetAllBookings()).Returns(Task.FromResult(existingBookings.AsQueryable()));

            // Act
            var actual = await bookingService.IsBookingAvailable(newBooking);

            // Assert
            Assert.Equal(expected, actual);
            bookingRepository.Verify(x => x.GetAllBookings(), Times.Once);

        }

        [Fact]
        public async Task GivenBooking_WhenReservationPossible_ThenShouldReturnTrue()
        {
            // Arrange
            var expected = true;
            var bookingRepository = new Mock<IBookingRepository>();
            var bookingService = new BookingService(bookingRepository.Object);

            var newBooking = new Booking { Id = Guid.NewGuid(), Name = "New Booking", BookingStartTime = new TimeSpan(10, 00, 0), BookingEndTime = new TimeSpan(11, 00, 0) };

            var existingBookings = new List<Booking>
            {
                new Booking{ Id = Guid.NewGuid(), Name="Name 1", BookingStartTime = new TimeSpan(9,0,0), BookingEndTime = new TimeSpan(10,0,0) },
                new Booking{ Id = Guid.NewGuid(), Name="Name 2", BookingStartTime = new TimeSpan(9,15,0), BookingEndTime = new TimeSpan(10,15,0) },
                new Booking{ Id = Guid.NewGuid(), Name="Name 3", BookingStartTime = new TimeSpan(9,30,0), BookingEndTime = new TimeSpan(10,30,0) },
                new Booking{ Id = Guid.NewGuid(), Name="Name 4", BookingStartTime = new TimeSpan(9,45,0), BookingEndTime = new TimeSpan(10,45,0) }
            };

            bookingRepository.Setup(x => x.GetAllBookings()).Returns(Task.FromResult(existingBookings.AsQueryable()));

            // Act
            var actual = await bookingService.IsBookingAvailable(newBooking);

            // Assert
            Assert.Equal(expected, actual);
            bookingRepository.Verify(x => x.GetAllBookings(), Times.Once);

        }
    }
}