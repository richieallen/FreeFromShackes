using System.Collections.Generic;
using Swagometer.Models;

namespace Swagometer.Data
{
    public interface IAttendeeRepository
    {
        IEnumerable<Attendee> GetAllAttendees();
        void AttendeeReceivedSwag(Attendee attendee, SwagItem swagItem);
        void Add(Attendee[] attendees);
        void ClearAll();
    }
}