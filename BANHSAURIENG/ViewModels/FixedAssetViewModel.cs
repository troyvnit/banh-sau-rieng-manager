using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BANHSAURIENG.ViewModels
{
    public class FixedAssetViewModel
    {
        public int FixedAssetID { get; set; }
        public string FixedAssetName { get; set; }
        public string Description { get; set; }
        public Nullable<long> SupllierID { get; set; }
        public decimal Price { get; set; }
        public System.DateTime PurchaseDate { get; set; }
        public byte DepreciationPeriod { get; set; }
        public bool isDelete { get; set; }
        public bool isHiddent { get; set; }
    }
}