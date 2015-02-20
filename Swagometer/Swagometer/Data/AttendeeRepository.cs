using System.Collections.Generic;
using Swagometer.Models;

namespace Swagometer.Data
{
    public class AttendeeRepository : IAttendeeRepository
    {
        private readonly List<Attendee> _attendees;

        public AttendeeRepository()
        {
            _attendees = new List<Attendee>();
        }

        public IEnumerable<Attendee> GetAllAttendees()
        {
            return _attendees;
        }

        public void AttendeeReceivedSwag(Attendee attendee, SwagItem swagItem)
        {
            attendee.ReceivedSwag.Add(swagItem.Name);
        }

        public void Add(Attendee[] attendees)
        {
            _attendees.AddRange(attendees);
        }

        public void ClearAll()
        {
            _attendees.Clear();
        }

        public void AddAttendee(Attendee attendee)
        {
            _attendees.Add(attendee);
        }

        public void RemoveAttendee(Attendee attendee)
        {
            _attendees.Remove(attendee);
        }
    }
}