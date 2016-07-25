using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using DAL;

namespace BLL
{
    public class IRepository<T> where T : class
    {
        GRepository<T> rep = new GRepository<T>();
        public IEnumerable<T> GetAll()
        {
            return rep.GetAll();
        }
        public IEnumerable<T> GetAllAsNOTracking()
        {
            return rep.GetAllAsNOTracking();
        }
        public T GetById(int? id)
        {

            return rep.GetById(id);
        }
        public void Insert(T data)
        {
            rep.Insert(data);
        }
        public void Update(T data)
        {
            rep.Update(data);
        }
        public void Delete(int id)
        {
            rep.Delete(id);

        }
        public List<DateTime?> GetInByMonth(int Eid, DateTime INN)
        {
            return rep.GetInByMonth(Eid, INN);
        }
        public List<DateTime?> GetOUTByMonth(int Eid, DateTime OUT)
        {
            return rep.GetOUTByMonth(Eid, OUT);
        }
        public List<DateTime?> GetInbyYearData(int Eid, DateTime INN)
        {
            return rep.GetInbyYearData(Eid, INN);
        }
        public List<DateTime?> GetOUTbyYearData(int Eid, DateTime OUTT)
        {
            return rep.GetOUTbyYearData(Eid, OUTT);
        }
        public IQueryable<EmptodayModel> GetEmployeeToday()
        {
           return rep.GetEmployeeToday();
        }

        //public orderDetailModel orderDetails(int id)
        //{
        //    orderDetailModel data = rep.orderDetails(id);
        //    return data;
        //}
        //public List<BOL.latestOrders_Result> latestOrders()
        //{
        //    return rep.latestOrders();
        //}
    }
}
