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
    
    public partial class tblBill
    {
        public tblBill()
        {
            this.tblBillDetails = new HashSet<tblBillDetail>();
        }
    
        public long BillID { get; set; }
        public Nullable<long> CustomerID { get; set; }
        public long CreateBy { get; set; }
        public System.DateTime CreateDate { get; set; }
        public Nullable<bool> isDiscount { get; set; }
        public Nullable<long> CampainID { get; set; }
        public decimal TotalMoney { get; set; }
        public string VoucherCode { get; set; }
        public Nullable<bool> Copy { get; set; }
        public Nullable<long> DeliveryStaff { get; set; }
        public Nullable<System.DateTime> OrderTime { get; set; }
    
        public virtual tblAccount tblAccount { get; set; }
        public virtual ICollection<tblBillDetail> tblBillDetails { get; set; }
    }
}
