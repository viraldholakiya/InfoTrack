using FluentValidation;
using InfoTrack.Domain.Entities;
using InfoTrack.Domain.Validators;
using InfoTrack.Infrastructure.InMemory;

namespace InfoTrack.App.Api.ServiceExtensions
{
    public static class InfrastructureServiceExtensions
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IValidator<Booking>, BookingValidator>();
            services.AddScoped<IBookingRepository, BookingRepository>();
        }
    }
}