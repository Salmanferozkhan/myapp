using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AttandencePortal.Areas.Common.Controllers
{
    public class EmployeesController : AttandenceApp.Areas.Common.Controllers.CommonBaseController
    {
        // GET: Common/Employees
        public ActionResult Index()
        {
            return View();
            //return Json(obj.Employee.GetAll().ToList(), JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult GetAllEmployees()
        {
            return Json(obj.Employee.GetAll().ToList(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetEmpByDid(int Did)
        {
            return Json(obj.Employee.GetAll().Where(x => x.Did == Did).ToList(), JsonRequestBehavior.AllowGet);
        }
        // GET: Common/Employees/Details/5
        public ActionResult Details(int Eid)
        {
            BOL.tbl_Employee Emp = obj.Employee.GetById(Eid);
            return Json(Emp, JsonRequestBehavior.AllowGet);
        }


        // POST: Common/Employees/Create
        [HttpPost]
        public ActionResult Create(BOL.tbl_Employee Emp)
        {
            try
            {
                obj.Employee.Insert(Emp);
                return Json(Emp, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        // POST: Common/Employees/Edit/5
        [HttpPost]
        public ActionResult Edit(BOL.tbl_Employee Emp)
        {
            try
            {
                obj.Employee.Update(Emp);
                return Json(Emp.EName, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        // POST: Common/Employees/Delete/5
        [HttpPost]
        public ActionResult Delete(int Eid)
        {
            try
            {
                obj.Employee.Delete(Eid);
                return Json(Eid, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
