using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BANHSAURIENG.Models;
using System.Data.Objects;
namespace BANHSAURIENG.DataAccess
{
    public class StoreDataAccess
    {
        private static StoreDataAccess _instance = null;
        private BSR_Entities _db;
        public static StoreDataAccess GetInstance()
        {
            if (_instance == null)
            {
                _instance = new StoreDataAccess();
            }
            return _instance;
        }

        public StoreDataAccess()
        {
            _db = new BSR_Entities();
            _db.Configuration.ProxyCreationEnabled = false;
        }
        //end of ini class
        public IEnumerable<BANHSAURIENG.ViewModels.StoreViewModel> GetStore(int page, int itemCount)
        {
            IEnumerable<tblStore> result;
            List<BANHSAURIENG.ViewModels.StoreViewModel> resultmodel = new List<BANHSAURIENG.ViewModels.StoreViewModel>();
            try
            {
                result = (from p in _db.tblStores
                          select p).OrderByDescending(i => i.StoreID).Skip((page - 1) * itemCount).Take(itemCount).ToList();
                //result = _db.spTest().ToList();
                foreach (var store in result)
                {
                    BANHSAURIENG.ViewModels.StoreViewModel storevm = new BANHSAURIENG.ViewModels.StoreViewModel { Address = store.Address, CityID = store.CityID, DistrictID = store.DistrictID, Fax = store.Fax, Fax1 = store.Fax1, StoreID = store.StoreID, WardID = store.WardID, Phone = store.Phone, Phone1 = store.Phone1 };
                    resultmodel.Add(storevm);
                }
                return resultmodel;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<tblStore> GetStore()
        {
            List<tblStore> result = new List<tblStore>();
            try
            {
                result = (from p in _db.tblStores
                          where p.isDelete == false && p.isHidden == false
                          orderby p.StoreID descending
                          select p).ToList();
                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }
        public int GetCountStore()
        {
            return _db.tblStores.Count();
        }
        public List<BANHSAURIENG.ViewModels.StoreHistoryViewModel> GetStoreHistory(int page, int itemCount, int materialID)
        {
            IEnumerable<Store_Material> result;
            List<BANHSAURIENG.ViewModels.StoreHistoryViewModel> resultmodel = new List<BANHSAURIENG.ViewModels.StoreHistoryViewModel>();
            try
            {
                result = (from p in _db.Store_Material
                          where p.MaterialID == materialID
                          select p).OrderByDescending(i => i.StoreID).Skip((page - 1) * itemCount).Take(itemCount).ToList();
                //result = _db.spTest().ToList();
                foreach (var store in result)
                {
                    BANHSAURIENG.ViewModels.StoreHistoryViewModel storevm = new BANHSAURIENG.ViewModels.StoreHistoryViewModel { CreateDate = store.CreateDate, StoreID = store.StoreID, MaterialID = store.MaterialID, MaterialQuantityAlert = store.MaterialQuantityAlert, SMId = store.SMId, MaterialQuantity = store.MaterialQuantity, Note = store.Note, Price = store.Price};
                    resultmodel.Add(storevm);
                }
                return resultmodel;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public int GetCountStoreHistory(int materialID)
        {
            return _db.Store_Material.Where(m => m.MaterialID == materialID).Count();
        }
        public List<spGetMaterialByStoreID_Result> GetMaterial(int page, int itemCount)
        {
            List<spGetMaterialByStoreID_Result> result;
            try
            {
                result = _db.spGetMaterialByStoreID(1).Where(i => i.isDelete == false).OrderByDescending(i => i.MaterialID).Skip((page - 1) * itemCount).Take(itemCount).ToList();
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<spGetMaterialByStoreID_Result> GetMaterial()
        {
            List<spGetMaterialByStoreID_Result> result;
            try
            {
                result = _db.spGetMaterialByStoreID(1).Where(i => i.isDelete == false).OrderByDescending(i => i.MaterialID).ToList();
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public int GetCountMaterial()
        {
            return _db.tblMaterials.Where(i => i.isDelete == false).Count();
        }
        public int GetCountMaterialByStoreID()
        {
            return _db.spGetMaterialByStoreID(1).Where(i => i.isDelete == false).Count();
        }
        public bool CreateMaterial(tblMaterial m)
        {
            try
            {
                m.isDelete = m.isHidden = false;
                _db.tblMaterials.Add(m);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool CreateStoreMaterial(Store_Material m)
        {
            try
            {
                _db.Store_Material.Add(m);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool EditMaterial(tblMaterial m)
        {
            try
            {
                m.isDelete = m.isHidden = false;
                var material = (from mt in _db.tblMaterials
                                where mt.MaterialID == m.MaterialID
                                select mt).FirstOrDefault();
                material.MaterialName = m.MaterialName;
                material.Avatar = m.Avatar;
                material.Unit = m.Unit;
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool EditStoreMaterial(Store_Material m)
        {
            CreateStoreMaterial(new Store_Material{CreateDate = DateTime.Now, MaterialID = m.MaterialID, MaterialQuantity = m.MaterialQuantity, MaterialQuantityAlert = m.MaterialQuantityAlert, Note = m.Note, Price = m.Price, StoreID = m.StoreID});
            IEnumerable<Store_Material> sm;
            try
            {
                sm = (from s in _db.Store_Material
                      where s.MaterialID == m.MaterialID && s.StoreID == m.StoreID
                      select s);
                foreach(var s in sm)
                {
                    s.MaterialQuantityAlert = m.MaterialQuantityAlert;
                    s.Price = m.Price;
                }
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public spGetMaterialByStoreID_Result GetMaterialByID(int mID)
        {
            try
            {
                spGetMaterialByStoreID_Result p = new spGetMaterialByStoreID_Result();
                p = _db.spGetMaterialByStoreID(1).Where(m => m.MaterialID == mID).FirstOrDefault();
                return p;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public bool HideMaterial(int mID)
        {
            try
            {
                tblMaterial p = _db.tblMaterials.Find(mID);
                p.isDelete = true;
                _db.Entry(_db.tblMaterials.Find(mID)).CurrentValues.SetValues(p);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public List<spGetMaterialByStoreID_Result> GetMaterialExist()
        {
            List<spGetMaterialByStoreID_Result> result;
            try
            {
                result = _db.spGetMaterialByStoreID(1).Where(i => i.isDelete == false).OrderByDescending(i => i.MaterialID).ToList();
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        //fixed asset
        public List<ViewModels.FixedAssetViewModel> GetFixedAsset(int page, int itemCount)
        {
            List<ViewModels.FixedAssetViewModel> result = new List<ViewModels.FixedAssetViewModel>();
            try
            {
                List<tblFixedAsset> fas = _db.tblFixedAssets.Where(i => i.isDelete == false).OrderByDescending(i => i.FixedAssetID).Skip((page - 1) * itemCount).Take(itemCount).ToList();
                foreach (var fa in fas)
                {
                    ViewModels.FixedAssetViewModel fav = new ViewModels.FixedAssetViewModel();
                    fav.DepreciationPeriod = fa.DepreciationPeriod;
                    fav.Description = fa.Description;
                    fav.FixedAssetID = fa.FixedAssetID;
                    fav.FixedAssetName = fa.FixedAssetName;
                    fav.isDelete = fa.isDelete;
                    fav.isHiddent = fa.isHiddent;
                    fav.Price = fa.Price;
                    fav.PurchaseDate = fa.PurchaseDate;
                    fav.SupllierID = fa.SupllierID;
                    result.Add(fav);
                }
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public int GetCountFixedAsset()
        {
            return _db.tblFixedAssets.Where(i => i.isDelete == false).Count();
        }
        public bool CreateFixedAsset(tblFixedAsset m)
        {
            try
            {
                m.isDelete = false;
                _db.tblFixedAssets.Add(m);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public tblFixedAsset GetFixedAssetByID(int mID)
        {
            return _db.tblFixedAssets.Find(mID);
        }
        public bool EditFixedAsset(tblFixedAsset m)
        {
            try
            {
                m.isDelete = false;
                _db.Entry(_db.tblFixedAssets.Find(m.FixedAssetID)).CurrentValues.SetValues(m);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool HideFixedAsset(int mID)
        {
            try
            {
                tblFixedAsset p = _db.tblFixedAssets.Find(mID);
                p.isDelete = true;
                _db.Entry(_db.tblFixedAssets.Find(mID)).CurrentValues.SetValues(p);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        // destroy 
        ~StoreDataAccess()
        {
            _db = null;
            _db.Dispose();
        }
    }
}