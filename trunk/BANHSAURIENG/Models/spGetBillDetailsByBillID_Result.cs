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
    
    public partial class spGetBillDetailsByBillID_Result
    {
        public Nullable<long> BillDetailID { get; set; }
        public Nullable<long> BillID { get; set; }
        public Nullable<int> ProductID { get; set; }
        public Nullable<short> ProductQuantity { get; set; }
        public Nullable<decimal> ProductPrice { get; set; }
        public Nullable<int> ComboID { get; set; }
        public Nullable<short> ComboQUantity { get; set; }
        public Nullable<decimal> ComboPrice { get; set; }
        public string ProductName { get; set; }
        public string ComboName { get; set; }
        public Nullable<int> ProductCompain { get; set; }
        public Nullable<int> ComboCompain { get; set; }
        public string Note { get; set; }
    }
}
