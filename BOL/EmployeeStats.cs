using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL
{
    public class EmployeeStats
    {
        public int Eid { get; set; }
        public string EName { get; set; }
        public Nullable<decimal> ESalary { get; set; }
        public Nullable<int> Did { get; set; }
        public string EGender { get; set; }
        public Nullable<System.DateTime> DateOfJoining { get; set; }
        public string StandartHours { get; set; }
        public string EmployeWorked { get; set; }
        public string EmployeeHolidays { get; set; }
    }
}
