using System;
using System.Collections.Generic;
using System.Text;

namespace musicfestivalmanagementapp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IEventStaffManagementRepository repository = new EventStaffManagementMySqlRepository(DbConfig.ConnectionString);
            EventStaffManagementManager manager = new EventStaffManagementManager(repository);
            IEventStaffManagementView view = new EventStaffManagementUI(manager);
            view.Run();


        }
    }
}
