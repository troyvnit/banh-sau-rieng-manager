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
    
    public partial class tblProduct
    {
        public tblProduct()
        {
            this.CampainDetails = new HashSet<CampainDetail>();
            this.Product_Material = new HashSet<Product_Material>();
            this.tblBillDetails = new HashSet<tblBillDetail>();
            this.tblProductPrices = new HashSet<tblProductPrice>();
        }
    
        public int ProductID { get; set; }
        public bool isHidden { get; set; }
        public bool isDelete { get; set; }
        public string Avatar { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string ProductCode { get; set; }
        public string Unit { get; set; }
    
        public virtual ICollection<CampainDetail> CampainDetails { get; set; }
        public virtual ICollection<Product_Material> Product_Material { get; set; }
        public virtual ICollection<tblBillDetail> tblBillDetails { get; set; }
        public virtual ICollection<tblProductPrice> tblProductPrices { get; set; }
    }
}
