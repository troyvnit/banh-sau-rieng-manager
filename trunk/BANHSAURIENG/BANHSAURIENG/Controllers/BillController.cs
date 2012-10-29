using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BANHSAURIENG.DataAccess;
using BANHSAURIENG.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace BANHSAURIENG.Controllers
{
    public class BillController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetAllBills([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                List<BillModel> bills = new List<BillModel>();
                bills = BillDataAccess.GetInstance().GetAllBills();
                return Json(bills.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public ActionResult GetDetailByBillID(long billID, [DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                List<BillDetailModel> details = new List<BillDetailModel>();
                details = BillDataAccess.GetInstance().GetDetailByBillID(billID);
                return Json(details.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}