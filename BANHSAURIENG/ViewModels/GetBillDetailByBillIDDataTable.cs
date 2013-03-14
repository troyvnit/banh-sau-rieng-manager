using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BANHSAURIENG.ViewModels
{
    public class GetBillDetailByBillIDDataTable
    {
        public long BillDetailID { get; set; }
        public long BillID { get; set; }
        public int ProductID { get; set; }
        public int ProductQuantity { get; set; }
        public decimal ProductPrice { get; set; }
        public int ComboID { get; set; }
        public int ComboQUantity { get; set; }
        public decimal ComboPrice { get; set; }
        public string ProductName { get; set; }
        public string ComboName { get; set; }
        public int ProductCompain { get; set; }
        public int ComboCompain { get; set; }
    }
}