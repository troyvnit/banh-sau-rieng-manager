using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BANHSAURIENG.ViewModels
{
    public class EmployeeViewModel
    {
        public long EmployeeID { get; set; }
        public string Avartar { get; set; }
        public string EmployeeName { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<bool> isDelete { get; set; }
        public Nullable<bool> isHidden { get; set; }
    }
}