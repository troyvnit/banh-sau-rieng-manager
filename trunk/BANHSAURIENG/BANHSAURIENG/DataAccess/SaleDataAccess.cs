using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BANHSAURIENG.Models;

namespace BANHSAURIENG.DataAccess
{
    public class SaleDataAccess
    {
        private static SaleDataAccess _instance = null;
        private BSR_Entities _db;
        public static SaleDataAccess GetInstance()
        {
            if (_instance == null)
            {
                _instance = new SaleDataAccess();
            }
            return _instance;
        }

        public SaleDataAccess()
        {
            _db = new BSR_Entities();
            _db.Configuration.ProxyCreationEnabled = false;
        }
        //end of ini class
        public IEnumerable<spGetProductByID_Result> GetProductByID(int productID)
        {
            return _db.spGetProductByID(productID);
        }

        public long CreateBill(tblBill bill)
        {
            try
            {
                _db.tblBills.Add(bill);
                _db.SaveChanges();
                return bill.BillID;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        public bool CreateBillDetail(tblBillDetail detail)
        {
            try
            {
                _db.tblBillDetails.Add(detail);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        // destroy 
        ~SaleDataAccess()
        {
            _db = null;
            _db.Dispose();
        }
    }
}