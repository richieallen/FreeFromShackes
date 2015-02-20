using System.Collections.Generic;

namespace Swagometer.Models
{
    public interface IAttendeeService
    {
        IEnumerable<Attendee> GetAllAttendees();
        void AttendeeReceivedSwag(Attendee attendee, SwagItem swag);
    }
}