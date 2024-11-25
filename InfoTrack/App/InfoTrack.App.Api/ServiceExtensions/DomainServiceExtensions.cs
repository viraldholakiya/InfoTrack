using InfoTrack.Domain;
using InfoTrack.Domain.Entities;
using InfoTrack.Domain.Interfaces;

namespace InfoTrack.App.Api.ServiceExtensions
{
    public static class DomainServiceExtensions
    {
        public static void AddDomainServices(this IServiceCollection services)
        {
            services.AddSingleton(typeof(BookingContext));
            services.AddScoped<IBookingService, BookingService>();
        }
    }
}