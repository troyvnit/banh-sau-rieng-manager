using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BANHSAURIENG.Models;
namespace BANHSAURIENG.DataAccess
{
    public class ProductDataAccess
    {
        private static ProductDataAccess _instance = null;
        private BSR_Entities _db;
        public static ProductDataAccess GetInstance()
        {
            if (_instance == null)
            {
                _instance = new ProductDataAccess();
            }
            return _instance;
        }

        public ProductDataAccess()
        {
            _db = new BSR_Entities();
            _db.Configuration.ProxyCreationEnabled = false;
        }
        //end of ini class
        // destroy 
        ~ProductDataAccess()
        {
            _db = null;
            _db.Dispose();
        }
    }
}