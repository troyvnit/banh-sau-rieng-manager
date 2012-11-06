using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BANHSAURIENG.Models;
using System.Data.Objects;
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
        public List<tblComBo> GetCombo(int page, int itemCount)
        {
            List<tblComBo> result = new List<tblComBo>();
            try
            {
                result = (from p in _db.tblComBoes
                          where p.isDelete == false && p.isHidden == false
                          select p).OrderByDescending(i => i.ComboID).Skip((page - 1) * itemCount).Take(itemCount).ToList();
                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }

        public List<tblComBo> GetCombo()
        {
            return _db.tblComBoes.Where(c=>c.isDelete==false && c.isHidden==false).ToList();
        }

        public int GetCountProduct()
        {
            return _db.tblComBoes.Count();
        }

        public int Create(tblComBo combo)
        {
            try
            {
                combo.isDelete = combo.isHidden = false;
                _db.tblComBoes.Add(combo);
                _db.SaveChanges();
                return combo.ComboID;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public bool CreateComboProduct(int comboID, int productID)
        {
            try
            {
                _db.spAddComboProduct(comboID, productID);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public int Edit(tblComBo combo)
        {
            try
            {
                _db.Entry(_db.tblComBoes.Find(combo.ComboID)).CurrentValues.SetValues(combo);
                _db.SaveChanges();
                return combo.ComboID;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public bool DeleteComboProduct(int comboID)
        {
            try
            {
                _db.spDeleteComboProduct(comboID);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Hide(int comboID)
        {
            try
            {
                tblComBo p = _db.tblComBoes.Find(comboID);
                p.isDelete = true;
                _db.Entry(_db.tblComBoes.Find(comboID)).CurrentValues.SetValues(p);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public tblComBo GetComboDetail(int comboID)
        {
            return _db.tblComBoes.Find(comboID);
        }

        public List<tblProduct> GetComboProduct()
        {
            return _db.tblProducts.Where(p => p.isDelete==false && p.isHidden==false).ToList();
        }
        public ObjectResult<tblProduct> GetComboProduct(int comboID)
        {
            return _db.spGetProductByComboID(comboID);
        }
        // destroy 
        ~ComboDataAccess()
        {
            _db = null;
            _db.Dispose();
        }
    }
}