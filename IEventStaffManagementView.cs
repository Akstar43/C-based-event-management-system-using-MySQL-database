using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace musicfestivalmanagementapp
{
    internal interface IEventStaffManagementView
    {
        void Run();

        // UI helper methods (you will map these to Console.WriteLine / ReadLine)
        void DisplayMenu();
    }
}
