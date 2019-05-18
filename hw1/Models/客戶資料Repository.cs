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
        /// <summary>
        /// 覆寫 All() 只取出 [刪除] 欄位為 0 的紀錄
        /// </summary>
        /// <returns></returns>
        public override IQueryable<客戶資料> All()
        {
            return base.All().Where(p => p.刪除 == false);
        }
    }

    public interface I客戶資料Repository : IRepository<客戶資料>
    {

    }
}