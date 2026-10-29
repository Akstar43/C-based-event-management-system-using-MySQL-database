using System;
using System.Collections.Generic;
using System.Text;

namespace musicfestivalmanagementapp
{
    internal interface IEventStaffManagementRepository
    {
        void Add(EventStaff eventstaff);
        List<EventStaff> GetAll();
        bool EmailExists(string email);
        void Delete(string PersonID);

    }

}
