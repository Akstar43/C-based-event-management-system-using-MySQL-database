using System;
using System.Collections.Generic;
using System.Text;

namespace musicfestivalmanagementapp
{
    internal class EventStaffManagementUI : IEventStaffManagementView
    {
        private EventStaffManagementManager manager;
        public EventStaffManagementUI(EventStaffManagementManager manager)
        {
            this.manager = manager;
        }
        public void Run()
        {
            Console.WriteLine("Welcome to Event Staff Management System");
            bool running = true;
            while (running)
            {
                DisplayMenu();

                Console.Write("Enter Choice (1-5)");
                if (int.TryParse(Console.ReadLine(), out int option))
                {
                    switch (option)
                    {
                        case 1:
                            Console.WriteLine("1. Add Vendor");
                            Console.WriteLine("2. Add Performer");
                            Console.WriteLine("3. Add Crew");
                            Console.WriteLine("Enter Choice: ");
                            if (int.TryParse(Console.ReadLine(), out int selectedoption))
                            {
                                switch (selectedoption)
                                {
                                    case 1:
                                        AddVendor();
                                        break;
                                    case 2:
                                        AddPerformer();
                                        break;
                                    case 3:
                                        AddCrew();
                                        break;
                                    default:
                                        Console.WriteLine("Invalid Choice");
                                        break;
                                }
                            }
                            break;
                        case 2:
                            ViewAllRecords();
                            break;
                        case 3:
                            Console.WriteLine("1. Add Vendor");
                            Console.WriteLine("2. Add Performer");
                            Console.WriteLine("3. Add Crew");
                            Console.WriteLine("Enter Choice: ");
                            if (int.TryParse(Console.ReadLine(), out int selectoption))
                            {
                                switch (selectoption)
                                {
                                    case 1:
                                        ViewAllVendors();
                                        break;
                                    case 2:
                                        ViewAllPerformers();
                                        break;
                                    case 3:
                                        ViewAllCrew();
                                        break;
                                    default:
                                        Console.WriteLine("Invalid Choice");
                                        break;
                                }
                            }
                            break;
                        case 4:
                            EditRecords();
                            break;
                        case 5:
                            DeleteRecords();
                            break;
                        default:
                            Console.WriteLine("Invalid Input Entered");
                            running = false;
                            break;
                    }
                }

            }
        }
        public void AddVendor()
        {
            List<string> categories = new List<string>();

            Console.WriteLine("Add new Vendor");
            Console.WriteLine("Vendor Name");
            string name = Console.ReadLine();
            Console.WriteLine("Vendor Telephone");
            string telephone = Console.ReadLine();
            Console.WriteLine("Vendor email");
            string email = Console.ReadLine();
            Console.WriteLine("Vendor Company name");
            string vendor_name = Console.ReadLine();
            int option;
            do
            {
                Console.WriteLine("Add Vendor Categories");
                string category = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(category))
                {
                    categories.Add(category);
                }
                Console.Write("Press 1 to add another category or 0 to finish");
                int.TryParse(Console.ReadLine(), out option);
            } while (option != 0);
            string role = "Vendor";

            string result = manager.AddVendor(name, email, telephone, role, vendor_name, categories);
            Console.WriteLine($"\n{result}");

        }
        public void AddPerformer()
        {
            List<string> genres = new List<string>();
            Console.WriteLine("Add new Performer");
            Console.WriteLine("person Name");
            string name = Console.ReadLine();
            Console.WriteLine("Performer Telephone");
            string telephone = Console.ReadLine();
            Console.WriteLine("Performer email");
            string email = Console.ReadLine();
            Console.WriteLine("Performer Fee");
            string fee = Console.ReadLine();
            int option;
            do
            {
                Console.WriteLine("Add Performer Genre");
                string genre = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(genre))
                {
                    genres.Add(genre);
                }
                Console.Write("Press 1 to add another genre or 0 to finish");
                int.TryParse(Console.ReadLine(), out option);
            } while (option != 0);


            string role = "Performer";

            string result = manager.AddPerformer(name, email, telephone, role, fee, genres);
            Console.WriteLine($"\n{result}");

        }
        public void AddCrew()
        {
            string employment_type = "";
            string employment_type_options = "";
            string weekly_hours_options = "";
            int weekly_hours = 0;
            Console.WriteLine("Add new Crew Member");
            Console.WriteLine("Crew Member Name");
            string name = Console.ReadLine();
            Console.WriteLine("Crew Member Telephone");
            string telephone = Console.ReadLine();
            Console.WriteLine("Crew Member email");
            string email = Console.ReadLine();
            Console.WriteLine("Crew Member Hourly rate ($)");
            string hourly_rate = Console.ReadLine();
            while (employment_type_options != "1" && employment_type_options != "2")
            {
                Console.WriteLine("Employment type \n 1. Full Time\n 2. Part time\n");
                employment_type_options = Console.ReadLine();

                if (employment_type_options == "1")
                {
                    employment_type = "Full Time";
                }
                else if (employment_type_options == "2")
                {
                    employment_type = "Part Time";
                }
                else
                {
                    Console.WriteLine("Invalid selection. Please try again.\n");
                }

            }
            while (weekly_hours == 0)
            {
                Console.WriteLine("Weekly hours");
                if (int.TryParse(Console.ReadLine(), out int weekly_hours_option))
                {
                    if (employment_type == "Full Time" && (weekly_hours_option >= 25 && weekly_hours_option <= 40))
                    {
                        weekly_hours = weekly_hours_option;
                    }
                    else if (employment_type == "Part Time" && (weekly_hours_option >= 1 && weekly_hours_option <= 24))
                    {
                        weekly_hours = weekly_hours_option;
                    }
                    else
                    {
                        Console.WriteLine($"Invalid hours for {employment_type}. Please try again.\n");
                    }


                }
            }
            string role = "Crew";

            string result = manager.AddCrew(name, email, telephone, role, hourly_rate, weekly_hours, employment_type);
            Console.WriteLine($"\n{result}");

        }
        private void ViewAllPerformers()
        {

        }
        private void ViewAllCrew()
        {

        }
        private void ViewAllVendors()
        {

        }
        private void EditRecords()
        {

        }
        private void DeleteRecords()
        {

        }
        private void ViewAllRecords()
        {
            Console.WriteLine("\n=== ALL STAFF MEMBERS ===");

            // Get all items from manager
            List<EventStaff> staff = manager.GetAllStaff();
            if (staff.Count <= 0)
            {
                Console.WriteLine("No Staff in system");
                return;
            }
            var count = 0;
            foreach (var people in staff)
            {
                Console.WriteLine($"Number: {count + 1}");
                people.DisplayInfo();
                Console.WriteLine();
                count++;

            }



        }
        public void DisplayMenu()
        {
            Console.WriteLine("\n== Main Menu");
            Console.WriteLine("1. Add New Record");
            Console.WriteLine("2. View all records");
            Console.WriteLine("3. View by role (Performer only/ Crew Only / Vendor Only)");
            Console.WriteLine("4. Edit an Existing record by PersonID");
            Console.WriteLine("5. Delete an Existing record by PersonID");
        }
    }
}

