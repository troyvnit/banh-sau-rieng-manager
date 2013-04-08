using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BANHSAURIENG.Models;
using System.Data.Objects;
namespace BANHSAURIENG.DataAccess
{
    using BANHSAURIENG.ViewModels;

    public class EmployeeDataAccess
    {
        private static EmployeeDataAccess _instance = null;
        private BSR_Entities _db;
        public static EmployeeDataAccess GetInstance()
        {
            if (_instance == null)
            {
                _instance = new EmployeeDataAccess();
            }
            return _instance;
        }

        public EmployeeDataAccess()
        {
            _db = new BSR_Entities();
            _db.Configuration.ProxyCreationEnabled = false;
        }
        //end of ini class

        public List<BANHSAURIENG.ViewModels.EmployeeHistoryViewModel> GetEmployeeHistory(int page, int itemCount, int EmployeeID)
        {
            IEnumerable<Employee_Attendance> result;
            List<BANHSAURIENG.ViewModels.EmployeeHistoryViewModel> resultmodel = new List<BANHSAURIENG.ViewModels.EmployeeHistoryViewModel>();
            try
            {
                result = (from p in _db.Employee_Attendance
                          where p.EmployeeID == EmployeeID
                          select p).OrderByDescending(i => i.EmployeeID).Skip((page - 1) * itemCount).Take(itemCount).ToList();
                //result = _db.spTest().ToList();
                foreach (var re in result)
                {
                    BANHSAURIENG.ViewModels.EmployeeHistoryViewModel revm = new BANHSAURIENG.ViewModels.EmployeeHistoryViewModel { EAId = re.EAId, EmployeeID = re.EmployeeID, CreateDate = re.CreateDate, Note = re.Note, ImageUrl = re.ImageUrl};
                    resultmodel.Add(revm);
                }
                return resultmodel;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public int GetCountEmployeeHistory(int EmployeeID)
        {
            return _db.Employee_Attendance.Where(m => m.EmployeeID == EmployeeID).Count();
        }
        public List<ViewModels.EmployeeViewModel> GetEmployee(int page, int itemCount)
        {
            List<ViewModels.EmployeeViewModel> result = new List<EmployeeViewModel>();
            try
            {
                List<tblEmployee> res = _db.tblEmployees.Where(i => i.isDelete == false).OrderByDescending(i => i.EmployeeID).Skip((page - 1) * itemCount).Take(itemCount).ToList();
                foreach (var re in res)
                {
                    result.Add(new EmployeeViewModel
                        {
                            EmployeeID = re.EmployeeID,
                            Avartar = re.Avartar,
                            CreateDate = re.CreateDate,
                            EmployeeName = re.EmployeeName,
                            isDelete = re.isDelete,
                            isHidden = re.isHidden
                        });
                }
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public int GetCountEmployee()
        {
            return _db.tblEmployees.Where(i => i.isDelete == false).Count();
        }
        public int GetCountMaterialByStoreID()
        {
            return _db.spGetMaterialByStoreID(1).Where(i => i.isDelete == false).Count();
        }
        public bool CreateEmployee(tblEmployee e)
        {
            try
            {
                e.isDelete = e.isHidden = false;
                _db.tblEmployees.Add(e);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool CreateHistoryEmployee(Employee_Attendance m)
        {
            try
            {
                _db.Employee_Attendance.Add(m);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool EditEmployee(tblEmployee e)
        {
            try
            {
                e.isDelete = e.isHidden = false;
                var employee = (from ep in _db.tblEmployees
                                where ep.EmployeeID == e.EmployeeID
                                select ep).FirstOrDefault();
                employee.EmployeeName = e.EmployeeName;
                employee.Avartar = e.Avartar;
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        
        public tblEmployee GetEmployeeByID(int mID)
        {
            try
            {
                tblEmployee re = _db.tblEmployees.Where(m => m.EmployeeID == mID).FirstOrDefault();
                ViewModels.EmployeeViewModel p = new EmployeeViewModel
                        {
                            EmployeeID = re.EmployeeID,
                            Avartar = re.Avartar,
                            CreateDate = re.CreateDate,
                            EmployeeName = re.EmployeeName,
                            isDelete = re.isDelete,
                            isHidden = re.isHidden
                        };
                return re;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public bool HideEmployee(int mID)
        {
            try
            {
                tblEmployee p = _db.tblEmployees.Find(mID);
                p.isDelete = true;
                _db.Entry(_db.tblEmployees.Find(mID)).CurrentValues.SetValues(p);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
       
        // destroy 
        ~EmployeeDataAccess()
        {
            _db = null;
            _db.Dispose();
        }
    }
}