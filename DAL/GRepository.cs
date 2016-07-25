using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;

namespace DAL
{
    public class GRepository<T> : Builder<T> where T : class
    {
        EmployeeEntities obj;


        public override IEnumerable<T> GetAll()
        {
            using (obj = new EmployeeEntities())
            {
                return obj.Set<T>().ToList();
            }


        }
        public override IEnumerable<T> GetAllAsNOTracking()
        {
            using (obj = new EmployeeEntities())
            {
                return obj.Set<T>().AsNoTracking().ToList();
            }
        }
        //public override tbl_Attandence GetByAttByEid(int? Eid)
        //{
        //    using (obj = new EmployeeEntities())
        //    {
        //        return obj.Set<tbl_Attandence>().Where(x => x.Eid == Eid).ToList();
        //    }
        //}
        public override T GetById(int? id)
        {
            using (obj = new EmployeeEntities())
            {
                return obj.Set<T>().Find(id);
            }
        }
        //public List<latestOrders_Result> latestOrders()
        //{
        //    return obj.latestOrders().ToList();
        //}

        public override void Insert(T data)
        {
            using (obj = new EmployeeEntities())
            {
                obj.Set<T>().Add(data);
                obj.Configuration.ValidateOnSaveEnabled = false;
                Save();
                obj.Configuration.ValidateOnSaveEnabled = true;
            }
        }
        public override void Update(T data)
        {
            using (obj = new EmployeeEntities())
            {
                obj.Entry(data).State = EntityState.Modified;
                obj.Configuration.ValidateOnSaveEnabled = false;
                Save(data);
                obj.Configuration.ValidateOnSaveEnabled = true;
            }
        }
        public override void Delete(int id)
        {
            using (obj = new EmployeeEntities())
            {
                var d = obj.Set<T>().Find(id);
                obj.Set<T>().Remove(d);
                Save();
            }

        }
        public override List<DateTime?> GetInByMonth(int Eid, DateTime INN)
        {
            using (obj = new EmployeeEntities())
            {

                var result = from productlist in obj.tbl_Attandence.AsNoTracking()
                             where productlist.Eid == Eid && (productlist.IN.Value.Month == INN.Month && productlist.IN.Value.Year == INN.Year)
                             select productlist.IN;
                return result.ToList();
            }
        }
        public override List<DateTime?> GetOUTByMonth(int Eid, DateTime OUT)
        {
            using ( obj = new EmployeeEntities())
            {
                var result = from productlist in obj.tbl_Attandence.AsNoTracking()
                             where productlist.Eid == Eid && (productlist.OUT.Value.Month == OUT.Month && productlist.IN.Value.Year == OUT.Year)
                             select productlist.OUT;
                return result.ToList();
            }
        }
        public override List<DateTime?> GetInbyYearData(int Eid, DateTime INN)
        {
            using (obj = new EmployeeEntities())
            {
                var result = from productlist in obj.tbl_Attandence.AsNoTracking()
                             where productlist.Eid == Eid && productlist.IN.Value.Year == INN.Year
                             select productlist.IN;
                return result.ToList();
            }
        }
        public override List<DateTime?> GetOUTbyYearData(int Eid, DateTime OUTT)
        {
            using (obj = new EmployeeEntities())
            {
                var result = from productlist in obj.tbl_Attandence.AsNoTracking()
                             where productlist.Eid == Eid && productlist.IN.Value.Year == OUTT.Year
                             select productlist.OUT;
                return result.ToList();
            }
        }
        public IQueryable<EmptodayModel> GetEmployeeToday()
        {  obj = new EmployeeEntities();
            TimeZoneInfo timeZoneInfo;
            //Set the time zone information to US Mountain Standard Time 
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Pakistan Standard Time");
            //Get date and time in US Mountain Standard Time 
            DateTime now = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);

            
                var employeetoday = from atd in obj.tbl_Attandence
                                    join e in obj.tbl_Employee on atd.Eid equals e.Eid
                                    where (DbFunctions.TruncateTime(atd.IN) == DbFunctions.TruncateTime(now))
                                    select new EmptodayModel
                                    {
                                        AID = atd.AID.ToString(),
                                        Eid = atd.Eid.ToString(),
                                        EName = e.EName,
                                        IN = (DateTime?)atd.IN,
                                        OUT = (DateTime?)atd.OUT
                                    };
                return employeetoday;
            
        }

        //public override tbl_Attandence getTodayEmp()
        //{

        //    var e = obj.Set<tbl_Attandence>().ToList().Where(x => x.IN.Value.Date == now.Date).Select(a => new getEmployeeToday
        //    {
        //        Ename = a.tbl_Employee.EName,
        //        IN = a.IN,
        //        OUT = a.OUT
        //    }).ToList();
        //    return e;

        //}
        public override void Save(T data)
        {
            if (obj==null)
            {
                obj = new EmployeeEntities();
            }
           
                try
                {
                    obj.SaveChanges();

                }
                catch (DbUpdateConcurrencyException E)
                {
                    ((IObjectContextAdapter)obj).ObjectContext
                        .Refresh(RefreshMode.ClientWins, data);
                    obj.SaveChanges();
                
            }
        }
        public override void Save()
        {
          if(obj==null)
            {
                obj = new EmployeeEntities();
            }
            obj.SaveChanges();

        }

        //public override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        obj.Dispose();
        //    }

    }

    //public override orderDetailModel orderDetails(int id)
    //{
    //    var results = from o in obj.Orders

    //var results = from atd in db.tbl_Attandence
    // join e in obj.tbl_Employees on atd.Eid equals e.Eid
    //where(
    //                  join od in obj.tbl_OrderDetails on o.OrderID equals od.OrderID
    //                  join oh in obj.tbl_OrderHistory on o.OrderID equals oh.OrderID
    //                  join l in obj.tbl_Login on o.UserID equals l.UserID
    //                  join os in obj.tbl_OrderStatus on oh.OrderStatusID equals os.OrderStatusID
    //                  where (od.OrderID == id)
    //                  group new { o, l, os } by new
    //                  {
    //                      o.OrderID,
    //                      o.Nature,
    //                      od.TotalPrice,
    //                      o.Date,
    //                      os.OrderStatus,
    //                      l.FirstName,
    //                      l.EmailAddress,
    //                      l.PhoneNumber
    //                  } into g
    //                  select new orderDetailModel
    //                  {
    //                      OrderID = g.Key.OrderID,
    //                      OrderStatus = g.Key.OrderStatus,
    //                      Date = g.Key.Date,
    //                      TotalPrice = g.Key.TotalPrice,
    //                      FirstName = g.Key.FirstName,
    //                      EmailAddress = g.Key.EmailAddress,
    //                      PhoneNumber = g.Key.PhoneNumber,
    //                      DeliveryNature = g.Key.Nature

    //                  };
    //    //group o by new {o.OrderID};
    //    orderDetailModel data = (orderDetailModel)results.FirstOrDefault();
    //    return data;
    //}
}

