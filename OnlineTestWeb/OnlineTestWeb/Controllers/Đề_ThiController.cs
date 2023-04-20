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
    public class Đề_ThiController : Controller
    {
        private QL_HeThongThiTracNghiemEntities db = new QL_HeThongThiTracNghiemEntities();

        // GET: Đề_Thi
        public ActionResult Index()
        {
            var đề_Thi = db.Đề_Thi.Include(đ => đ.Giáo_Viên).Include(đ => đ.Loại_Đề).Include(đ => đ.Môn_Học).Include(đ => đ.Nhóm_Câu_Hỏi);
            return View(đề_Thi.ToList());
        }

        // GET: Đề_Thi/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Đề_Thi đề_Thi = db.Đề_Thi.Find(id);
            if (đề_Thi == null)
            {
                return HttpNotFound();
            }
            return View(đề_Thi);
        }

        // GET: Đề_Thi/Create
        public ActionResult Create()
        {
            ViewBag.MaGV = new SelectList(db.Giáo_Viên, "MaGV", "HoTen");
            ViewBag.MaLoaiDe = new SelectList(db.Loại_Đề, "MaLoaiDe", "TenLoaiDe");
            ViewBag.MaMon = new SelectList(db.Môn_Học, "MaMon", "TenMon");
            ViewBag.MaNhom = new SelectList(db.Nhóm_Câu_Hỏi, "MaNhom", "TenNhom");
            return View();
        }

        // POST: Đề_Thi/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaDe,TenDe,ThoiGianLamBai,ThoiGianBatDau,ThoiGianKetThuc,MaMon,MaNhom,MaGV,MaLoaiDe,SoDeThiDuocTao,DaoDapAn,TrangThai")] Đề_Thi đề_Thi)
        {
            if (ModelState.IsValid)
            {
                db.Đề_Thi.Add(đề_Thi);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaGV = new SelectList(db.Giáo_Viên, "MaGV", "HoTen", đề_Thi.MaGV);
            ViewBag.MaLoaiDe = new SelectList(db.Loại_Đề, "MaLoaiDe", "TenLoaiDe", đề_Thi.MaLoaiDe);
            ViewBag.MaMon = new SelectList(db.Môn_Học, "MaMon", "TenMon", đề_Thi.MaMon);
            ViewBag.MaNhom = new SelectList(db.Nhóm_Câu_Hỏi, "MaNhom", "TenNhom", đề_Thi.MaNhom);
            return View(đề_Thi);
        }

        // GET: Đề_Thi/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Đề_Thi đề_Thi = db.Đề_Thi.Find(id);
            if (đề_Thi == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaGV = new SelectList(db.Giáo_Viên, "MaGV", "HoTen", đề_Thi.MaGV);
            ViewBag.MaLoaiDe = new SelectList(db.Loại_Đề, "MaLoaiDe", "TenLoaiDe", đề_Thi.MaLoaiDe);
            ViewBag.MaMon = new SelectList(db.Môn_Học, "MaMon", "TenMon", đề_Thi.MaMon);
            ViewBag.MaNhom = new SelectList(db.Nhóm_Câu_Hỏi, "MaNhom", "TenNhom", đề_Thi.MaNhom);
            return View(đề_Thi);
        }

        // POST: Đề_Thi/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaDe,TenDe,ThoiGianLamBai,ThoiGianBatDau,ThoiGianKetThuc,MaMon,MaNhom,MaGV,MaLoaiDe,SoDeThiDuocTao,DaoDapAn,TrangThai")] Đề_Thi đề_Thi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(đề_Thi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaGV = new SelectList(db.Giáo_Viên, "MaGV", "HoTen", đề_Thi.MaGV);
            ViewBag.MaLoaiDe = new SelectList(db.Loại_Đề, "MaLoaiDe", "TenLoaiDe", đề_Thi.MaLoaiDe);
            ViewBag.MaMon = new SelectList(db.Môn_Học, "MaMon", "TenMon", đề_Thi.MaMon);
            ViewBag.MaNhom = new SelectList(db.Nhóm_Câu_Hỏi, "MaNhom", "TenNhom", đề_Thi.MaNhom);
            return View(đề_Thi);
        }

        // GET: Đề_Thi/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Đề_Thi đề_Thi = db.Đề_Thi.Find(id);
            if (đề_Thi == null)
            {
                return HttpNotFound();
            }
            return View(đề_Thi);
        }

        // POST: Đề_Thi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Đề_Thi đề_Thi = db.Đề_Thi.Find(id);
            db.Đề_Thi.Remove(đề_Thi);
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
    }
}
