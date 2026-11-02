using System;
using System.Collections.Generic;
using System.Text;

namespace musicfestivalmanagementapp
{
    internal class Vendor : EventStaff
    {
        public string VendorName { get; set; }
        public List<string> Categories = new List<string>();
        public Vendor(string vendorname, List<string> categories, string personid, string name, string telephone, string role, string email) : base(personid, name, telephone, email, role)
        {
            VendorName = vendorname;
            Categories = categories;
        }
        public override string GetCategory()
        {
            return "Vendor";
        }
        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"Vendor Company Name: {VendorName}");
            Console.WriteLine("Vendor Categories");
            for (int i = 0; i < Categories.Count; i++)
            {
                Console.WriteLine(i + 1);
                Console.WriteLine(Categories[i]);
            }


        }
    }
}
