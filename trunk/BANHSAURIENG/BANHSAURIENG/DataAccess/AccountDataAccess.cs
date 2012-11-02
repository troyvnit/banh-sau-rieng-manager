using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BANHSAURIENG.Models;
namespace BANHSAURIENG.DataAccess
{
    public class AccountDataAccess
    {
        private static AccountDataAccess _instance = null;
        private BSR_Entities _db;
        public static AccountDataAccess GetInstance()
        {
            if (_instance == null)
            {
                _instance = new AccountDataAccess();
            }
            return _instance;
        }

        public AccountDataAccess()
        {
            _db = new BSR_Entities();
            _db.Configuration.ProxyCreationEnabled = false;
        }
        //end of ini class
        public Int32 checkLogin(string username, string password)
        {
            try
            {
                if (_db.spCheckLogin(username, password).Count() > 0)
                {
                    return 0;
                }
                else return 1;
            }
            catch (Exception e)
            {
                return 5;
            }
        }
        // destroy 
        ~AccountDataAccess()
        {
            _db = null;
            _db.Dispose();
        }

    }
}
