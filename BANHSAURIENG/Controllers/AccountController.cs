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
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
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
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(FormCollection f)
        {

            if (Session["UserBANHSAURIENG"] != null)
            {
                return RedirectToAction("Create");
            }
            else
            {
                string username = f["UserName"];
                string password = f["Password"];
                string remember = f["Remember"];
                int login_status = AccountDataAccess.GetInstance().checkLogin(username.Trim(), password);
                if (login_status == 0)
                {
                    if (remember == "on")
                    {
                        FormsAuthentication.SetAuthCookie(username, true);
                    }
                    FormsAuthentication.SetAuthCookie(username, false);
                    Session["UserBANHSAURIENG"] = username;
                    Session["BillIDBANHSAURIENG"] = 0;
                    return RedirectToAction("Index", "Sale");
                }
                else
                {
                    if (Session["LoginFailBANHSAURIENG"] == null)
                    {
                        Session["LoginFailBANHSAURIENG"] = 1;
                    }
                    else
                    {
                        Session["LoginFailBANHSAURIENG"] = Int32.Parse(Session["LoginFailBANHSAURIENG"].ToString()) + 1;
                    }
                    return RedirectToAction("Login", new { status = "Tài khoản hoặc mật khẩu không đúng!" });
                }
            }
        }
        //Troy
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            Session["UserBANHSAURIENG"] = null;
            Session["BillIDBANHSAURIENG"] = null;
            return RedirectToAction("Login");
        }
        public ActionResult Employee()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Upload(long eID, string image)
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
            Employee_Attendance ea = new Employee_Attendance();
            ea.EmployeeID = eID;
            ea.CreateDate = DateTime.Now;
            ea.ImageUrl = String.Format("{0}_{1}", time, ".png");
            bool s = EmployeeDataAccess.GetInstance().CreateHistoryEmployee(ea);
            return Json(new { success = s });
        }
        public ActionResult Read()
        {
            List<Account> accs = new List<Account>();
            foreach(tblAccount acc in AccountDataAccess.GetInstance().Read())
            {
                accs.Add(new Account(){
                    ObjectID = acc.ObjectID,
                    Username = acc.Username,
                    Password = acc.Password,
                    isEnable = acc.isEnable,
                    isSystem = acc.isSystem,
                    isDelete = acc.isDelete,
                    isHidden = acc.isHidden
                });
            }
            return Json(accs, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Create(string models)
        {
            models = models.Replace("[", "");
            models = models.Replace("]", "");
            JObject o = JObject.Parse(models);
            tblAccount acc = new tblAccount();
            acc.Username = (string)o.SelectToken("Username");
            acc.Password = (string)o.SelectToken("Password");
            acc.isEnable = (bool)o.SelectToken("isEnable");
            acc.isSystem = (bool)o.SelectToken("isSystem");
            acc.isDelete = (bool)o.SelectToken("isDelete");
            acc.isHidden = (bool)o.SelectToken("isHidden");
            acc.CreateDate = DateTime.Now;
            acc.LastLogin = DateTime.Now;
            acc.LastUpdate = DateTime.Now;
            return Json(AccountDataAccess.GetInstance().Create(acc), JsonRequestBehavior.AllowGet);
        }
        public ActionResult Update(string models)
        {
            models = models.Replace("[", "");
            models = models.Replace("]", "");
            JObject o = JObject.Parse(models);
            tblAccount acc = new tblAccount();
            acc.ObjectID = (long)o.SelectToken("ObjectID");
            acc.Username = (string)o.SelectToken("Username");
            acc.Password = (string)o.SelectToken("Password");
            acc.isEnable = (bool)o.SelectToken("isEnable");
            acc.isSystem = (bool)o.SelectToken("isSystem");
            acc.isDelete = (bool)o.SelectToken("isDelete");
            acc.isHidden = (bool)o.SelectToken("isHidden");
            acc.CreateDate = DateTime.Now;
            acc.LastLogin = DateTime.Now;
            acc.LastUpdate = DateTime.Now;
            return Json(AccountDataAccess.GetInstance().Update(acc), JsonRequestBehavior.AllowGet);
        }
        public ActionResult Destroy(string models)
        {
            models = models.Replace("[", "");
            models = models.Replace("]", "");
            JObject o = JObject.Parse(models);
            tblAccount acc = new tblAccount();
            acc.ObjectID = (long)o.SelectToken("ObjectID");
            acc.Username = (string)o.SelectToken("Username");
            acc.Password = (string)o.SelectToken("Password");
            acc.isEnable = (bool)o.SelectToken("isEnable");
            acc.isSystem = (bool)o.SelectToken("isSystem");
            acc.isDelete = (bool)o.SelectToken("isDelete");
            acc.isHidden = (bool)o.SelectToken("isHidden");
            acc.CreateDate = DateTime.Now;
            acc.LastLogin = DateTime.Now;
            acc.LastUpdate = DateTime.Now;
            return Json(AccountDataAccess.GetInstance().Destroy(acc), JsonRequestBehavior.AllowGet);
        }
    }
}
