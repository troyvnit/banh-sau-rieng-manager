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
    public class StoreController : Controller
    {
        //
        // GET: /Store/
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult ReadDataToGrid(int page, int itemCount)
        {
            IEnumerable<BANHSAURIENG.ViewModels.StoreViewModel> list;
            list = StoreDataAccess.GetInstance().GetStore(page, itemCount);
            int count = 0;
            count = StoreDataAccess.GetInstance().GetCountStore();
            return Json(new ViewModel(list, count, null), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ReadMaterialToGrid(int page, int itemCount)
        {
            IEnumerable<spGetMaterialByStoreID_Result> list;
            list = StoreDataAccess.GetInstance().GetMaterial(page, itemCount);
            int count = 0;
            count = StoreDataAccess.GetInstance().GetCountMaterialByStoreID();
            return Json(new ViewModel(list, count, null), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetHistoryToGrid(int page, int itemCount, int materialID)
        {
            IEnumerable<BANHSAURIENG.ViewModels.StoreHistoryViewModel> list;
            list = StoreDataAccess.GetInstance().GetStoreHistory(page, itemCount, materialID);
            int count = 0;
            count = StoreDataAccess.GetInstance().GetCountStoreHistory(materialID);
            return Json(new ViewModel(list, count, null), JsonRequestBehavior.AllowGet);
        }
        public ActionResult CreateMaterial()
        {
            return PartialView();
        }
        [HttpPost]
        public void CreateMaterial(string MaterialName, string Avatar, string Description, decimal MaterialQuantity, decimal MaterialQuantityAlert, string Unit, decimal MaterialPrice)
        {
            tblMaterial p = new tblMaterial()
            {
                MaterialName = MaterialName,
                Avatar = Avatar,
                Description = Description,
                Unit = Unit
            };
            if (StoreDataAccess.GetInstance().CreateMaterial(p))
            {
                Store_Material pm = new Store_Material { MaterialID = p.MaterialID, StoreID = 1, MaterialQuantity = MaterialQuantity, MaterialQuantityAlert = MaterialQuantityAlert, CreateDate = DateTime.Now, Price = MaterialPrice, Note = Description};
                StoreDataAccess.GetInstance().CreateStoreMaterial(pm);
            }
        }
        public ActionResult EditMaterial(int mID)
        {
            ViewBag.MaterialEdit = StoreDataAccess.GetInstance().GetMaterialByID(mID);
            return PartialView();
        }
        [HttpPost]
        public void EditMaterial(int MaterialID, string MaterialName, string Avatar, string Description, decimal MaterialQuantity, decimal MaterialQuantityAlert, string Unit, decimal MaterialPrice)
        {
            tblMaterial p = new tblMaterial()
            {
                MaterialID = MaterialID,
                MaterialName = MaterialName,
                Avatar = Avatar,
                Description = Description,
                Unit = Unit
            };
            if (StoreDataAccess.GetInstance().EditMaterial(p))
            {
                Store_Material pm = new Store_Material { MaterialID = p.MaterialID, StoreID = 1, MaterialQuantity = MaterialQuantity, MaterialQuantityAlert = MaterialQuantityAlert, CreateDate = DateTime.Now, Price = MaterialPrice, Note = Description};
                StoreDataAccess.GetInstance().EditStoreMaterial(pm);
            }
        }
        public void HideMaterial(int mID)
        {
            StoreDataAccess.GetInstance().HideMaterial(mID);
        }

        #region Fixed Asset
        public ActionResult ViewFixedAsset()
        {
            return View();
        }
        [HttpPost]
        public JsonResult ReadFixedAssetToGrid(int page, int itemCount)
        {
            IEnumerable<ViewModels.FixedAssetViewModel> list;
            list = StoreDataAccess.GetInstance().GetFixedAsset(page, itemCount);
            int count = 0;
            count = StoreDataAccess.GetInstance().GetCountFixedAsset();
            return Json(new ViewModel(list, count, null), JsonRequestBehavior.AllowGet);
        }
        public ActionResult CreateFixedAsset()
        {
            return PartialView();
        }
        [HttpPost]
        public void CreateFixedAsset(string FixedAssetName, string Description, decimal FixedAssetPrice)
        {
            tblFixedAsset p = new tblFixedAsset()
            {
                FixedAssetName = FixedAssetName,
                Description = Description,
                Price = FixedAssetPrice,
                PurchaseDate = DateTime.Now
            };
            StoreDataAccess.GetInstance().CreateFixedAsset(p);
        }
        public ActionResult EditFixedAsset(int mID)
        {
            ViewBag.FixedAssetEdit = StoreDataAccess.GetInstance().GetFixedAssetByID(mID);
            return PartialView();
        }
        [HttpPost]
        public void EditFixedAsset(int FixedAssetID, string FixedAssetName, string Description, decimal FixedAssetPrice)
        {
            tblFixedAsset p = new tblFixedAsset()
            {
                FixedAssetID = FixedAssetID,
                FixedAssetName = FixedAssetName,
                Description = Description,
                Price = FixedAssetPrice,
                PurchaseDate = DateTime.Now
            };
            StoreDataAccess.GetInstance().EditFixedAsset(p);
        }
        public void HideFixedAsset(int mID)
        {
            StoreDataAccess.GetInstance().HideFixedAsset(mID);
        }
        #endregion
    }
}
