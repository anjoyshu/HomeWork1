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