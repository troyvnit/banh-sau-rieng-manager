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
    
    public partial class tblShop
    {
        public tblShop()
        {
            this.tblFixedAsset_Store_Shop = new HashSet<tblFixedAsset_Store_Shop>();
            this.tblShop_tblStore_tblEmployee = new HashSet<tblShop_tblStore_tblEmployee>();
        }
    
        public short ShopID { get; set; }
        public string Address { get; set; }
        public Nullable<int> CityID { get; set; }
        public Nullable<int> WardID { get; set; }
        public Nullable<int> DistrictID { get; set; }
        public string Phone { get; set; }
        public string Phone1 { get; set; }
        public string Fax { get; set; }
        public string Fax1 { get; set; }
        public bool isDelete { get; set; }
        public bool isHidden { get; set; }
    
        public virtual ICollection<tblFixedAsset_Store_Shop> tblFixedAsset_Store_Shop { get; set; }
        public virtual ICollection<tblShop_tblStore_tblEmployee> tblShop_tblStore_tblEmployee { get; set; }
    }
}
