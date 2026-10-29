using System;
using System.Collections.Generic;
using System.Text;

namespace musicfestivalmanagementapp
{
    internal class EventStaffManagementRepository : IEventStaffManagementRepository  
    {
        private List<EventStaff> eventstaffs;
        private int nextId;
        public EventStaffManagementRepository()
        {
            eventstaffs = new List<EventStaff>();
            nextId = 1;
        }
        public void Add(EventStaff eventstaff)
        {
            if (eventstaff == null)
            {
                throw new Exception("Item cannot be null");
            }

            if (string.IsNullOrWhiteSpace(eventstaff.PersonID))
            {
                eventstaff.PersonID = GeneratePersonID(eventstaff);
            }

            eventstaffs.Add(eventstaff);
        }
        public List<EventStaff> GetAll()
        {
            return eventstaffs;
        }

        public string GeneratePersonID(EventStaff eventstaff)
        {
            string prefix = "I";

            if (eventstaff is Vendor)
            {
                prefix = "V";
            }
            if (eventstaff is Performer)
            {
                prefix = "P";
            }
            if (eventstaff is Crew)
            {
                prefix = "C";
            }
            //provide code to generate unique ID based on item type (V for vendor, P for Performer, C for Crew)
            string id = prefix + nextId.ToString("D4");
            nextId++;
            return id;
        }
        public bool EmailExists(string email)
        {
            for (int i = 0; i < eventstaffs.Count; i++)
            {
                if(eventstaffs[i].Email == email)
                {
                    return true;
                }

            }
            return false;
        }
        public void Delete(string PersonID) { }
    }
}
