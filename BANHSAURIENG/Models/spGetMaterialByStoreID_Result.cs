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
    
    public partial class spGetMaterialByStoreID_Result
    {
        public long MaterialID { get; set; }
        public string MaterialName { get; set; }
        public string Description { get; set; }
        public string Unit { get; set; }
        public string Avatar { get; set; }
        public Nullable<bool> isHidden { get; set; }
        public Nullable<bool> isDelete { get; set; }
        public decimal MaterialQuantity { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public short StoreID { get; set; }
        public decimal MaterialQuantityUsed { get; set; }
        public decimal MaterialQuantityNet { get; set; }
        public decimal Price { get; set; }
        public decimal MaterialQuantityAlert { get; set; }
    }
}
