using System;
using System.Linq;
using System.Collections.Generic;
	
namespace hw1.Models
{   
	public  class 客戶銀行資訊Repository : EFRepository<客戶銀行資訊>, I客戶銀行資訊Repository
	{
        public 客戶銀行資訊 Find(int id)
        {
            return this.All().Where(p => p.Id == id).FirstOrDefault();
        }
        /// <summary>
        /// 覆寫 All() 只取出 [刪除] 欄位為 0 的紀錄
        /// </summary>
        /// <returns></returns>
        public override IQueryable<客戶銀行資訊> All()
        {
            return base.All().Where(p => p.刪除 == false);
        }
    }

	public  interface I客戶銀行資訊Repository : IRepository<客戶銀行資訊>
	{

	}
}