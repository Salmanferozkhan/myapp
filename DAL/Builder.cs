using BOL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public abstract class Builder<T> where T : class
    {
        public abstract IEnumerable<T> GetAll();
        public abstract IEnumerable<T> GetAllAsNOTracking();

        //public abstract tbl_Attandence GetByAttByEid(int? Eid);
        
        public abstract T GetById(int? id);
        public abstract void Insert(T data);
        public abstract void Update(T data);
        public abstract void Delete(int id);
        public abstract void Save(T data);
        public abstract void Save();
        public abstract List<DateTime?> GetInByMonth(int Eid, DateTime INN);
        public abstract List<DateTime?> GetOUTByMonth(int Eid, DateTime INN);
        public abstract List<DateTime?> GetInbyYearData(int Eid, DateTime INN);
        public abstract List<DateTime?> GetOUTbyYearData(int Eid, DateTime OUTT);
    }
}
