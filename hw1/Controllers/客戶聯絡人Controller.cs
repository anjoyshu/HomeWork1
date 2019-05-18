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
    public class 客戶聯絡人Controller : Controller
    {
        客戶聯絡人Repository repo聯絡;
        客戶資料Repository repo客戶;

        public 客戶聯絡人Controller()
        {
            repo聯絡 = RepositoryHelper.Get客戶聯絡人Repository();
            repo客戶 = RepositoryHelper.Get客戶資料Repository(repo聯絡.UnitOfWork);
        }

        // GET: 客戶聯絡人
        public ActionResult Index(string sortOrder, string currentOrder, string searchString)
        {
            var contact = repo聯絡.All();
            
            if (!String.IsNullOrEmpty(searchString))
            {
                contact = repo聯絡.Search(searchString);
                return View(contact.ToList());
            }
            else
            {
                sortOrder = String.IsNullOrEmpty(sortOrder) ? "職稱" : sortOrder;

                switch (sortOrder)
                {
                    case "職稱":
                        if (currentOrder == "職稱_desc")
                        {
                            contact = contact.OrderByDescending(p => p.職稱);
                            ViewBag.currentOrder = "職稱";
                        }
                        else
                        {
                            contact = contact.OrderBy(p => p.職稱);
                            ViewBag.currentOrder = "職稱_desc";
                        }
                        break;
                    case "姓名":
                        if (currentOrder == "姓名_desc")
                        {
                            contact = contact.OrderByDescending(p => p.姓名);
                            ViewBag.currentOrder = "姓名";
                        }
                        else
                        {
                            contact = contact.OrderBy(p => p.姓名);
                            ViewBag.currentOrder = "姓名_desc";
                        }
                        break;
                    case "Email":
                        if (currentOrder == "Email_desc")
                        {
                            contact = contact.OrderByDescending(p => p.Email);
                            ViewBag.currentOrder = "Email";
                        }
                        else
                        {
                            contact = contact.OrderBy(p => p.Email);
                            ViewBag.currentOrder = "Email_desc";
                        }
                        break;
                    case "手機":
                        if (currentOrder == "手機_desc")
                        {
                            contact = contact.OrderByDescending(p => p.手機);
                            ViewBag.currentOrder = "手機";
                        }
                        else
                        {
                            contact = contact.OrderBy(p => p.手機);
                            ViewBag.currentOrder = "手機_desc";
                        }
                        break;
                    case "電話":
                        if (currentOrder == "電話_desc")
                        {
                            contact = contact.OrderByDescending(p => p.電話);
                            ViewBag.currentOrder = "電話";
                        }
                        else
                        {
                            contact = contact.OrderBy(p => p.電話);
                            ViewBag.currentOrder = "電話_desc";
                        }
                        break;
                    case "客戶資料.客戶名稱":
                        if (currentOrder == "客戶資料.客戶名稱_desc")
                        {
                            contact = contact.OrderByDescending(p => p.客戶資料.客戶名稱);
                            ViewBag.currentOrder = "客戶資料.客戶名稱";
                        }
                        else
                        {
                            contact = contact.OrderBy(p => p.客戶資料.客戶名稱);
                            ViewBag.currentOrder = "客戶資料.客戶名稱_desc";
                        }
                        break;
                    default:
                        break;
                }
                return View(contact.ToList());
            }
        }

        [HttpPost]
        public FileResult Export()
        {
            var data = repo聯絡.All();
            DataTable dt = new DataTable("客戶聯絡人");
            dt.Columns.AddRange(new DataColumn[4] {
                new DataColumn("客戶ID"),
                new DataColumn("客戶姓名"),
                new DataColumn("職稱"),
                new DataColumn("手機") });

            foreach (var contact in data)
            {
                dt.Rows.Add(contact.客戶Id, contact.姓名, contact.職稱, contact.手機);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.ms-excel", "ContactData.xlsx");
                }
            }
        }

        public JsonResult DuplicateEmail(string Email)
        {
            bool isValidate = false;

            客戶聯絡人 客戶聯絡 = repo聯絡.IsExist(Email);
            if (客戶聯絡 == null) { isValidate = true; }
            else { isValidate = false; }

            return Json(isValidate, JsonRequestBehavior.AllowGet);

        }

        // GET: 客戶聯絡人/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = repo聯絡.Find(id.Value);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Create
        public ActionResult Create()
        {
            ViewBag.客戶Id = new SelectList(repo客戶.All(), "Id", "客戶名稱");
            return View();
        }

        // POST: 客戶聯絡人/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話")] 客戶聯絡人 客戶聯絡人)
        {
            if (ModelState.IsValid)
            {
                repo聯絡.Add(客戶聯絡人);
                repo聯絡.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            ViewBag.客戶Id = new SelectList(repo客戶.All(), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = repo聯絡.Find(id.Value);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            ViewBag.客戶Id = new SelectList(repo客戶.All(), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // POST: 客戶聯絡人/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話")] 客戶聯絡人 客戶聯絡人)
        {
            if (ModelState.IsValid)
            {
                repo聯絡.UnitOfWork.Context.Entry(客戶聯絡人).State = EntityState.Modified;
                repo聯絡.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            ViewBag.客戶Id = new SelectList(repo客戶.All(), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = repo聯絡.Find(id.Value);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        // POST: 客戶聯絡人/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶聯絡人 客戶聯絡人 = repo聯絡.Find(id);
            repo聯絡.Delete(客戶聯絡人);
            repo聯絡.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repo聯絡.UnitOfWork.Context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
