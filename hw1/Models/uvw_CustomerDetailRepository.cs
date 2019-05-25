using System;
using System.Linq;
using System.Collections.Generic;
	
namespace hw1.Models
{   
	public  class uvw_CustomerDetailRepository : EFRepository<uvw_CustomerDetail>, Iuvw_CustomerDetailRepository
	{

    }

	public  interface Iuvw_CustomerDetailRepository : IRepository<uvw_CustomerDetail>
	{

	}
}