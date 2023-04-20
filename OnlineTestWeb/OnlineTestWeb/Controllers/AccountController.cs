using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineTestWeb.Models;

namespace OnlineTestWeb.Controllers
{
    public class AccountController : Controller
    {
        private QL_HeThongThiTracNghiemEntities db = new QL_HeThongThiTracNghiemEntities();

        // GET: Account
        public ActionResult Index()
        {
            var tài_Khoản = db.Tài_Khoản.Include(t => t.Lớp).Include(t => t.Phân_Quyền);
            return View(tài_Khoản.ToList());
        }

        // GET: Account/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tài_Khoản tài_Khoản = db.Tài_Khoản.Find(id);
            if (tài_Khoản == null)
            {
                return HttpNotFound();
            }
            return View(tài_Khoản);
        }

        // GET: Account/Create
        public ActionResult Create()
        {
            ViewBag.MaLop = new SelectList(db.Lớp, "MaLop", "TenLop");
            ViewBag.MaPhanQuyen = new SelectList(db.Phân_Quyền, "MaPhanQuyen", "TenPhanQuyen");
            return View();
        }

        // POST: Account/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaTaiKhoan,TenDN,MatKhau,MaPhanQuyen,TrangThai,MaLop")] Tài_Khoản tài_Khoản)
        {
            if (ModelState.IsValid)
            {
                db.Tài_Khoản.Add(tài_Khoản);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaLop = new SelectList(db.Lớp, "MaLop", "TenLop", tài_Khoản.MaLop);
            ViewBag.MaPhanQuyen = new SelectList(db.Phân_Quyền, "MaPhanQuyen", "TenPhanQuyen", tài_Khoản.MaPhanQuyen);
            return View(tài_Khoản);
        }

        // GET: Account/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tài_Khoản tài_Khoản = db.Tài_Khoản.Find(id);
            if (tài_Khoản == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaLop = new SelectList(db.Lớp, "MaLop", "TenLop", tài_Khoản.MaLop);
            ViewBag.MaPhanQuyen = new SelectList(db.Phân_Quyền, "MaPhanQuyen", "TenPhanQuyen", tài_Khoản.MaPhanQuyen);
            return View(tài_Khoản);
        }

        // POST: Account/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaTaiKhoan,TenDN,MatKhau,MaPhanQuyen,TrangThai,MaLop")] Tài_Khoản tài_Khoản)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tài_Khoản).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaLop = new SelectList(db.Lớp, "MaLop", "TenLop", tài_Khoản.MaLop);
            ViewBag.MaPhanQuyen = new SelectList(db.Phân_Quyền, "MaPhanQuyen", "TenPhanQuyen", tài_Khoản.MaPhanQuyen);
            return View(tài_Khoản);
        }

        // GET: Account/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tài_Khoản tài_Khoản = db.Tài_Khoản.Find(id);
            if (tài_Khoản == null)
            {
                return HttpNotFound();
            }
            return View(tài_Khoản);
        }

        // POST: Account/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Tài_Khoản tài_Khoản = db.Tài_Khoản.Find(id);
            db.Tài_Khoản.Remove(tài_Khoản);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Tài_Khoản objUser)
        {
            if (ModelState.IsValid)
            {
                var obj = db.Tài_Khoản.Where(a => a.TenDN.Equals(objUser.TenDN) && a.MatKhau.Equals(objUser.MatKhau)).FirstOrDefault();
                if (obj != null)
                {
                    Session["KH"] = obj;
                    return RedirectToAction("BookRoom", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Login data is incorrect!");
                }
            }
            return View(objUser);
        }
        [HttpGet]
        public ActionResult Login()
        {
            Session["KH"] = null;
            Tài_Khoản kh = (Tài_Khoản)Session["KH"];
            if (kh != null)
                return RedirectToAction("BookRoom", "Home");
            return View();
        }
    }

}
