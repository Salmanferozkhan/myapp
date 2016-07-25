using AttandencePortal.Areas.Common.Models;
using BOL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;

namespace AttandenceApp.Areas.Common.Controllers
{
    public class HomeController : CommonBaseController
    {
        DateTime now;
        tbl_Attandence att = new tbl_Attandence();

        // GET: Common/Home
        public ActionResult Index()
        {

            return View();
        }
        public ActionResult GetEmployees()
        {
            return Json(obj.Employee.GetAll().ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetEmpToday()
        {
            var emp = obj.attendance.GetEmployeeToday();

            //var emp = obj.attendance.GetAll().Where(x => x.IN.Value.Date == now.Date).Select(x => new
            //{
            //    x.AID,
            //    x.Eid,
            //    x.IN,
            //    x.OUT
            //}).ToList();
            return Json(emp, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public ActionResult EmployeeIN(DateTime IN, string e)
        {

            TimeZoneInfo timeZoneInfo;
            //Set the time zone information to US Mountain Standard Time 
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Pakistan Standard Time");
            //Get date and time in US Mountain Standard Time 
            now = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);


            int Count = obj.attendance.GetAll().Where(x => x.IN.Value.Date == now.Date && x.Eid == Convert.ToInt32(e)).Count();
            if (IN != null || e != null)
            {
                if (Count <= 0)
                {
                    att.IN = TimeZoneInfo.ConvertTime(IN, timeZoneInfo);
                    att.Eid = Convert.ToInt32(e);
                    obj.attendance.Insert(att);
                    e = e + " Employee successfully IN";
                }
                else
                {
                    e = e + " Employee already IN";
                }
            }

            return Json(e, JsonRequestBehavior.AllowGet);
            //obj.attendance.Insert(att);
            //return Json(att, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult EmployeeOUT(DateTime OUT, string Eid)
        {
            TimeZoneInfo timeZoneInfo;
            //Set the time zone information to US Mountain Standard Time 
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Pakistan Standard Time");
            //Get date and time in US Mountain Standard Time 
            now = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);

            if (OUT != null || Eid != null)
            {
                List<tbl_Attandence> data = new List<tbl_Attandence>();
                string time = OUT.ToString("dd/MM/yyyy HH:mm:ss");
                DateTime n = DateTime.ParseExact(time,
                                     "dd/MM/yyyy HH:mm:ss", new CultureInfo("en-US"));
                data = obj.attendance.GetAll().Where(x => x.IN.Value.Date == now.Date && x.Eid == Convert.ToInt32(Eid)).ToList();
                int aid = data.FirstOrDefault().AID;
                att = obj.attendance.GetById(aid);
                int Count = data.Count;
                if (Count > 0)
                {
                    att.OUT = TimeZoneInfo.ConvertTime(n, timeZoneInfo);
                    obj.attendance.Update(att);
                }
            }

            return Json( JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult DeleteEmployee(int Aid)
        {

            obj.attendance.Delete(Aid);
            return Json(Aid, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Details(int Aid)
        {
            tbl_Attandence Emp = obj.attendance.GetById(Aid);
            return Json(Emp, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Edit(tbl_Attandence data)
        {
            obj.attendance.Update(data);
            return Json(data.Eid, JsonRequestBehavior.AllowGet);
        }
    }
}
