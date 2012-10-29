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
        public List<BillModel> GetAllBills()
        {
            List<BillModel> bills = new List<BillModel>();
            foreach (tblBill bill in _db.tblBills)
            {
                bills.Add(new BillModel() { 
                    BillID = bill.BillID,
                    CustomerID = bill.CustomerID,
                    CreateBy = bill.CreateBy,
                    CreateDate = bill.CreateDate,
                    isDiscount = bill.isDiscount,
                    CampainID = bill.CampainID,
                    TotalMoney = bill.TotalMoney,
                    VoucherCode = bill.VoucherCode
                });
            }
            return bills;
        }
        public List<BillDetailModel> GetDetailByBillID(long billID)
        {
            List<BillDetailModel> details = new List<BillDetailModel>();
            foreach (tblBillDetail detail in _db.tblBillDetails.Where(b => b.BillID == billID))
            {
                details.Add(new BillDetailModel()
                {
                    BillDetailID = detail.BillDetailID,
                    BillID = detail.BillID,
                    ProductID = detail.ProductID,
                    Quantity = detail.Quantity,
                    Price = detail.Price
                });
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