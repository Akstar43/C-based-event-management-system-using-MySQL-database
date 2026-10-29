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
                return "ERROR: Title cannot be empty";
            }
            if (email == "")
            {
                return "ERROR: Author cannot be empty";

            }
            if (telephone == "")
            {
                return "ERROR: ISBN cannot be empty";
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
                return "ERROR: Book with this ISBN already exists!";

            }
            Vendor newvendor = new Vendor(vendor_name, categories, "", name, telephone, role, email);
            repository.Add(newvendor);
            return "SUCCESS: Vendor '" + vendor_name + "' added successfully with ID: ??";
        }
        public string AddPerformer(string name, string email, string telephone, string role, string fee, List<string> genres)
        {
            if (name == "")
            {
                return "ERROR: Title cannot be empty";
            }
            if (email == "")
            {
                return "ERROR: Author cannot be empty";

            }
            if (telephone == "")
            {
                return "ERROR: ISBN cannot be empty";
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
        public string AddCrew(string crew_name, string email, string telephone, string role, string hourly_rate, int weekly_hours, string employment_type)
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
                return "ERROR: Person with this Email already exists!";
            }
            Crew newcrew = new Crew(hourly_rate, weekly_hours, employment_type, "",  crew_name, telephone, role, email);
            repository.Add(newcrew);
            return "SUCCESS: Crew Member '" + crew_name + "' added successfully with ID: ??";
        }
        public List<EventStaff> GetAllStaff()
        {
            return repository.GetAll();
        }

    }
}
