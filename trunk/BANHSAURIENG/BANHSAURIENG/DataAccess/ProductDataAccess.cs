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
        public IEnumerable<tblProduct> GetProduct(int page, int itemCount)
        {
            IEnumerable<tblProduct> result;
            try
            {
                result = (from p in _db.tblProducts
                          where p.isDelete==false && p.isHidden ==false
                          orderby p.ProductID descending
                          select new tblProduct { ProductID = p.ProductID , Avatar = p.Avatar, Description = p.Description, Price = p.Price, ProductName = p.ProductName, ShortcutKey = p.ShortcutKey, Unit = p.Unit }).OrderByDescending(i => i.ProductID).Skip((page - 1) * itemCount).Take(itemCount).ToList();
                //result = _db.spTest().ToList();
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<tblProduct> GetProduct()
        {
            List<tblProduct> result = new List<tblProduct>();
            try
            {
                result = (from p in _db.tblProducts
                          where p.isDelete == false && p.isHidden == false
                          orderby p.ProductID descending
                          select p).ToList();
                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }
        public int GetCountProduct()
        {
            return _db.tblProducts.Count();
        }

        public bool Create(tblProduct p)
        {
            try
            {
                p.isDelete = p.isHidden= false;
                _db.tblProducts.Add(p);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Hide(long proID)
        {
            try
            {
                tblProduct p = _db.tblProducts.Find(proID);
                p.isDelete = true;
                _db.Entry(_db.tblProducts.Find(proID)).CurrentValues.SetValues(p);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Edit(tblProduct p)
        {
            try
            {
                p.isDelete =p.isHidden= false;
                _db.Entry(_db.tblProducts.Find(p.ProductID)).CurrentValues.SetValues(p);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public tblProduct GetProductDetail(long proID)
        {
            try
            {
                tblProduct p = new tblProduct();
                p= _db.tblProducts.Find(proID);
                return p;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // destroy 
        ~ProductDataAccess()
        {
            _db = null;
            _db.Dispose();
        }
    }
}