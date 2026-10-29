using System;
using System.Collections.Generic;
using System.Text;

namespace musicfestivalmanagementapp
{
    internal class Crew : EventStaff
    {
        public string HourlyRate { get; set; }
        public string EmploymentType { get; set; }
        public int WeeklyHours { get; set; }
        public Crew(string hourly_rate, int weekly_hours, string employment_type, string personid, string name, string telephone, string role, string email) : base(personid, name, telephone, email, role)
        {
            HourlyRate = hourly_rate;
            EmploymentType = employment_type;
            WeeklyHours = weekly_hours;
        }
        public override string GetCategory()
        {
            return "Crew";

        }
        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"Hourly Rate {HourlyRate}");
            Console.WriteLine($"Employment type {EmploymentType}");
            Console.WriteLine($"Weekly Hours {WeeklyHours}");

        }

    }
}
