using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using System.IO;
using BANHSAURIENG.Filters;
using BANHSAURIENG.Models;
using BANHSAURIENG.DataAccess;

namespace BANHSAURIENG.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        //Troy
        [AllowAnonymous]
        public ActionResult Login(string status, string user)
        {
            ViewBag.Status = status;

            if (Session["UserBANHSAURIENG"] != null)
            {
                return RedirectToAction("Index","Sale");
            }
            else
            {
                if (Session["LoginFailBANHSAURIENG"] != null && Int32.Parse(Session["LoginFailBANHSAURIENG"].ToString()) > 3)
                {
                    ViewBag.loginformvisible = "false";
                    return View();
                }
                else
                {
                    ViewBag.loginformvisible = "true";
                    HttpCookie oldCook = Request.Cookies["UserAccountBANHSAURIENG"];
                    if (oldCook != null)
                    {
                        ViewBag.Username = user;
                    }
                    return View();
                }
            }
        }
        //Troy
        //[HttpPost]
        //[AllowAnonymous]
        //public ActionResult Login(FormCollection f)
        //{

        //    if (Session["UserBANHSAURIENG"] != null)
        //    {
        //        return RedirectToAction("Create");
        //    }
        //    else
        //    {
        //        string username = f["UserName"];
        //        string password = f["Password"];
        //        string remember = f["Remember"];
        //        int login_status = AccountDataAccess.GetInstance().checkLogin(username.Trim(), password);
        //        if (login_status == 0)
        //        {
        //            if (remember == "on")
        //            {
        //                FormsAuthentication.SetAuthCookie(username, true);
        //            }
        //            FormsAuthentication.SetAuthCookie(username, false);
        //            Session["UserBANHSAURIENG"] = username;
        //            return RedirectToAction("Index", "Sale");
        //        }
        //        else
        //        {
        //            if (Session["LoginFailBANHSAURIENG"] == null)
        //            {
        //                Session["LoginFailBANHSAURIENG"] = 1;
        //            }
        //            else
        //            {
        //                Session["LoginFailBANHSAURIENG"] = Int32.Parse(Session["LoginFailBANHSAURIENG"].ToString()) + 1;
        //            }
        //            return RedirectToAction("Login", new { status = "Tài khoản hoặc mật khẩu không đúng!" });
        //        }
        //    }
        //}
        //Troy
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            Session["UserBANHSAURIENG"] = null;
            return RedirectToAction("Login");
        }
        public ActionResult Attendance()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Upload(string image)
        {
            image = image.Substring("data:image/png;base64,".Length);
            var buffer = Convert.FromBase64String(image);
            // TODO: I am saving the image on the hard disk but
            // you could do whatever processing you want with it
            string time = DateTime.Now.ToString().Replace("/", "_");
            time = time.Replace(":", "_");
            time = time.Replace(" ", "_");
            var croppath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Images/"), String.Format("{0}_{1}", time, ".png"));
            System.IO.File.WriteAllBytes(croppath, buffer);
            return Json(new { success = true });
        }
    }
}
