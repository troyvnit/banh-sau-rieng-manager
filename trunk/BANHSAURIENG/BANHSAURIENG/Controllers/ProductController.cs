using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BANHSAURIENG.Models;
using BANHSAURIENG.DataAccess;
namespace BANHSAURIENG.Controllers
{
    public class ProductController : Controller
    {
        //
        // GET: /Product/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ReadDataToGrid(int page, int itemCount)
        {
            List<ProductSummary> list=new List<ProductSummary>();
            list=ProductDataAccess.GetInstance().GetProduct(page,itemCount);
            int count=0;
            count = ProductDataAccess.GetInstance().GetCountProduct();
            return Json(new ViewModel(list,count,null),JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            return PartialView();
        }
        [HttpPost]
        public void Create(string ProductName, string Avatar, string Description, decimal price, string Unit, string ShortcutKey)
        {
            tblProduct p = new tblProduct()
            {
                ProductName = ProductName,
                Avatar = Avatar,
                Description = Description,
                Unit = Unit,
                ShortcutKey = ShortcutKey
            };
            ProductDataAccess.GetInstance().Create(p, price);
        }

    }
}
