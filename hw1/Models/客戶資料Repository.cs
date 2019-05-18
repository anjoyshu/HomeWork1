using System;
using System.Linq;
using System.Collections.Generic;

namespace hw1.Models
{
    public class 客戶資料Repository : EFRepository<客戶資料>, I客戶資料Repository
    {
        public 客戶資料 Find(int id)
        {
            return this.All().Where(p => p.Id == id).FirstOrDefault();
        }
        
        public IQueryable<客戶資料> Search(string searchString)
        {
            return this.All().Where(p => p.客戶名稱.Contains(searchString));
        }

        public 客戶資料 IsExist(string Email)
        {
            return this.All().Where(p => p.Email == Email).SingleOrDefault();
        }

    }

    public interface I客戶資料Repository : IRepository<客戶資料>
    {

    }
}