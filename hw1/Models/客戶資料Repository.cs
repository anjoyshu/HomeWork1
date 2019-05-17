using System;
using System.Linq;
using System.Collections.Generic;
	
namespace hw1.Models
{   
	public  class 客戶資料Repository : EFRepository<客戶資料>, I客戶資料Repository
	{
        public 客戶資料 Find(int id)
        {
            return this.All().Where(p => p.Id == id).FirstOrDefault();
        }

        
	}

	public  interface I客戶資料Repository : IRepository<客戶資料>
	{

	}
}