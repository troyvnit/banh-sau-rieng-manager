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
    
    public partial class tblCampaign
    {
        public tblCampaign()
        {
            this.CampainDetails = new HashSet<CampainDetail>();
            this.tblVouchers = new HashSet<tblVoucher>();
        }
    
        public long CampainID { get; set; }
        public string CampainName { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime DueDate { get; set; }
        public System.TimeSpan StartHours { get; set; }
        public System.TimeSpan DueHours { get; set; }
        public bool ForBill { get; set; }
        public Nullable<decimal> Discount { get; set; }
        public Nullable<bool> isPercent { get; set; }
        public System.DateTime CreateDate { get; set; }
        public long CreateBy { get; set; }
        public System.DateTime UpdateDate { get; set; }
        public long UpdateBy { get; set; }
    
        public virtual ICollection<CampainDetail> CampainDetails { get; set; }
        public virtual ICollection<tblVoucher> tblVouchers { get; set; }
    }
}