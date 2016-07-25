using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public abstract class BaseBs
    {
        public BLL.IRepository<BOL.tbl_Attandence> attendance { get; set; }
        public BLL.IRepository<BOL.tbl_Employee> Employee { get; set; }
        public BLL.IRepository<BOL.tbl_Department> Department { get; set; }

        public BaseBs()
        {

            attendance = new IRepository<BOL.tbl_Attandence>();
            Employee = new IRepository<BOL.tbl_Employee>();
            Department = new IRepository<BOL.tbl_Department>();
        }
    }
}
