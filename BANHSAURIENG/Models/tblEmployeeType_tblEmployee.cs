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
    
    public partial class tblEmployeeType_tblEmployee
    {
        public long ObjectID { get; set; }
        public System.DateTime CreateDate { get; set; }
        public int EmployeeTypeID { get; set; }
        public decimal Salary { get; set; }
    
        public virtual tblEmployeeType tblEmployeeType { get; set; }
    }
}
