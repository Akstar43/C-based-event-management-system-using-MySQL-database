using System;
using System.Collections.Generic;
using System.Text;

namespace musicfestivalmanagementapp
{
    internal class EventStaffManagementManager
    {
        private IEventStaffManagementRepository repository;

        public EventStaffManagementManager(IEventStaffManagementRepository repository)
        {
            this.repository = repository;
        }

        public string AddVendor(string name, string email, string telephone, string role, string vendor_name, List<string> categories)
        {
            if (name == "")
            {
                return "ERROR:  name cannot be empty";
            }
            if (email == "")
            {
                return "ERROR:  email cannot be empty";

            }
            if (telephone == "")
            {
                return "ERROR: telephone cannot be empty";
            }
            if (vendor_name == "")
            {
                return "ERROR: vendor_name cannot be empty";
            }
            if (categories.Count() == 0)
            {
                return "ERROR: categories cannot be empty";
            }

            if (repository.EmailExists(email))
            {
                return "ERROR: Staff with this email already exists";

            }
            Vendor newvendor = new Vendor(vendor_name, categories, "", name, telephone, role, email);
            repository.Add(newvendor);
            return "SUCCESS: Vendor '" + vendor_name + "' added successfully with ID: ??";
        }
        public string AddPerformer(string name, string email, string telephone, string role, string fee, List<string> genres)
        {
            if (name == "")
            {
                return "ERROR: name cannot be empty";
            }
            if (email == "")
            {
                return "ERROR: email cannot be empty";

            }
            if (telephone == "")
            {
                return "ERROR: telephone cannot be empty";
            }
            if (fee == "")
            {
                return "ERROR: fee cannot be empty";
            }

            if (repository.EmailExists(email))
            {
                return "ERROR: Event Staff with this email already exists!";

            }
            Performer newperformer = new Performer(genres, fee, "", name, telephone, role, email);
            repository.Add(newperformer);
            return "SUCCESS: Performer '" + name + role + "' added successfully with ID: ??";
        }
        public string AddCrew(string crew_name, string email, string telephone, string role, string hourly_rate, string weekly_hours, string employment_type)
        {
            if (crew_name == "")
            {
                return "ERROR: Name cannot be empty";
            }
            if (email == "")
            {
                return "ERROR: Email cannot be empty";

            }
            if (telephone == "")
            {
                return "ERROR: telephone cannot be empty";
            }
            if (hourly_rate == "")
            {
                return "ERROR: hourly_rate cannot be empty";
            }

            if (repository.EmailExists(email))
            {
                return "ERROR: Staff with this Email already exists!";
            }
            Crew newcrew = new Crew(hourly_rate, weekly_hours, employment_type, "",  crew_name, telephone, role, email);
            repository.Add(newcrew);
            return "SUCCESS: Crew Member '" + crew_name + "' added successfully with ID: ??";
        }
        public string EditVendor(string name, string email, string telephone, string role, string vendor_name, List<string> categories, string personID)
        {
            if (name == "")
            {
                return "ERROR: name cannot be empty";
            }
            if (email == "")
            {
                return "ERROR: email cannot be empty";

            }
            if (telephone == "")
            {
                return "ERROR: telephone cannot be empty";
            }
            if (vendor_name == "")
            {
                return "ERROR: vendor name cannot be empty";
            }

            if (repository.EmailExists(email))
            {
                return "ERROR: Staff with this email already exists";

            }
            Vendor editedvendor = new Vendor(vendor_name, categories, "", name, telephone, role, email);
            repository.Edit(editedvendor, int.Parse(personID));
            return "SUCCESS: Vendor '" + vendor_name + "' added successfully with ID: ??";
        }
        public string EditPerformer(string name, string email, string telephone, string role, string fee, List<string> genres, string personID)
        {
            if (name == "")
            {
                return "ERROR: performer name cannot be empty";
            }
            if (email == "")
            {
                return "ERROR: email cannot be empty";

            }
            if (telephone == "")
            {
                return "ERROR: telephone cannot be empty";
            }
            if (fee == "")
            {
                return "ERROR: fee cannot be empty";
            }

            if (repository.EmailExists(email))
            {
                return "ERROR: Staff with this email already exists";

            }
            Performer editedperformer = new Performer(genres, fee, "", name, telephone, role, email);
            repository.Edit(editedperformer, int.Parse(personID));
            return "SUCCESS: Performer '" + name + role + "' added successfully with ID: ??";
        }
        public string EditCrew(string crew_name, string email, string telephone, string role, string hourly_rate, string weekly_hours, string employment_type, string personID)
        {
            if (crew_name == "")
            {
                return "ERROR: Name cannot be empty";
            }
            if (email == "")
            {
                return "ERROR: Email cannot be empty";

            }
            if (telephone == "")
            {
                return "ERROR: telephone cannot be empty";
            }
            if (hourly_rate == "")
            {
                return "ERROR: hourly_rate cannot be empty";
            }

            if (repository.EmailExists(email))
            {
                return "ERROR: Staff with this email already exists";

            }
            Crew editedcrew = new Crew(hourly_rate, weekly_hours, employment_type, "", crew_name, telephone, role, email);
            repository.Edit(editedcrew, int.Parse(personID));
            return "SUCCESS: Crew Member '" + crew_name + "' added successfully with ID: ??";
        }
        public List<EventStaff> GetAllStaff()
        {
            return repository.GetAll();
        }
        public List<EventStaff> GetAllVendors()
        {
            return repository.GetAllVendors();
        }
        public List<EventStaff> GetAllPerformers()
        {
            return repository.GetAllPerformers();
        }
        public List<EventStaff> GetAllCrew()
        {
            return repository.GetAllCrew();
        }
        public void delete(int personId)
        {
            repository.Delete(personId);
        }
        public void Edit(EventStaff eventstaff, int personId)
        {
            repository.Edit(eventstaff,  personId);
        }
 
    }
}
