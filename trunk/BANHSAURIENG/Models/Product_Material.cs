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
    
    public partial class Product_Material
    {
        public long MaterialID { get; set; }
        public int ProductID { get; set; }
        public decimal MaterialQuantity { get; set; }
    
        public virtual tblMaterial tblMaterial { get; set; }
        public virtual tblProduct tblProduct { get; set; }
    }
}
