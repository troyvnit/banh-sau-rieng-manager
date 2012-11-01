using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BANHSAURIENG.Models;
namespace BANHSAURIENG.DataAccess
{
    public class ComboDataAccess
    {
        private static ComboDataAccess _instance = null;
        private BSR_Entities _db;
        public static ComboDataAccess GetInstance()
        {
            if (_instance == null)
            {
                _instance = new ComboDataAccess();
            }
            return _instance;
        }

        public ComboDataAccess()
        {
            _db = new BSR_Entities();
            _db.Configuration.ProxyCreationEnabled = false;
        }
        //end of ini class
        // destroy 
        ~ComboDataAccess()
        {
            _db = null;
            _db.Dispose();
        }
    }
}