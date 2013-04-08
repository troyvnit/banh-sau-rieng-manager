using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BANHSAURIENG.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class StoreHistoryViewModel
    {
        public long MaterialID { get; set; }
        public short StoreID { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public decimal MaterialQuantity { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<decimal> MaterialQuantityAlert { get; set; }
        public string Note { get; set; }
        public int SMId { get; set; }
    }
}