using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BANHSAURIENG.ViewModels
{
    public class EmployeeHistoryViewModel
    {
        public int EAId { get; set; }
        public Nullable<long> EmployeeID { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string ImageUrl { get; set; }
        public string Note { get; set; }
    }
}