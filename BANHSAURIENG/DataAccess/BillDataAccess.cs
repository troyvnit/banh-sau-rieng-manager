using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BANHSAURIENG.Models;

namespace BANHSAURIENG.DataAccess
{
    public class BillDataAccess
    {
        private static BillDataAccess _instance = null;
        private BSR_Entities _db;
        public static BillDataAccess GetInstance()
        {
            if (_instance == null)
            {
                _instance = new BillDataAccess();
            }
            return _instance;
        }

        public BillDataAccess()
        {
            _db = new BSR_Entities();
            _db.Configuration.ProxyCreationEnabled = false;
        }
        //end of ini class
        public List<BillModel> GetAllBills(string from, string to)
        {
            List<BillModel> bills = new List<BillModel>();
            foreach (tblBill bill in _db.spGetAllBills(FunctionLibrary.ConvertDate(from), FunctionLibrary.ConvertDate(to)))
            {
                bills.Add(new BillModel() { 
                    BillID = bill.BillID,
                    CustomerID = bill.CustomerID,
                    CreateBy = bill.CreateBy,
                    CreateDate = bill.CreateDate,
                    isDiscount = bill.isDiscount,
                    CampainID = bill.CampainID,
                    TotalMoney = bill.TotalMoney,
                    GeneralCompain = bill.GeneralCompain.Value
                });
            }
            return bills;
        }
        public List<spGetBillDetailsByBillID_Result> GetDetailByBillID(long billID)
        {
            List<spGetBillDetailsByBillID_Result> details = new List<spGetBillDetailsByBillID_Result>();
            foreach (spGetBillDetailsByBillID_Result detail in _db.spGetBillDetailsByBillID(billID))
            {
                spGetBillDetailsByBillID_Result d = new spGetBillDetailsByBillID_Result();
                d.BillDetailID = detail.BillDetailID;
                d.BillID = detail.BillID;
                d.ProductID = detail.ProductID;
                d.ProductQuantity = detail.ProductQuantity;
                d.ProductPrice = detail.ProductPrice;
                d.ComboID = detail.ComboID;
                d.ComboQUantity = detail.ComboQUantity;
                d.ComboPrice = detail.ComboPrice;
                d.ComboName = detail.ComboName;
                d.ProductName = detail.ProductName;
                d.ComboCompain = detail.ComboCompain;
                d.ProductCompain = detail.ProductCompain;
                d.Note = detail.Note;
                details.Add(d);
            }
            return details;
        }
        // destroy 
        ~BillDataAccess()
        {
            _db = null;
            _db.Dispose();
        }
    }
}