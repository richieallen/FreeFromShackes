using System.Collections.Generic;

namespace Swagometer.BlackBox.Specs.TestData
{
    public class TestAttendee
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }

        public List<string> ReceivedSwag { get; set; }
        public List<string> DeclinedSwag { get; set; }

        public TestAttendee()
        {
            ReceivedSwag = new List<string>();
            DeclinedSwag = new List<string>();
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