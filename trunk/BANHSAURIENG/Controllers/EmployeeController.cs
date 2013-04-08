using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BANHSAURIENG.DataAccess;
using BANHSAURIENG.Models;

namespace BANHSAURIENG.Controllers
{
    public class EmployeeController : Controller
    {
        //
        // GET: /Employee/

        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ReadEmployeeToGrid(int page, int itemCount)
        {
            IEnumerable<BANHSAURIENG.ViewModels.EmployeeViewModel> list;
            list = EmployeeDataAccess.GetInstance().GetEmployee(page, itemCount);
            int count = 0;
            count = EmployeeDataAccess.GetInstance().GetCountEmployee();
            return Json(new ViewModel(list, count, null), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetHistoryToGrid(int page, int itemCount, int EmployeeID)
        {
            IEnumerable<BANHSAURIENG.ViewModels.EmployeeHistoryViewModel> list;
            list = EmployeeDataAccess.GetInstance().GetEmployeeHistory(page, itemCount, EmployeeID);
            int count = 0;
            count = EmployeeDataAccess.GetInstance().GetCountEmployeeHistory(EmployeeID);
            return Json(new ViewModel(list, count, null), JsonRequestBehavior.AllowGet);
        }
        public ActionResult CreateEmployee()
        {
            return PartialView();
        }
        [HttpPost]
        public void CreateEmployee(string EmployeeName, string Avartar)
        {
            tblEmployee p = new tblEmployee()
            {
                EmployeeName = EmployeeName,
                Avartar = Avartar,
                CreateDate = DateTime.Now
            };
            EmployeeDataAccess.GetInstance().CreateEmployee(p);
        }
        public ActionResult EditEmployee(int mID)
        {
            ViewBag.EmployeeEdit = EmployeeDataAccess.GetInstance().GetEmployeeByID(mID);
            return PartialView();
        }
        [HttpPost]
        public void EditEmployee(int EmployeeID, string EmployeeName, string Avartar)
        {
            tblEmployee p = new tblEmployee()
            {
                EmployeeID = EmployeeID,
                EmployeeName = EmployeeName,
                Avartar = Avartar,
                CreateDate = DateTime.Now
            };
            EmployeeDataAccess.GetInstance().EditEmployee(p);
        }
        public void HideEmployee(int mID)
        {
            EmployeeDataAccess.GetInstance().HideEmployee(mID);
        }
    }
}
