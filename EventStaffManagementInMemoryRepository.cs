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
                throw new Exception("Staff cannot be null");
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
        public List<EventStaff> GetAllVendors()
        {
            List<EventStaff> vendors = new List<EventStaff>();
            for (int i = 0; i < eventstaffs.Count; i++)
            {
                if (eventstaffs[i] is Vendor vendor)
                {
                    vendors.Add(vendor);
                }
            }
            return vendors;
        }
        public List<EventStaff> GetAllPerformers()
        {
            List<EventStaff> performers = new List<EventStaff>();
            for (int i = 0; i < eventstaffs.Count; i++)
            {
                if (eventstaffs[i] is Performer performer)
                {
                    performers.Add(performer);
                }
            }
            return performers;
        }
        public List<EventStaff> GetAllCrew()
        {
            List<EventStaff> crew_members = new List<EventStaff>();
            for (int i = 0; i < eventstaffs.Count; i++)
            {
                if (eventstaffs[i] is Crew crew)
                {
                    crew_members.Add(crew);
                }
            }
            return crew_members;
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
                if (eventstaffs[i].Email == email)
                {
                    return true;
                }

            }
            return false;
        }
        public void Delete(int PersonID)
        {
            EventStaff eventstaff = FindByID(PersonID);
            if (eventstaff == null)
            {
                eventstaffs.Remove(eventstaff);
            }
        }
        public EventStaff FindByID(int id)
        {
            string newid = id.ToString();
            for (int i = 0; i < eventstaffs.Count; i++)

            {
                if (eventstaffs[i].PersonID == newid)
                {
                    return eventstaffs[i];

                }
            }
            return null;
        }
        public void Edit(EventStaff eventstaff, int PersonID)
        {
            if (eventstaff == null)
            {
                throw new ArgumentException(nameof(eventstaff));
            }
            string targetID = PersonID.ToString();
            for (int i = 0; i < eventstaffs.Count; i++)
            {
                if (eventstaffs[i].PersonID == targetID)
                {
                    eventstaff.PersonID = eventstaffs[i].PersonID;
                    eventstaffs[i] = eventstaff;
                    return;
                }
            }
            throw new Exception($"Staff member with ID {PersonID} was not found");
        }
    }
}
