using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using hw1.Models;

namespace hw1.Controllers
{
    public class HomeController : Controller
    {

        private CustomerEntities db = new CustomerEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult uvw_CustomerDetail()
        {
            ViewBag.Title = "客戶檢視表";
            return View();
        }
    }
}