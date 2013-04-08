using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BANHSAURIENG.Models;
using BANHSAURIENG.ViewModels;
using BANHSAURIENG.DataAccess;
using System.Web.Helpers;
namespace BANHSAURIENG.Controllers
{
    [Authorize]
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
            IEnumerable<BANHSAURIENG.ViewModels.ProductViewModel> list;
            list = ProductDataAccess.GetInstance().GetProduct(page, itemCount);
            int count = 0;
            count = ProductDataAccess.GetInstance().GetCountProduct();
            return Json(new ViewModel(list, count, null), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            ViewBag.Material = ProductDataAccess.GetInstance().GetMaterial();
            return PartialView();
        }
        [HttpPost]
        public void Create(string ProductName, string Avatar, string Description, decimal price, string Unit, string ShortcutKey, int OrderID, List<string> Material)
        {
            tblProduct p = new tblProduct()
            {
                ProductName = ProductName,
                Avatar = Avatar,
                Description = Description,
                Unit = Unit,
                ShortcutKey = ShortcutKey,
                Price = price,
                OrderID = OrderID
            };
            if (ProductDataAccess.GetInstance().Create(p) && Material != null && Material.Count() > 0)
            {
                foreach (var m in Material)
                {
                    var sub = m.Split('_');
                    Product_Material pm = new Product_Material { ProductID = p.ProductID, MaterialID = Int64.Parse(sub[1]), MaterialQuantity = Decimal.Parse(sub[2]) };
                    ProductDataAccess.GetInstance().CreateProductMaterial(pm);
                }
            }
        }


        public ActionResult Edit(int proID)
        {
            ViewBag.ProductEdit = ProductDataAccess.GetInstance().GetProductDetail(proID);
            ViewBag.Material = ProductDataAccess.GetInstance().GetMaterial(proID);
            return PartialView();
        }

        [HttpPost]
        public void Edit(int ProductID, string ProductName, string Avatar, string Description, decimal price, string Unit, string ShortcutKey, int OrderID, List<string> Material)
        {
            tblProduct p = new tblProduct();
            p.ProductID = ProductID;
            p.ProductName = ProductName;
            p.Avatar = Avatar;
            p.Description = Description;
            p.Unit = Unit;
            p.ShortcutKey = ShortcutKey;
            p.Price = price;
            p.OrderID = OrderID;
            if (ProductDataAccess.GetInstance().Edit(p) && Material != null && Material.Count() > 0)
            {
                foreach (var m in Material)
                {
                    var sub = m.Split('_');
                    Product_Material pm = new Product_Material { ProductID = p.ProductID, MaterialID = Int64.Parse(sub[1]), MaterialQuantity = Decimal.Parse(sub[2]) };
                    ProductDataAccess.GetInstance().EditProductMaterial(pm);
                }
            }
        }
        public void Hide(int proID)
        {
            ProductDataAccess.GetInstance().Hide(proID);
        }

        #region Material
        #endregion

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
