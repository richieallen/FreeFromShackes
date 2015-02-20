using Swagometer.Models;

namespace Swagometer.Tests.TestData
{
    public class TestAttendee : Attendee
    {
        public TestAttendee()
        {
            FirstName = "Barrie";
            LastName = "Jones";
        }

        public TestAttendee HasReceivedSwagItem(string swagName)
        {
            ReceivedSwag.Add(swagName);
            return this;
        }

        public TestAttendee HasDeclinedReceivingSwagItem(string swagName)
        {
            DeclinedSwag.Add(swagName);
            return this;
        }
    }
}