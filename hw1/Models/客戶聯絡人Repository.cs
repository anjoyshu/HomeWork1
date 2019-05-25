using System;
using System.Linq;
using System.Collections.Generic;
	
namespace hw1.Models
{   
	public  class 客戶聯絡人Repository : EFRepository<客戶聯絡人>, I客戶聯絡人Repository
	{
        public 客戶聯絡人 Find(int id)
        {
            return this.All().Where(p => p.Id == id).FirstOrDefault();
        }

        public IQueryable<客戶聯絡人> Search(string searchString)
        {
            return this.All().Where(p => p.姓名.Contains(searchString));
        }

        public 客戶聯絡人 IsExist(string Email)
        {
            return this.All().Where(p => p.Email == Email).SingleOrDefault();
        }

        public IQueryable<客戶聯絡人> CategoryQuery(string 職稱, string searchString)
        {
            if (string.IsNullOrEmpty(searchString))
                return All().Where(p => p.職稱.Equals(職稱));
            else
                return All().Where(p => p.職稱.Equals(職稱) && p.姓名.Contains(searchString));
        }

        public IQueryable<string> 職稱GroupByList()
        {
            return this.All().GroupBy(p => p.職稱).Select(p => p.Key);
        }
        /// <summary>
        /// 覆寫 All() 只取出 [刪除] 欄位為 0 的紀錄
        /// </summary>
        /// <returns></returns>
        public override IQueryable<客戶聯絡人> All()
        {
            return base.All().Where(p => p.刪除 == false);
        }
    }

	public  interface I客戶聯絡人Repository : IRepository<客戶聯絡人>
	{

	}
}