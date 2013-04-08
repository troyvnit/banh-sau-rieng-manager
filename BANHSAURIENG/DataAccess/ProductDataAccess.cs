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
        public IEnumerable<BANHSAURIENG.ViewModels.ProductViewModel> GetProduct(int page, int itemCount)
        {
            IEnumerable<tblProduct> result;
            List<BANHSAURIENG.ViewModels.ProductViewModel> resultmodel = new List<BANHSAURIENG.ViewModels.ProductViewModel>();
            try
            {
                result = (from p in _db.tblProducts
                          where p.isDelete == false && p.isHidden == false
                          
                          select p).OrderBy(i => i.OrderID).Skip((page - 1) * itemCount).Take(itemCount).ToList();
                //result = _db.spTest().ToList();
                foreach (var pro in result)
                {
                    BANHSAURIENG.ViewModels.ProductViewModel provm = new BANHSAURIENG.ViewModels.ProductViewModel { Avatar = pro.Avatar, Description = pro.Description, isDelete = pro.isDelete, isHidden = pro.isHidden, Price = pro.Price, OrderID = pro.OrderID, ProductID = pro.ProductID, ProductName = pro.ProductName, ShortcutKey = pro.ShortcutKey, Unit = pro.Unit };
                    resultmodel.Add(provm);
                }
                return resultmodel;
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
                          orderby p.OrderID ascending 
                          select p).ToList();
                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }
        public List<tblMaterial> GetMaterial()
        {
            List<tblMaterial> result = new List<tblMaterial>();
            try
            {
                result = (from p in _db.tblMaterials
                          where p.isDelete == false && p.isHidden == false
                          orderby p.MaterialID descending
                          select p).ToList();
                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }
        public List<spGetMaterialByProductID_Result> GetMaterial(int proID)
        {
            List<spGetMaterialByProductID_Result> result = new List<spGetMaterialByProductID_Result>();
            try
            {
                result = _db.spGetMaterialByProductID(proID).ToList();
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

        public bool CreateProductMaterial(Product_Material m)
        {
            try
            {
                _db.Product_Material.Add(m);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Create(tblProduct p)
        {
            try
            {
                p.isDelete = p.isHidden = false;
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
                p.isDelete = p.isHidden = false;
                _db.Entry(_db.tblProducts.Find(p.ProductID)).CurrentValues.SetValues(p);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool UpdateOrderID(int proID, int orID)
        {
            try
            {
                var p = (from pr in _db.tblProducts
                         where pr.ProductID == proID
                         select pr).FirstOrDefault();
                p.OrderID = orID;
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool EditProductMaterial(Product_Material m)
        {
            var pm = new Product_Material();
            try
            {
                pm = (from p in _db.Product_Material
                              where p.MaterialID == m.MaterialID && p.ProductID == m.ProductID
                              select p).FirstOrDefault();
                if (pm != null)
                {
                    pm.MaterialQuantity = m.MaterialQuantity;
                    _db.SaveChanges();
                }
                else
                {
                    CreateProductMaterial(m);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public BANHSAURIENG.ViewModels.ProductViewModel GetProductDetail(int proID)
        {
            try
            {
                var pr = _db.tblProducts.Find(proID);
                BANHSAURIENG.ViewModels.ProductViewModel p = new BANHSAURIENG.ViewModels.ProductViewModel { Avatar = pr.Avatar, Description = pr.Description, isDelete = pr.isDelete, isHidden = pr.isHidden, Price = pr.Price, OrderID = pr.OrderID, ProductID = pr.ProductID, ProductName = pr.ProductName, ShortcutKey = pr.ShortcutKey, Unit = pr.Unit};
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