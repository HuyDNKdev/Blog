using OnlineTestWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.Net;

namespace OnlineTestWeb.Controllers
{
    public class TeacherController : Controller
    {
        QL_HeThongThiTracNghiemEntities db = new QL_HeThongThiTracNghiemEntities();
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Tài_Khoản objUser)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(objUser.TenDN))
                    ModelState.AddModelError(string.Empty, "Tên đăng nhập không được để trống");
                if (string.IsNullOrEmpty(objUser.MatKhau))
                    ModelState.AddModelError(string.Empty, "Mật khẩu không được để trống");
                if (ModelState.IsValid)
                {
                    var obj = db.Tài_Khoản.Where(a => a.TenDN.Equals(objUser.TenDN) && a.MatKhau.Equals(objUser.MatKhau) && a.TenDN.StartsWith("GV")).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["Teacher"] = obj;
                        return RedirectToAction("QuanLyDeThi", "Teacher");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Đăng nhập không thành công!");
                    }
                }
            }
            return View(objUser);
        }
        [HttpGet]
        public ActionResult Login()
        {
            Session["Teacher"] = null;
            Tài_Khoản kh = (Tài_Khoản)Session["Teacher"];
            if (kh != null)
                return RedirectToAction("Index", "Teacher");
            return View();
        }
        public ActionResult GanMenu()
        {
            return PartialView();
        }
        public ActionResult Information()
        {

            return View();
        }
        #region QuảnLýCâuHỏi
        public ActionResult QuanLyCauHoi()
        {
            var câu_Hỏi = db.Câu_Hỏi.Include(c => c.Nhóm_Câu_Hỏi);
            return View(câu_Hỏi.ToList());
        }
        // GET: Câu_Hỏi/CreateQues
        public ActionResult CreateQues()
        {
            ViewBag.MaNhom = new SelectList(db.Nhóm_Câu_Hỏi, "MaNhom", "TenNhom");
            return View();
        }

        // POST: Câu_Hỏi/CreateQues
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateQues([Bind(Include = "MaCauHoi,PhanCauHoi,PhanCauTraLoi,DapAn,MaNhom")] Câu_Hỏi câu_Hỏi)
        {
            if (ModelState.IsValid)
            {
                db.Câu_Hỏi.Add(câu_Hỏi);
                db.SaveChanges();
                return RedirectToAction("QuanLyCauHoi");
            }

            ViewBag.MaNhom = new SelectList(db.Nhóm_Câu_Hỏi, "MaNhom", "TenNhom", câu_Hỏi.MaNhom);
            return View(câu_Hỏi);
        }

        // GET: Câu_Hỏi/Edit/5
        public ActionResult EditQues(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Câu_Hỏi câu_Hỏi = db.Câu_Hỏi.Find(id);
            if (câu_Hỏi == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaNhom = new SelectList(db.Nhóm_Câu_Hỏi, "MaNhom", "TenNhom", câu_Hỏi.MaNhom);
            return View(câu_Hỏi);
        }

        // POST: Câu_Hỏi/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditQues([Bind(Include = "MaCauHoi,PhanCauHoi,PhanCauTraLoi,DapAn,MaNhom")] Câu_Hỏi câu_Hỏi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(câu_Hỏi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("QuanLyCauHoi");
            }
            ViewBag.MaNhom = new SelectList(db.Nhóm_Câu_Hỏi, "MaNhom", "TenNhom", câu_Hỏi.MaNhom);
            return View(câu_Hỏi);
        }

        // GET: Câu_Hỏi/Delete/5
        public ActionResult DeleteQues(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Câu_Hỏi câu_Hỏi = db.Câu_Hỏi.Find(id);
            if (câu_Hỏi == null)
            {
                return HttpNotFound();
            }
            return View(câu_Hỏi);
        }

        // POST: Câu_Hỏi/Delete/5
        [HttpPost, ActionName("DeleteQues")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteQuesConfirmed(string id)
        {
            Câu_Hỏi câu_Hỏi = db.Câu_Hỏi.Find(id);
            db.Câu_Hỏi.Remove(câu_Hỏi);
            db.SaveChanges();
            return RedirectToAction("QuanLyCauHoi");
        }
        #endregion
        #region QuảnLýNhómCâuHỏi
        public ActionResult QuanLyNhomCauHoi()
        {
            var nhóm_Câu_Hỏi = db.Nhóm_Câu_Hỏi.Include(n => n.Môn_Học);
            return View(nhóm_Câu_Hỏi.ToList());
        }
        // GET: Nhóm_Câu_Hỏi/Details/5
        public ActionResult DetailsQuesGroup(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nhóm_Câu_Hỏi nhóm_Câu_Hỏi = db.Nhóm_Câu_Hỏi.Find(id);
            if (nhóm_Câu_Hỏi == null)
            {
                return HttpNotFound();
            }
            return View(nhóm_Câu_Hỏi);
        }

        // GET: Nhóm_Câu_Hỏi/Create
        public ActionResult CreateQuesGroup()
        {
            ViewBag.MaMon = new SelectList(db.Môn_Học, "MaMon", "TenMon");
            return View();
        }

        // POST: Nhóm_Câu_Hỏi/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateQuesGroup([Bind(Include = "MaNhom,TenNhom,MaMon,SoCauHoi")] Nhóm_Câu_Hỏi nhóm_Câu_Hỏi)
        {
            if (ModelState.IsValid)
            {
                db.Nhóm_Câu_Hỏi.Add(nhóm_Câu_Hỏi);
                db.SaveChanges();
                return RedirectToAction("QuanLyNhomCauHoi");
            }

            ViewBag.MaMon = new SelectList(db.Môn_Học, "MaMon", "TenMon", nhóm_Câu_Hỏi.MaMon);
            return View(nhóm_Câu_Hỏi);
        }

        // GET: Nhóm_Câu_Hỏi/Edit/5
        public ActionResult EditQuesGroup(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nhóm_Câu_Hỏi nhóm_Câu_Hỏi = db.Nhóm_Câu_Hỏi.Find(id);
            if (nhóm_Câu_Hỏi == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaMon = new SelectList(db.Môn_Học, "MaMon", "TenMon", nhóm_Câu_Hỏi.MaMon);
            return View(nhóm_Câu_Hỏi);
        }

        // POST: Nhóm_Câu_Hỏi/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditQuesGroup([Bind(Include = "MaNhom,TenNhom,MaMon,SoCauHoi")] Nhóm_Câu_Hỏi nhóm_Câu_Hỏi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nhóm_Câu_Hỏi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("QuanLyNhomCauHoi");
            }
            ViewBag.MaMon = new SelectList(db.Môn_Học, "MaMon", "TenMon", nhóm_Câu_Hỏi.MaMon);
            return View(nhóm_Câu_Hỏi);
        }

        // GET: Nhóm_Câu_Hỏi/Delete/5
        public ActionResult DeleteQuesGroup(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nhóm_Câu_Hỏi nhóm_Câu_Hỏi = db.Nhóm_Câu_Hỏi.Find(id);
            if (nhóm_Câu_Hỏi == null)
            {
                return HttpNotFound();
            }
            return View(nhóm_Câu_Hỏi);
        }

        // POST: Nhóm_Câu_Hỏi/Delete/5
        [HttpPost, ActionName("DeleteQuesGroup")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteQuesGroupConfirmed(string id)
        {
            Nhóm_Câu_Hỏi nhóm_Câu_Hỏi = db.Nhóm_Câu_Hỏi.Find(id);
            db.Nhóm_Câu_Hỏi.Remove(nhóm_Câu_Hỏi);
            db.SaveChanges();
            return RedirectToAction("QuanLyNhomCauHoi");
        }
        #endregion
        #region QuảnLýĐềThi
        public ActionResult QuanLyDeThi()
        {
            var đề_Thi = db.Đề_Thi.Include(đ => đ.Giáo_Viên).Include(đ => đ.Loại_Đề).Include(đ => đ.Môn_Học).Include(đ => đ.Nhóm_Câu_Hỏi);
            return View(đề_Thi.ToList());
        }

        // GET: Đề_Thi/Details/5
        public ActionResult DetailsExam(string id)
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
        public ActionResult CreateExam()
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
        public ActionResult CreateExam([Bind(Include = "MaDe,TenDe,ThoiGianLamBai,ThoiGianBatDau,ThoiGianKetThuc,MaMon,MaNhom,MaGV,MaLoaiDe,SoDeThiDuocTao,DaoDapAn,TrangThai")] Đề_Thi đề_Thi)
        {
            if (ModelState.IsValid)
            {
                db.Đề_Thi.Add(đề_Thi);
                db.SaveChanges();
                return RedirectToAction("QuanLyDeThi");
            }

            ViewBag.MaGV = new SelectList(db.Giáo_Viên, "MaGV", "HoTen", đề_Thi.MaGV);
            ViewBag.MaLoaiDe = new SelectList(db.Loại_Đề, "MaLoaiDe", "TenLoaiDe", đề_Thi.MaLoaiDe);
            ViewBag.MaMon = new SelectList(db.Môn_Học, "MaMon", "TenMon", đề_Thi.MaMon);
            ViewBag.MaNhom = new SelectList(db.Nhóm_Câu_Hỏi, "MaNhom", "TenNhom", đề_Thi.MaNhom);
            return View(đề_Thi);
        }

        // GET: Đề_Thi/Edit/5
        public ActionResult EditExam(string id)
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
        public ActionResult EditExam([Bind(Include = "MaDe,TenDe,ThoiGianLamBai,ThoiGianBatDau,ThoiGianKetThuc,MaMon,MaNhom,MaGV,MaLoaiDe,SoDeThiDuocTao,DaoDapAn,TrangThai")] Đề_Thi đề_Thi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(đề_Thi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("QuanLyDeThi");
            }
            ViewBag.MaGV = new SelectList(db.Giáo_Viên, "MaGV", "HoTen", đề_Thi.MaGV);
            ViewBag.MaLoaiDe = new SelectList(db.Loại_Đề, "MaLoaiDe", "TenLoaiDe", đề_Thi.MaLoaiDe);
            ViewBag.MaMon = new SelectList(db.Môn_Học, "MaMon", "TenMon", đề_Thi.MaMon);
            ViewBag.MaNhom = new SelectList(db.Nhóm_Câu_Hỏi, "MaNhom", "TenNhom", đề_Thi.MaNhom);
            return View(đề_Thi);
        }

        // GET: Đề_Thi/Delete/5
        public ActionResult DeleteExam(string id)
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
        [HttpPost, ActionName("DeleteExam")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteExamConfirmed(string id)
        {
            Đề_Thi đề_Thi = db.Đề_Thi.Find(id);
            db.Đề_Thi.Remove(đề_Thi);
            db.SaveChanges();
            return RedirectToAction("QuanLyDeTHI");
        }
        #endregion
    }
}