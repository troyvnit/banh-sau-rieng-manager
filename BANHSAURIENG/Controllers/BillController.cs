using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BANHSAURIENG.DataAccess;
using BANHSAURIENG.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System.Data;
using System.Data.Metadata;
using System.IO;

namespace BANHSAURIENG.Controllers
{
    [Authorize]
    public class BillController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetAllBills([DataSourceRequest] DataSourceRequest request, string from, string to)
        {
            try
            {
                List<BillModel> bills = new List<BillModel>();
                bills = BillDataAccess.GetInstance().GetAllBills(from,to);
                return Json(bills.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public ActionResult PrintReport([DataSourceRequest] DataSourceRequest request, string from, string to)
        {
            try
            {
                List<BillModel> bills = new List<BillModel>();
                bills = BillDataAccess.GetInstance().GetAllBills(from, to);
                List<spGetBillDetailsByBillID_Result> details = new List<spGetBillDetailsByBillID_Result>();
                foreach (BillModel bill in bills)
                {
                    foreach (spGetBillDetailsByBillID_Result detail in BillDataAccess.GetInstance().GetDetailByBillID(bill.BillID))
                    {
                        details.Add(detail);
                    }
                }
                string refrom = from, reto = to;
                refrom = refrom.Replace('/', '_');
                reto = reto.Replace('/', '_');
                return ExportCSV("report_from_" + refrom + "_to_" + reto + ".csv", details);
            }
            catch (Exception)
            {
                return null;
            }
        }
        //Export CSV
        private FileStreamResult ExportCSV(string FileName, List<BANHSAURIENG.Models.spGetBillDetailsByBillID_Result> bills)
        {
            string savePath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath(@"~/ExportTemplates"), FileName);

            var sw = new StreamWriter(savePath, false);
            List<ViewModels.GetBillDetailByBillIDDataTable> billsdatatables = new List<ViewModels.GetBillDetailByBillIDDataTable>();
            foreach (var bill in bills)
            {
                billsdatatables.Add(new ViewModels.GetBillDetailByBillIDDataTable { 
                    BillDetailID = bill.BillDetailID != null ? bill.BillDetailID.Value : 0,
                    BillID = bill.BillID != null ? bill.BillID.Value : 0,
                    ComboCompain = bill.ComboCompain != null ? bill.ComboCompain.Value : 0,
                    ComboID = bill.ComboID != null ? bill.ComboID.Value : 0,
                    ComboPrice = bill.ComboPrice != null ? bill.ComboPrice.Value : 0,
                    ComboQUantity = bill.ComboQUantity != null ? bill.ComboQUantity.Value : 0,
                    ProductCompain = bill.ProductCompain != null ? bill.ProductCompain.Value : 0,
                    ProductID = bill.ProductID != null ? bill.ProductID.Value : 0,
                    ProductPrice = bill.ProductPrice != null ? bill.ProductPrice.Value : 0,
                    ProductQuantity = bill.ProductQuantity != null ? bill.ProductQuantity.Value : 0
                });
            }
            System.Data.DataTable dt = FunctionLibrary.ConvertListToDataTable<ViewModels.GetBillDetailByBillIDDataTable>(billsdatatables);
            int iColCount = dt.Columns.Count;
            for (int i = 0; i < iColCount; i++)
            {
                sw.Write(dt.Columns[i]);
                if (i < iColCount - 1)
                {
                    sw.Write(",");
                }
            }
            sw.Write(sw.NewLine);
            foreach (System.Data.DataRow dr in dt.Rows)
            {
                for (int i = 0; i < iColCount; i++)
                {
                    if (!Convert.IsDBNull(dr[i]))
                    {
                        sw.Write(dr[i].ToString());
                    }
                    if (i < iColCount - 1)
                    {
                        sw.Write(",");
                    }
                }
                sw.Write(sw.NewLine);
            }
            sw.Close();
            FileStream fs = new FileStream(savePath, FileMode.Open, FileAccess.Read);
            MemoryStream ms = new MemoryStream();
            ms.SetLength(fs.Length);
            fs.Read(ms.GetBuffer(), 0, (int)fs.Length);
            fs.Dispose();
            return File(ms, "text/excel", FileName);
        }
        public ActionResult GetDetailByBillID(long billID, [DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                List<spGetBillDetailsByBillID_Result> details = new List<spGetBillDetailsByBillID_Result>();
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