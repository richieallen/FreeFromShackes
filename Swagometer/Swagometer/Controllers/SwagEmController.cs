using System.Linq;
using System.Web.Mvc;
using Swagometer.Models;

namespace Swagometer.Controllers
{
    public class SwagEmController : Controller
    {
        private readonly IAttendeeService _attendeeService;
        private readonly ISwagService _swagService;

        public SwagEmController(IAttendeeService attendeeService, ISwagService swagService)
        {
            _attendeeService = attendeeService;
            _swagService = swagService;
        }

        public ActionResult Index()
        {
            var swagEmViewModel = new SwagEmViewModel
            {
                BaseUrl = Request.Url != null ? Request.Url.ToString() : string.Empty
            };

            return View(swagEmViewModel);
        }

        [HttpPost]
        public ActionResult SwagEm()
        {
            var swagEmResult = new SwagEmResult();
            var swag = _swagService.GetAllSwag()
                                   .FirstOrDefault(s => !s.HasBeenSwagged);

            if (swag != null)
            {
                var attendee = _attendeeService.GetAllAttendees()
                    .FirstOrDefault(a => !a.HasReceivedSwag() &&
                                         !a.DeclinedSwag.Contains(swag.Name) &&
                                         (!swag.IsMemberOnly || a.IsMember));

                _attendeeService.AttendeeReceivedSwag(attendee, swag);
                _swagService.MarkAsSwagged(swag);


                swagEmResult = new SwagEmResult
                {
                    Attendee = attendee,
                    SwagItem = swag
                };
            }

            return Json(swagEmResult);
        }
    }
}