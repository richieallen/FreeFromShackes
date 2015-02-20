using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using BoDi;
using Moq;
using Swagometer.Controllers;
using Swagometer.Data;
using Swagometer.Models;
using Swagometer.Services;
using Swagometer.Tests.TestData;
using TechTalk.SpecFlow;

namespace Swagometer.Tests
{
    [Binding]
    public class TestRunContext
    {
        public string BaseUrl { get; set; }
        public IEnumerable<TestSwagItem> SwagItems { get; set; }
        public IEnumerable<TestAttendee> Attendees { get; set; }
        public ActionResult CurrentActionResult { get; set; }
        public Mock<IAttendeeRepository> MockAttendeeRepository { get; set; }
        public Mock<ISwagRepository> MockSwagRepository { get; set; }
        public Mock<HttpContextBase> MockHttpContext { get; set; }
        public Mock<ControllerContext> MockControllerContext { get; set; }

        public TestRunContext(ObjectContainer container)
        {
            BaseUrl = "http://swagometer.com";

            MockHttpContext = new Mock<HttpContextBase>();
            MockHttpContext.Setup(x => x.Request.Url).Returns(() => new Uri(BaseUrl));

            MockControllerContext = new Mock<ControllerContext>();
            MockControllerContext.Setup(x => x.HttpContext).Returns(MockHttpContext.Object);

            MockAttendeeRepository = new Mock<IAttendeeRepository>();
            MockSwagRepository = new Mock<ISwagRepository>();
            
            SetupDependencies(container);
        }

        private void SetupDependencies(ObjectContainer container)
        {
            container.RegisterInstanceAs(MockAttendeeRepository.Object, typeof(IAttendeeRepository));
            container.RegisterInstanceAs(MockSwagRepository.Object, typeof(ISwagRepository));
            container.RegisterTypeAs<IAttendeeService>(typeof(AttendeeService));
            container.RegisterTypeAs<ISwagService>(typeof(SwagService));
            container.RegisterTypeAs<SwagEmController>(typeof(SwagEmController));
        }

        [BeforeScenario()]
        public void BeforeScenario()
        {
            MockAttendeeRepository.Setup(x => x.GetAllAttendees())
                                  .Returns(() => Attendees);

            MockSwagRepository.Setup(x => x.GetAllSwag())
                              .Returns(() => SwagItems);
        }
    }
}
