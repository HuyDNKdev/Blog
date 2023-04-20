using OnlineTestWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineTestWeb.Controllers
{
    public class StudentController : Controller
    {
        QL_HeThongThiTracNghiemEntities db = new QL_HeThongThiTracNghiemEntities();
        // GET: Student
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
                    var obj = db.Tài_Khoản.Where(a => a.TenDN.Equals(objUser.TenDN) && a.MatKhau.Equals(objUser.MatKhau) && a.TenDN.StartsWith("20DH")).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["Student"] = obj;
                        return RedirectToAction("Index", "Student");
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
            Session["Student"] = null;
            Tài_Khoản kh = (Tài_Khoản)Session["Student"];
            if (kh != null)
                return RedirectToAction("Index", "Student");
            return View();
        }
        public ActionResult GanMenu()
        {
            return PartialView();
        }
        public ActionResult DoExam()
        {
            return View();
        }
    }
}