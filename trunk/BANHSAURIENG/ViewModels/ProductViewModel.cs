using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BANHSAURIENG.ViewModels
{
    public class ProductViewModel
    {
        public int ProductID { get; set; }
        public bool isHidden { get; set; }
        public bool isDelete { get; set; }
        public string Avatar { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public Nullable<int> OrderID { get; set; }
        public string Unit { get; set; }
        public string ShortcutKey { get; set; }
        public Nullable<decimal> Price { get; set; }
    }
}