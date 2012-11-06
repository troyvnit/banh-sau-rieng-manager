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
            List<tblComBo> list = new List<tblComBo>();
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
        public void Create(string ComboName, string Avatar, decimal price, string ShortcutKey,string productItem)
        {
            tblComBo combo = new tblComBo();
            combo.ComboName=ComboName;
            combo.Avatar = Avatar;
            combo.Price = price;
            combo.ShortcutKey = ShortcutKey;
            int comboID = ComboDataAccess.GetInstance().Create(combo);
            if (!string.IsNullOrEmpty(productItem) && comboID != 0)
            {
                string[] p = productItem.Split('-');
                foreach (var item in p)
                {
                    int p1=0;
                    if(Int32.TryParse(item.ToString(),out p1)){
                        ComboDataAccess.GetInstance().CreateComboProduct(comboID,p1);
                    }
                }
            }
        }

        public ActionResult Edit(int comboID)
        {
            ViewBag.Combo = ComboDataAccess.GetInstance().GetComboDetail(comboID);
            ViewBag.ComboProduct = ComboDataAccess.GetInstance().GetComboProduct();
            ViewBag.ComboProduct1 = ComboDataAccess.GetInstance().GetComboProduct(comboID);
            return PartialView();
        }
        [HttpPost]
        public void Edit(int ComboID,string ComboName, string Avatar, decimal price, string ShortcutKey, string productItem)
        {
            tblComBo combo = new tblComBo();
            combo.ComboID = ComboID;
            combo.ComboName = ComboName;
            combo.Avatar = Avatar;
            combo.Price = price;
            combo.ShortcutKey = ShortcutKey;
            int comboID = ComboDataAccess.GetInstance().Edit(combo);
            if (comboID != 0)
            {
                ComboDataAccess.GetInstance().DeleteComboProduct(comboID);
                string[] p = productItem.Split('-');
                foreach (var item in p)
                {
                    int p1 = 0;
                    if (Int32.TryParse(item.ToString(), out p1))
                    {
                        ComboDataAccess.GetInstance().CreateComboProduct(comboID, p1);
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
