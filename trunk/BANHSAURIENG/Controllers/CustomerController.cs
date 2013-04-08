using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BANHSAURIENG.Controllers
{
    public class CustomerController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
    }
}