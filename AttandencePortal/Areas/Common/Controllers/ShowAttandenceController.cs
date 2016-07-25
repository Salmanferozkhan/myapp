using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BOL;
using BLL;
using System.Collections;

namespace AttandenceApp.Areas.Common.Controllers
{

    public class ShowAttandenceController : CommonBaseController
    {
        List<EmployeeStats> Empstats = new List<EmployeeStats>();
        public static int TotalWorkingDays(int month, int year)
        {
            //Monday to Friday are business days.
            var weekends = new DayOfWeek[] { DayOfWeek.Saturday, DayOfWeek.Sunday };

            //Fetch the amount of days in your given month.
            int daysInMonth = DateTime.DaysInMonth(year, month);

            //Here we create and enumerable from 1 to daysInMonth,
            //and ask whether the DateTime object we create belongs to a weekend day,
            //if it doesn't, add it to our IEnumerable<int> collection of days.
            IEnumerable<int> businessDaysInMonth = Enumerable.Range(1, daysInMonth)
                                                   .Where(d => !weekends.Contains(new DateTime(year, month, d).DayOfWeek));

            int totalWorkingDays = 0;
            //Pretty smooth. :)
            foreach (var day in businessDaysInMonth)
            {
                totalWorkingDays++;
            }
            return totalWorkingDays;
        }

        public ActionResult Index(DateTime IN)
        {

            var employees = getbyYearEMP(IN);
            return Json(employees, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetEmpByMonth(DateTime IN)
        {
            var employees = getEMP(IN);
            return Json(employees, JsonRequestBehavior.AllowGet);
        }

        public List<EmployeeStats> getEMP(DateTime IN)
        {
            var ids = obj.Employee.GetAllAsNOTracking().Select(x => new
            {
                x.Eid,
                x.tbl_Attandence
            }).ToList();
            List<EmployeeStats> EList = new List<EmployeeStats>();
            foreach (var id in ids)
            {
                //tbl_Attandence M = new tbl_Attandence();
                var M = obj.attendance.GetAllAsNOTracking().Where(x => x.Eid == Convert.ToInt32(id.Eid)).First().Eid;
                if (M != null)
                {
                    EmployeeStats m = new EmployeeStats();

                    //calculation

                    //m.EName = M.tbl_Employee.EName;

                    //m.ESalary = M.tbl_Employee.ESalary;
                    //m.DateOfJoining = M.tbl_Employee.DateOfJoining;

                    var Emp = obj.Employee.GetAllAsNOTracking().Where(x => x.Eid == M).Select(x => new EmployeeStats
                    {
                        EName = x.EName,
                        ESalary = x.ESalary
                    }).ToList();

                    m.Eid = Convert.ToInt32(M);
                    // m.EName=Emp
                    var time = getTotalTime(Convert.ToInt32(id.Eid), IN);
                    foreach (var item in Emp)
                    {
                        m.EName = item.EName;
                        m.ESalary = item.ESalary;
                    }
                    m.StandartHours = time[0].ToString();
                    m.EmployeWorked = time[1].ToString();
                    m.EmployeeHolidays = time[2].ToString();

                    EList.Add(m);
                }

            }

            return EList;

        }
        public List<EmployeeStats> getbyYearEMP(DateTime IN)
        {
            var ids = obj.Employee.GetAllAsNOTracking().Select(x => new
            {
                x.Eid
            }).ToList();
            List<EmployeeStats> EList = new List<EmployeeStats>();
            foreach (var id in ids)
            {
                //tbl_Attandence M = new tbl_Attandence();
                var M = obj.attendance.GetAllAsNOTracking().Where(x => x.Eid == Convert.ToInt32(id.Eid)).First().Eid;
                if (M != null)
                {
                    EmployeeStats m = new EmployeeStats();

                    //calculation

                    //m.EName = M.tbl_Employee.EName;
                    m.Eid = Convert.ToInt32(M);
                    //m.ESalary = M.tbl_Employee.ESalary;
                    //m.DateOfJoining = M.tbl_Employee.DateOfJoining;
                    int totalWorkingDays = 0;
                    int year = DateTime.Now.Year;
                    for (int i = 1; i < 13; i++)
                    {
                        totalWorkingDays = totalWorkingDays + (TotalWorkingDays(i, year));
                    }
                    var time = getTotalTimeByYear(Convert.ToInt32(id.Eid), IN, totalWorkingDays);
                    var Emp = obj.Employee.GetAllAsNOTracking().Where(x => x.Eid == M).Select(x => new EmployeeStats
                    {
                        EName = x.EName,
                        ESalary = x.ESalary
                    }).ToList();
                    foreach (var item in Emp)
                    {
                        m.EName = item.EName;
                        m.ESalary = item.ESalary;
                    }
                    m.StandartHours = time[0].ToString();
                    m.EmployeWorked = time[1].ToString();
                    m.EmployeeHolidays = time[2].ToString();
                    EList.Add(m);
                }

            }

            return EList;

        }

        public List<double> getTotalTime(int Eid, DateTime byMonth)
        {
            double employeesWorkingMin = 0;
            List<double> list = new List<double>();
            var inn = obj.attendance.GetInByMonth(Eid, byMonth);
            var outt = obj.attendance.GetOUTByMonth(Eid, byMonth);
            for (int i = 0; i < outt.Count(); i++)
            {
                DateTime starting = (DateTime)inn[i];
                string startTime = starting.ToString("HH:mm");
                DateTime end = (DateTime)outt[i];
                string endTime = end.ToString("HH:mm");

                TimeSpan duration = DateTime.Parse(endTime).Subtract(DateTime.Parse(startTime));

                employeesWorkingMin = employeesWorkingMin + duration.TotalMinutes;

            }

            DateTime now = DateTime.Now;
            int totalWorkingDays = TotalWorkingDays(now.Month, now.Year);
            double standardHours = totalWorkingDays * 9;

            TimeSpan span = TimeSpan.FromHours(standardHours);
            double standardMin = span.TotalMinutes;

            double calculatedEmployeWorktime = employeesWorkingMin - standardMin;


            span = TimeSpan.FromMinutes(calculatedEmployeWorktime);
            double totalHours = span.TotalHours;
            double holidays = 0;
            //if (totalHours > 0)
            //{
            TimeSpan holiday = TimeSpan.FromHours(totalHours);
            holidays = holiday.TotalDays;
            //}
            //else
            //{
            //    holidays = totalHours + standardHours;
            //}

            //double holidays = totalHours - standardHours;
            //TimeSpan holidayss = TimeSpan.FromHours(holidays);
            //holidays = Convert.ToDouble(holidayss.TotalDays);
            list.Add(standardHours);
            list.Add(totalHours);
            list.Add(holidays);

            return list;

        }
        public List<double> getTotalTimeByYear(int Eid, DateTime byYear, double totalWorkingDays)
        {
            double employeesWorkingMin = 0;
            List<double> list = new List<double>();
            var inn = obj.attendance.GetInbyYearData(Eid, byYear);
            var outt = obj.attendance.GetOUTbyYearData(Eid, byYear);
            for (int i = 0; i < outt.Count(); i++)
            {
                DateTime starting = (DateTime)inn[i];
                string startTime = starting.ToString("HH:mm");
                if (outt[i] != null)
                {
                    DateTime end = (DateTime)outt[i];
                    string endTime = end.ToString("HH:mm");

                    TimeSpan duration = DateTime.Parse(endTime).Subtract(DateTime.Parse(startTime));

                    employeesWorkingMin = employeesWorkingMin + duration.TotalMinutes;
                }
                continue;

            }

            DateTime now = DateTime.Now;

            double standardHours = totalWorkingDays * 9;

            TimeSpan span = TimeSpan.FromHours(standardHours);
            double standardMin = span.TotalMinutes;

            double calculatedEmployeWorktime = employeesWorkingMin - standardMin;

            span = TimeSpan.FromMinutes(calculatedEmployeWorktime);
            double totalHours = span.TotalHours;
            double holidays = 0;
            //if (totalHours > 0)
            //{
            TimeSpan holiday = TimeSpan.FromHours(totalHours);
            holidays = holiday.TotalDays;
            //}
            //else
            //{
            //    holidays = totalHours + standardHours;
            //}

            //double holidays = totalHours - standardHours;
            //TimeSpan holidayss = TimeSpan.FromHours(holidays);
            //holidays = Convert.ToDouble(holidayss.TotalDays);
            list.Add(standardHours);
            list.Add(totalHours);
            list.Add(holidays);
            return list;
        }
        //public List<DateTime> GetProdcutName()
        //{

        //    using (var db = new EmployeeEntities())
        //    {
        //        var result = from productlist in db.tbl_Attandence
        //                     select productlist.IN;
        //        return result.ToList() as DateTime;
        //    }

        //}
        //// GET: Common/ShowAttandence
        //public List<double> getTotalTime(int Eid)
        //{
        //    double employeesWorkingMin = 0;
        //    List<double> list = new List<double>();
        //    var inn = obj.attendance.GetInData(Eid); ;
        //    var outt = obj.attendance.GetOUTData(Eid);
        //    for (int i = 0; i < inn.Count(); i++)
        //    {
        //        DateTime starting = (DateTime)inn[i];
        //        string startTime = starting.ToString("HH:mm");
        //        DateTime end = (DateTime)outt[i];
        //        string endTime = end.ToString("HH:mm");

        //        TimeSpan duration = DateTime.Parse(endTime).Subtract(DateTime.Parse(startTime));

        //        employeesWorkingMin = employeesWorkingMin + duration.TotalMinutes;
        //        //var salman = obj.attendance.GetAll().Where(x => x.Eid == 3).ToList();
        //    }

        //    DateTime now = DateTime.Now;
        //    int totalWorkingDays = TotalWorkingDays(now.Month, now.Year);
        //    double standardHours = totalWorkingDays * 9;

        //    TimeSpan span = TimeSpan.FromHours(standardHours);
        //    double standardMin = span.TotalMinutes;

        //    double calculatedEmployeWorktime = employeesWorkingMin - standardMin;

        //    span = TimeSpan.FromMinutes(calculatedEmployeWorktime);
        //    double totalHours = span.TotalHours;

        //    double holidays = totalHours - standardHours;
        //    TimeSpan holidayss = TimeSpan.FromDays(holidays);
        //    holidays = Convert.ToDouble(holidayss.TotalDays);
        //    list.Add(standardHours);
        //    list.Add(totalHours);
        //    list.Add(holidays);
        //    return list;

        //}

    }
}