using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BANHSAURIENG.Models;
using BANHSAURIENG.DataAccess;
using System.Web.Helpers;
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
            IEnumerable<tblProduct> list;
            list= ProductDataAccess.GetInstance().GetProduct(page,itemCount);
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
                ShortcutKey = ShortcutKey,
                Price=price
            };
            ProductDataAccess.GetInstance().Create(p);
        }

       
        public ActionResult Edit(long proID)
        {
            ViewBag.ProductEdit = ProductDataAccess.GetInstance().GetProductDetail(proID);
            return PartialView();
        }

        [HttpPost]
        public void Edit(int ProductID, string ProductName, string Avatar, string Description, decimal price, string Unit, string ShortcutKey)
        {
            tblProduct p = new tblProduct();
            p.ProductID = ProductID;
            p.ProductName = ProductName;
            p.Avatar = Avatar;
            p.Description = Description;
            p.Unit = Unit;
            p.ShortcutKey = ShortcutKey;
            p.Price = price;
            ProductDataAccess.GetInstance().Edit(p);
        }
        public void Hide(long proID)
        {
            ProductDataAccess.GetInstance().Hide(proID);
        }

        [HttpPost]
        public void UploadImage()
        {
            var image = WebImage.GetImageFromRequest();
            string imageName = image.FileName;
            ImageDataAccess.GetInstance().UploadImage(image, imageName, "~/Images/Product/", 120);
        }

        [HttpPost]
        public void RemoveImage(string fileNames)
        {
            ImageDataAccess.GetInstance().DeleteImage("~/Images/Product/", fileNames);
        }
    }
}
