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
    
    public partial class tblAccountGroup
    {
        public tblAccountGroup()
        {
            this.tblRoles = new HashSet<tblRole>();
            this.tblAccounts = new HashSet<tblAccount>();
        }
    
        public int AccountGroupID { get; set; }
        public string GroupName { get; set; }
        public string Description { get; set; }
        public bool isDelete { get; set; }
        public bool isHiddent { get; set; }
    
        public virtual ICollection<tblRole> tblRoles { get; set; }
        public virtual ICollection<tblAccount> tblAccounts { get; set; }
    }
}