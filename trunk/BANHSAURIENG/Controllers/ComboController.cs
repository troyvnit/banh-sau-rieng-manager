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
    [Authorize]
    public class ComboController : Controller
    {
        //
        // GET: /Combo/
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ReadDataToGrid(int page, int itemCount)
        {
            List<BANHSAURIENG.ViewModels.ComboViewModel> list = new List<BANHSAURIENG.ViewModels.ComboViewModel>();
            list = ComboDataAccess.GetInstance().GetCombo(page, itemCount);
            int count = 0;
            count = ComboDataAccess.GetInstance().GetCountProduct();
            return Json(new ViewModel(list, count, null), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            ViewBag.ListProduct = ProductDataAccess.GetInstance().GetProduct();
            return PartialView();
        }

        [HttpPost]
        public void Create(string ComboName, string Avatar, decimal price, string ShortcutKey, List<string> productItems)
        {
            tblComBo combo = new tblComBo();
            combo.ComboName = ComboName;
            combo.Avatar = Avatar;
            combo.Price = price;
            combo.ShortcutKey = ShortcutKey;
            int comboID = ComboDataAccess.GetInstance().Create(combo);
            if (comboID != 0 && productItems.Count() > 0)
            {
                foreach (var m in productItems)
                {
                    var sub = m.Split('_');
                    int proID = 0;
                    int quantity = 0;
                    if (Int32.TryParse(sub[0], out proID) && Int32.TryParse(sub[1], out quantity))
                    {
                        ComboDataAccess.GetInstance().CreateComboProduct(comboID, proID, quantity);
                    }
                }
            }
        }

        public ActionResult Edit(int comboID)
        {
            ViewBag.Combo = ComboDataAccess.GetInstance().GetComboDetail(comboID);
            ViewBag.ComboProduct = ComboDataAccess.GetInstance().GetComboProduct(comboID);
            return PartialView();
        }
        [HttpPost]
        public void Edit(int ComboID, string ComboName, string Avatar, decimal price, string ShortcutKey, List<string> productItems)
        {
            tblComBo combo = new tblComBo();
            combo.ComboID = ComboID;
            combo.ComboName = ComboName;
            combo.Avatar = Avatar;
            combo.Price = price;
            combo.ShortcutKey = ShortcutKey;
            int comboID = ComboDataAccess.GetInstance().Edit(combo);
            if (comboID != 0 && productItems.Count() > 0)
            {
                foreach (var m in productItems)
                {
                    var sub = m.Split('_');
                    int proID = 0;
                    int quantity = 0;
                    if (Int32.TryParse(sub[0], out proID) && Int32.TryParse(sub[1], out quantity))
                    {
                        tblProduct_Combo pc = new tblProduct_Combo { ComboID = comboID, ProductID = proID, Quantity = quantity };
                        ComboDataAccess.GetInstance().EditComboProduct(pc);
                    }
                }
            }
        }
        public void Hide(int comboID)
        {
            ComboDataAccess.GetInstance().Hide(comboID);
        }

        [HttpPost]
        public void UploadImage()
        {
            var image = WebImage.GetImageFromRequest();
            string imageName = image.FileName;
            ImageDataAccess.GetInstance().UploadImage(image, imageName, "~/Images/Combo/", 120);
        }

        [HttpPost]
        public void RemoveImage(string fileNames)
        {
            ImageDataAccess.GetInstance().DeleteImage("~/Images/Combo/", fileNames);
        }
    }
}
