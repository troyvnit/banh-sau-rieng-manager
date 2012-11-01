using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BANHSAURIENG.DataAccess;
using BANHSAURIENG.Models;

namespace BANHSAURIENG.Controllers
{
    public class SaleController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetProductByID(string productID)
        {
            try
            {
                Int32 ProductID = Int32.Parse(productID);
                return Json(SaleDataAccess.GetInstance().GetProductByID(ProductID), JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return null;
            }
        }
        [HttpPost]
        public ActionResult PrintForm(string result, string totalmoney, string voucher)
        {
           var result1=result.Split(':');
           long billID=0;
           decimal price=0;
           int total = Int32.Parse(totalmoney);
           tblBill bill = new tblBill()
           {
               CreateDate = DateTime.Now,
               TotalMoney = total,
               CreateBy = 7,
               VoucherCode = voucher
           };
           billID = SaleDataAccess.GetInstance().CreateBill(bill); ;
           foreach (var item in result1)
           {
               if (!string.IsNullOrEmpty(item))
               {
                   var item1 = item.Split('_');
                   int productID = Int32.Parse(item1[0].ToString());
                   short quantity = short.Parse(item1[1].ToString());
                   price = SaleDataAccess.GetInstance().GetProductByID(productID).First().Price;
                   tblBillDetail detail = new tblBillDetail()
                   {
                       ProductID = productID,
                       ProductQuantity = quantity,
                       BillID = billID,
                       ProductPrice = price
                   };
                   SaleDataAccess.GetInstance().CreateBillDetail(detail);
               }
           }
           return View();
        }
    }
}