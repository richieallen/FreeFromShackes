using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BoDi;
using Newtonsoft.Json;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using Swagometer.BlackBox.Specs.TestData;
using TechTalk.SpecFlow;

namespace Swagometer.BlackBox.Specs
{
    [Binding]
    public class TestRunContext
    {
        private readonly ObjectContainer _objectContainer;
        public string BaseUrl = "http://localhost:51080/";
        public IWebDriver Driver { get; set; }
        public IEnumerable<TestSwagItem> SwagItems { get; set; }
        public IEnumerable<TestAttendee> Attendees { get; set; }
        

        public TestRunContext(ObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
        }

        private void SetupDepdendencies(ObjectContainer objectContainer)
        {
            objectContainer.RegisterInstanceAs(Driver, typeof(IWebDriver));
        }

        [BeforeScenario()]
        public void BeforeScenario()
        {
            Driver = new FirefoxDriver();
            SetupDepdendencies(_objectContainer);
        }

        [BeforeScenarioBlock()]
        public void BeforeWhen()
        {
            if (CanRunScenario())
            {
                if (ScenarioContext.Current.CurrentScenarioBlock == ScenarioBlock.When)
                {
                    SetupTestData();
                }
            }
            else
            {
                ScenarioContext.Current.Pending();
            }
        }

        private void SetupTestData()
        {
            const string baseUrl = "http://localhost:51080/api/";

            using (var httpClient = new HttpClient())
            {
                var clearAllResult = httpClient.PostAsync(baseUrl + "TestApi/ClearAll", null).Result;
                if (!clearAllResult.IsSuccessStatusCode)
                {
                    throw new Exception("Failed to clear data stores");
                }

                var attendeesTask = httpClient.PostAsync(baseUrl + "TestApi/Attendees", new StringContent(JsonConvert.SerializeObject(Attendees)));
                var swagTask = httpClient.PostAsync(baseUrl + "TestApi/Swag", new StringContent(JsonConvert.SerializeObject(SwagItems)));

                Task.WaitAll(attendeesTask, swagTask);
            }
        }

        private bool CanRunScenario()
        {
            if (!IsUnitTest() && !IsManual())
            {
                return true;
            }

            return false;
        }

        private bool IsManual()
        {
            return HasTag("Manual"); ;
        }

        private bool IsUnitTest()
        {
            return HasTag("UnitTest");
        }

        private bool HasTag(string tag)
        {
            return FeatureContext.Current.FeatureInfo.Tags
                            .Union(ScenarioContext.Current.ScenarioInfo.Tags)
                            .Select(t => t.ToLowerInvariant())
                            .Contains(tag.ToLowerInvariant());
        }
    }
}
