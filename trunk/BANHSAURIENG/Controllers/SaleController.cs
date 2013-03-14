using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BANHSAURIENG.DataAccess;
using BANHSAURIENG.Models;
using System.Management;

namespace BANHSAURIENG.Controllers
{
    [Authorize]
    public class SaleController : Controller
    {
        public ActionResult Index()
        {
            var str = "";
            ManagementObject disk = new ManagementObject("Win32_LogicalDisk.DeviceID=\"" + "C" + ":\"");
            disk.Get();
            str = disk["VolumeSerialNumber"].ToString();
            if(AccountDataAccess.GetInstance().CheckSerial(str))
            {
                ViewBag.Product = ProductDataAccess.GetInstance().GetProduct();
                ViewBag.Combo = ComboDataAccess.GetInstance().GetCombo();
                ViewBag.CheckSerial = true;
                return View();
            }
            else
            {
                ViewBag.CheckSerial = false;
                return View();
            }
        }
        public JsonResult Register(string serial)
        {
            var str = "";
            ManagementObject disk = new ManagementObject("Win32_LogicalDisk.DeviceID=\"" + "C" + ":\"");
            disk.Get();
            str = disk["VolumeSerialNumber"].ToString();
            if (FunctionLibrary.DecryptStringAES(serial, "troylee") == str)
            {
                Serial s = new Serial();
                s.HDDSerial = str;
                s.Code = serial;
                AccountDataAccess.GetInstance().AddSerial(s);
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetProductByID(string productID)
        {
            try
            {
                Int32 ProductID = Int32.Parse(productID);
                BANHSAURIENG.ViewModels.ProductViewModel list = ProductDataAccess.GetInstance().GetProductDetail(ProductID);
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public ActionResult GetComboByID(string comboID)
        {
            try
            {
                Int32 ComboID = Int32.Parse(comboID);
                BANHSAURIENG.ViewModels.ComboViewModel com = ComboDataAccess.GetInstance().GetComboDetail(ComboID);
                return Json(com, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return null;
            }
        }

        [HttpPost]
        public ActionResult ReportOnDay()
        {
            try
            {
                IEnumerable<spReportOnDayDetails_Result> report = SaleDataAccess.GetInstance().ReportOnDay();
                return Json(report, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return null;
            }
        }
        [HttpPost]
        public JsonResult PrintForm(string result, string totalmoney, string generalcompain)
        {
           var result1=result.Split(':');
           long billID=0;
           decimal price=0;
           int total = Int32.Parse(totalmoney);
           int gcompain = Int32.Parse(generalcompain);
           int orID = 0;
           tblBill bill = new tblBill()
           {
               CreateDate = DateTime.Now,
               TotalMoney = total,
               CreateBy = 1,
               GeneralCompain = gcompain
           };
           billID = SaleDataAccess.GetInstance().CreateBill(bill); 
           foreach (var item in result1)
           {
               if (!string.IsNullOrEmpty(item))
               {
                   var item1 = item.Split('_');
                   var subitem1 = item1[0].Split('-');
                   if (subitem1.Length > 1)
                   {
                       int comboID = Int32.Parse(subitem1[1].ToString());
                       short quantity = short.Parse(item1[1].ToString());
                       price = (decimal)SaleDataAccess.GetInstance().GetComboByID(comboID).Price;
                       tblBillDetail detail = new tblBillDetail()
                       {
                           ComboID = comboID,
                           ComboQUantity = quantity,
                           BillID = billID,
                           ComboPrice = price,
                           ComboCompain = Int32.Parse(item1[2].ToString()),
                           Note = item1[3]
                       };
                       SaleDataAccess.GetInstance().CreateBillDetail(detail);
                   }
                   else
                   {
                       int productID = Int32.Parse(item1[0].ToString());
                       short quantity = short.Parse(item1[1].ToString());
                       price = (decimal)SaleDataAccess.GetInstance().GetProductByID(productID).Price;
                       tblBillDetail detail = new tblBillDetail()
                       {
                           ProductID = productID,
                           ProductQuantity = quantity,
                           BillID = billID,
                           ProductPrice = price,
                           ProductCompain = Int32.Parse(item1[2].ToString()),
                           Note = item1[3]
                       };
                       SaleDataAccess.GetInstance().CreateBillDetail(detail);
                   }
               }
           }
           if (Session["BillIDBANHSAURIENG"] != null)
           {
               orID = (int)Session["BillIDBANHSAURIENG"];
               orID = orID + 1;
               Session["BillIDBANHSAURIENG"] = orID;
           }
            List<spGetMaterialByStoreID_Result> list;
            list = StoreDataAccess.GetInstance().GetMaterial();
            for (int index = 0; index < list.Count; index++)
            {
                var m = list[index];
                if (m.MaterialQuantityAlert < m.MaterialQuantityNet)
                {
                    list.RemoveAt(index);
                    index--;
                }
            }
            Result r = new Result();
            r.orID = orID;
            r.list = list;
            r.billID = billID;
            return Json(r, JsonRequestBehavior.AllowGet); ;
        }
        [HttpPost]
        public JsonResult SortButton(string result)
        {
            var result1 = result.Split(':');
            foreach (var item in result1)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    var item1 = item.Split('_');
                    var subitem1 = item1[0].Split('-');
                    if (subitem1.Length > 1)
                    {
                        int comboID = Int32.Parse(subitem1[1].ToString());
                        int orID = short.Parse(item1[1].ToString());
                    }
                    else
                    {
                        int productID = Int32.Parse(item1[0].ToString());
                        int orID = short.Parse(item1[1].ToString());
                        ProductDataAccess.GetInstance().UpdateOrderID(productID,orID);
                    }
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet); 
        }
    }
}

public class Result
{
    public int orID;
    public long billID;
    public List<spGetMaterialByStoreID_Result> list;
}