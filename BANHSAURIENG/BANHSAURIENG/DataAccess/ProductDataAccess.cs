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
        public List<ProductSummary> GetProduct(int page, int itemCount)
        {
            List<ProductSummary> result = new List<ProductSummary>();
            try
            {
                result = (from p in _db.tblProducts
                          join pr in _db.tblProductPrices on p.ProductID equals pr.ProductID where p.isDelete==false && p.isHidden ==false
                          select new ProductSummary { ProductID = p.ProductID, Avatar = p.Avatar, Description = p.Description, Price = pr.Price, ProductName = p.ProductName, ShortcutKey = p.ShortcutKey, Unit = p.Unit }).OrderByDescending(i => i.ProductID).Skip((page - 1) * itemCount).Take(itemCount).ToList();
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
        public bool Create(tblProduct p,decimal price)
        {
            try
            {
                p.isDelete = p.isHidden= false;
                _db.tblProducts.Add(p);
                _db.SaveChanges();
                tblProductPrice pr = new tblProductPrice();
                pr.ProductID = p.ProductID;
                pr.Price = price;
                pr.CreateDate = DateTime.Now;
                _db.tblProductPrices.Add(pr);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
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