//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BANHSAURIENG.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblFixedAsset
    {
        public tblFixedAsset()
        {
            this.tblFixedAsset_Store_Shop = new HashSet<tblFixedAsset_Store_Shop>();
        }
    
        public int FixedAssetID { get; set; }
        public string FixedAssetName { get; set; }
        public string Description { get; set; }
        public Nullable<long> SupllierID { get; set; }
        public decimal Price { get; set; }
        public System.DateTime PurchaseDate { get; set; }
        public byte DepreciationPeriod { get; set; }
        public bool isDelete { get; set; }
        public bool isHiddent { get; set; }
    
        public virtual ICollection<tblFixedAsset_Store_Shop> tblFixedAsset_Store_Shop { get; set; }
    }
}