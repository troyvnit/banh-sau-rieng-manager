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
                return -1;
            }
        }
        public List<tblAccount> Read()
        {
            return _db.tblAccounts.ToList();
        }
        public bool Create(tblAccount acc)
        {
            try
            {
                tblObject o = new tblObject();
                o.ObjectName = acc.Username;
                _db.tblObjects.Add(o);
                _db.SaveChanges();
                acc.ObjectID = o.ObjectID;
                _db.tblAccounts.Add(acc);
                _db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Update(tblAccount acc)
        {
            try
            {
                _db.Entry(_db.tblAccounts.Find(acc.ObjectID)).CurrentValues.SetValues(acc);
                _db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Destroy(tblAccount acc)
        {
            try
            {
                _db.tblObjects.Remove(_db.tblObjects.Find(acc.ObjectID));
                _db.tblAccounts.Remove(_db.tblAccounts.Find(acc.ObjectID));
                _db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool AddSerial(Serial s)
        {
            try
            {
                _db.Serials.Add(s);
                _db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool CheckSerial(string hddserial)
        {
            try
            {
                Serial ss = _db.Serials.Where(se => se.HDDSerial == hddserial).FirstOrDefault();
                return FunctionLibrary.DecryptStringAES(ss.Code, "troylee") == hddserial;
            }
            catch (Exception)
            {
                return false;
                throw;
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
