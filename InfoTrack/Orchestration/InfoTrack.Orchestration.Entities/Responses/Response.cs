using System.Net;

namespace InfoTrack.Orchestration.Entities.Responses
{
    public class Response<T>
    {
        public T? Data { get; set; }
        public IList<string> Errors { get; set; } = new List<string>();
        public HttpStatusCode StatusCode { get; set; }
    }
}
