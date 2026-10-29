using System;
using System.Collections.Generic;
using System.Text;

namespace musicfestivalmanagementapp
{
    internal class Performer : EventStaff
    {
        public string Fee { get; set; }
        public List<string> Genres = new List<string>();
        public Performer(List<string> genre, string fee, string personid, string name, string telephone, string role, string email) : base(personid, name, telephone, email, role)
        {
            Fee = fee;
            Genres = genre;
        }
        public override string GetCategory()
        {
            return "Performer";
        }
        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"Fee ${Fee}");
            for (int i = 0; i < Genres.Count; i++)
            {
                Console.WriteLine(Genres[i]);
            }


        }
    }
}
