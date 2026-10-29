using System;


namespace musicfestivalmanagementapp
{
    //Initialization of the parent abstract class EventStaff containing properties each event staff must have.
    internal abstract class EventStaff
    {
        //To uniquely identify EventStaff
        public string PersonID { get; set; }
        //Personal Information for reference purposes
        public string Name { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        //They are 3 distinct roles performer, vendor and crew
        public string Role { get; set; }
        //Event Staff contructor with parameters which each Event Staff must have regardless of role
        public EventStaff(string personId, string name, string telephone, string email, string role) {
            PersonID = personId;
            Name = name;
            Telephone = telephone;
            Email = email;
            Role = role;

        }
        //abstract function which can be overrided by children classes returning string datatype
        public abstract string GetCategory();
        //Displays information about each Event Staff can be Overriden by Children Classes 
        public virtual void DisplayInfo()
        {
            Console.WriteLine($"ID: {PersonID}");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Telephone: { Telephone}");
            Console.WriteLine($"Email: {Email}");
            Console.WriteLine($"Role: { Role }");


        }
    }
}
