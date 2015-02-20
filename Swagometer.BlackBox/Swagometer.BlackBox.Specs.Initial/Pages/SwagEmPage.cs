using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Swagometer.BlackBox.Specs.Pages
{
    [Binding]
    public class SwagEmPage
    {
        private readonly TestRunContext _context;
        private readonly IWebDriver _driver;

        public SwagEmPage(TestRunContext context, IWebDriver driver)
        {
            _context = context;
            _driver = driver;
        }

        public void GiveAwaySwag()
        {
            _driver.Navigate().GoToUrl(_context.BaseUrl);

            var swagButton = _driver.FindElement(By.Id("swagEm"));
            swagButton.Click();
        }

        public bool ItemSupplierIsVisible()
        {
            var element = _driver.FindElement(By.Id("swag-supplier"));
            return element.Displayed && element.Enabled;
        }

        public bool ItemNameIsVisible()
        {
            var element = _driver.FindElement(By.Id("swag-name"));
            return element.Displayed && element.Enabled;
        }

        public bool AttendeeIsVisible()
        {
            var element = _driver.FindElement(By.Id("attendee-fullname"));
            return element.Displayed && element.Enabled;
        }
    }
}
