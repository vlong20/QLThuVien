using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using THPTPhuBinh.Models;

namespace THPTPhuBinh.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Autherize(THPTPhuBinh.Models.TaiKhoan account)
        {
            using (thuvienEntities db = new thuvienEntities())
            {
                var userDetails = db.TaiKhoans.Where(x => x.TaiKhoan1 == account.TaiKhoan1 && x.MatKhau == account.MatKhau).FirstOrDefault();
                if (ModelState.IsValid)
                {
                    
                    if (userDetails == null)
                    {
                        account.LoginErrorMessage = "Tài khoản hoặc mật khẩu không đúng";
                        return View("Index", account);
                    } else
                    {
                        Session["LoaiTK"] = userDetails.LoaiTK;

                        return RedirectToAction("Index", "Sach");
                        
                    }
                }
                
                else
                {
                    return View("Index", account);
                }
            }
        }
    }
}