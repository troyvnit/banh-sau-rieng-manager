using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BANHSAURIENG.Models
{
    public class ProductSummary
    {
        public int ProductID { get; set; }
        public string Avatar { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string Unit { get; set; }
        public string ShortcutKey { get; set; }
        public decimal Price { get; set; }
    }
}