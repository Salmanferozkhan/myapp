using AttandenceApp.Areas.Common.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AttandencePortal.Areas.Common.Controllers
{
    public class DepartmentController : CommonBaseController
    {
        // GET: Common/Department
        public ActionResult Index()
        {
            return View();
        }

        // GET: Common/Department/Details/5
        public ActionResult GetAllDepartments()
        {
            return Json(obj.Department.GetAll().ToList(), JsonRequestBehavior.AllowGet);
        }

        // GET: Common/Employees/Details/5
        public ActionResult Details(int Did)
        {
            BOL.tbl_Department dept = obj.Department.GetById(Did);
            return Json(dept, JsonRequestBehavior.AllowGet);
        }
        // POST: Common/Department/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Common/Department/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Common/Department/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Common/Department/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Common/Department/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
