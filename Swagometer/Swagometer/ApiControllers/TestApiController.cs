using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Swagometer.Data;
using Swagometer.Models;

namespace Swagometer.ApiControllers
{
    public class TestApiController : ApiController
    {
        private readonly IAttendeeRepository _attendeeRepository;
        private readonly ISwagRepository _swagRepository;

        public TestApiController(IAttendeeRepository attendeeRepository, ISwagRepository swagRepository)
        {
            _attendeeRepository = attendeeRepository;
            _swagRepository = swagRepository;
        }

        [Route("api/testapi/clearall")]
        [HttpPost]
        public HttpResponseMessage ClearAll(HttpRequestMessage request)
        {
            _attendeeRepository.ClearAll();
            _swagRepository.ClearAll();
            return new HttpResponseMessage();
        }

        [Route("api/testapi/attendees")]
        [HttpPost]
        public HttpResponseMessage Attendees(HttpRequestMessage request)
        {
            var attendees = JsonConvert.DeserializeObject<Attendee[]>(request.Content.ReadAsStringAsync().Result);
            _attendeeRepository.Add(attendees);
            return new HttpResponseMessage();
        }

        [Route("api/testapi/swag")]
        [HttpPost]
        public HttpResponseMessage Swag(HttpRequestMessage request)
        {
            var swag = JsonConvert.DeserializeObject<SwagItem[]>(request.Content.ReadAsStringAsync().Result);
            _swagRepository.AddSwag(swag);
            return new HttpResponseMessage();
        }
    }
}
