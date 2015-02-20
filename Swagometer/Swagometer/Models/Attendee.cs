using System.Collections.Generic;
using System.Linq;

namespace Swagometer.Models
{
    public class Attendee
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsMember { get; set; }
        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }

        public List<string> ReceivedSwag { get; set; }
        public List<string> DeclinedSwag { get; set; }

        public Attendee()
        {
            ReceivedSwag = new List<string>();
            DeclinedSwag = new List<string>();
        }

        public bool HasReceivedSwag()
        {
            return ReceivedSwag.Any();
        }

        
    }
}