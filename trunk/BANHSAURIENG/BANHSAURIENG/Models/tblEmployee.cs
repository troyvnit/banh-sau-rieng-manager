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
    
    public partial class tblEmployee
    {
        public tblEmployee()
        {
            this.tblEmployeeType_tblEmployee = new HashSet<tblEmployeeType_tblEmployee>();
            this.tblShop_tblStore_tblEmployee = new HashSet<tblShop_tblStore_tblEmployee>();
        }
    
        public long ObjectID { get; set; }
        public string Avartar { get; set; }
        public long CreateBy { get; set; }
        public System.DateTime LastUpdate { get; set; }
        public long UpdateBy { get; set; }
        public System.DateTime CreateDate { get; set; }
        public bool isDelete { get; set; }
        public bool isHiddent { get; set; }
    
        public virtual tblObject tblObject { get; set; }
        public virtual ICollection<tblEmployeeType_tblEmployee> tblEmployeeType_tblEmployee { get; set; }
        public virtual ICollection<tblShop_tblStore_tblEmployee> tblShop_tblStore_tblEmployee { get; set; }
    }
}
