using InfoTrack.Orchestration.Interfaces;
using InfoTrack.Orchestration.Services;

namespace InfoTrack.App.Api.ServiceExtensions
{
    public static class OrchestrationServiceExtensions
    {
        public static void AddOrchestrationServices(this IServiceCollection services)
        {
            services.AddScoped<IBookingService, BookingService>();
        }
    }
}   