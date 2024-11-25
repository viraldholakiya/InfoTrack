using InfoTrack.Orchestration.Entities.Requests;
using InfoTrack.Orchestration.Entities.Responses;
using InfoTrack.Orchestration.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InfoTrack.App.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingsController(IBookingService bookingService) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<Response<BookingCreateResponse>>> Post(BookingCreateRequest bookingCreateRequest)
        {
            var response = await bookingService.Book(bookingCreateRequest);

            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return BadRequest(response);
            }

            if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                return Conflict(response);
            }

            return Ok(response);
        }
    }
}
