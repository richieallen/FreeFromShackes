using System.Collections.Generic;
using Swagometer.Data;
using Swagometer.Models;

namespace Swagometer.Services
{
    public class AttendeeService : IAttendeeService
    {
        private readonly IAttendeeRepository _attendeeRepository;

        public AttendeeService(IAttendeeRepository attendeeRepository)
        {
            _attendeeRepository = attendeeRepository;
            
        }

        public IEnumerable<Attendee> GetAllAttendees()
        {
            return _attendeeRepository.GetAllAttendees();
        }

        public void AttendeeReceivedSwag(Attendee attendee, SwagItem swag)
        {
            _attendeeRepository.AttendeeReceivedSwag(attendee, swag);
        }
    }
}