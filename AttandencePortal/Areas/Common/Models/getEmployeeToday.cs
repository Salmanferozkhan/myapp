using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AttandencePortal.Areas.Common.Models
{
    public class getEmployeeToday
    {
       
        public string Ename { get; set; }

        public DateTime? IN { get; set; }
        public DateTime? OUT { get; set; }
    }
}