using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClosedXML.Excel;
using hw1.Models;

namespace hw1.Controllers
{
    public class 客戶資料Controller : Controller
    {
        客戶資料Repository repo客戶;
        uvw_CustomerDetailRepository repoCustomerDetail;

        public 客戶資料Controller()
        {
            repo客戶 = RepositoryHelper.Get客戶資料Repository();
            repoCustomerDetail = RepositoryHelper.Getuvw_CustomerDetailRepository();
        }

        // GET: 客戶資料
        public ViewResult Index(string sortOrder, string currentOrder, string searchString, string 客戶分類)
        {
            var customer = repo客戶.All();

            ViewBag.客戶分類SelectList = new SelectList(items: repo客戶.客戶分類GroupByList().ToList());

            if (!String.IsNullOrEmpty(searchString))
            {
                if (!String.IsNullOrEmpty(客戶分類))
                {
                    customer = repo客戶.CategoryQuery(客戶分類, searchString);
                }
                else
                {
                    customer = repo客戶.Search(searchString);
                }
                return View(customer.ToList());
            }
            else if(!String.IsNullOrEmpty(客戶分類))
            {
                customer = repo客戶.CategoryQuery(客戶分類, searchString);
                return View(customer.ToList());
            }
            else
            {
                sortOrder = String.IsNullOrEmpty(sortOrder) ? "客戶名稱" : sortOrder;

                switch (sortOrder)
                {
                    case "客戶名稱":
                        if (currentOrder == "客戶名稱_desc")
                        {
                            customer = customer.OrderByDescending(p => p.客戶名稱);
                            ViewBag.currentOrder = "客戶名稱";
                        }
                        else
                        {
                            customer = customer.OrderBy(p => p.客戶名稱);
                            ViewBag.currentOrder = "客戶名稱_desc";
                        }
                        break;
                    case "統一編號":
                        if (currentOrder == "統一編號_desc")
                        {
                            customer = customer.OrderByDescending(p => p.統一編號);
                            ViewBag.currentOrder = "統一編號";
                        }
                        else
                        {
                            customer = customer.OrderBy(p => p.統一編號);
                            ViewBag.currentOrder = "統一編號_desc";
                        }
                        break;
                    case "電話":
                        if (currentOrder == "電話_desc")
                        {
                            customer = customer.OrderByDescending(p => p.電話);
                            ViewBag.currentOrder = "電話";
                        }
                        else
                        {
                            customer = customer.OrderBy(p => p.電話);
                            ViewBag.currentOrder = "電話_desc";
                        }
                        break;
                    case "傳真":
                        if (currentOrder == "傳真_desc")
                        {
                            customer = customer.OrderByDescending(p => p.傳真);
                            ViewBag.currentOrder = "傳真";
                        }
                        else
                        {
                            customer = customer.OrderBy(p => p.傳真);
                            ViewBag.currentOrder = "傳真_desc";
                        }
                        break;
                    case "地址":
                        if (currentOrder == "地址_desc")
                        {
                            customer = customer.OrderByDescending(p => p.地址);
                            ViewBag.currentOrder = "地址";
                        }
                        else
                        {
                            customer = customer.OrderBy(p => p.地址);
                            ViewBag.currentOrder = "地址_desc";
                        }
                        break;
                    case "Email":
                        if (currentOrder == "Email_desc")
                        {
                            customer = customer.OrderByDescending(p => p.Email);
                            ViewBag.currentOrder = "Email";
                        }
                        else
                        {
                            customer = customer.OrderBy(p => p.Email);
                            ViewBag.currentOrder = "Email_desc";
                        }
                        break;
                    default:
                        break;
                }
                return View(customer.ToList());
            }
        }

        [HttpPost]
        public FileResult Export()
        {
            var data = repo客戶.All();
            DataTable dt = new DataTable("客戶資料");
            dt.Columns.AddRange(new DataColumn[4] {
                new DataColumn("客戶名稱"),
                new DataColumn("客戶聯絡人"),
                new DataColumn("電話"),
                new DataColumn("地址") });

            foreach (var customer in data)
            {
                dt.Rows.Add(customer.客戶名稱, customer.客戶聯絡人, customer.電話, customer.地址);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.ms-excel", "CustomerData.xlsx");
                }
            }
        }

        public JsonResult DuplicateEmail(string Email)
        {
            return Json(repo客戶.IsExist(Email), JsonRequestBehavior.AllowGet);
        }

        // GET: 客戶資料/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = repo客戶.Find(id.Value);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // GET: 客戶資料/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: 客戶資料/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email")] 客戶資料 客戶資料)
        {

            if (ModelState.IsValid)
            {
                repo客戶.Add(客戶資料);
                repo客戶.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            return View(客戶資料);
        }

        // GET: 客戶資料/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = repo客戶.Find(id.Value);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // POST: 客戶資料/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email")] 客戶資料 客戶資料)
        {
            if (ModelState.IsValid)
            {
                repo客戶.UnitOfWork.Context.Entry(客戶資料).State = EntityState.Modified;
                repo客戶.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            return View(客戶資料);
        }

        // GET: 客戶資料/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = repo客戶.Find(id.Value);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // POST: 客戶資料/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶資料 客戶資料 = repo客戶.Find(id);
            //repo客戶.Delete(客戶資料);
            客戶資料.刪除 = true;
            repo客戶.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }

        public ActionResult 客戶關聯資料表()
        {
            return View(repoCustomerDetail.All().ToList());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repo客戶.UnitOfWork.Context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
