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
            this.Employee_Attendance = new HashSet<Employee_Attendance>();
        }
    
        public long EmployeeID { get; set; }
        public string Avartar { get; set; }
        public string EmployeeName { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<bool> isDelete { get; set; }
        public Nullable<bool> isHidden { get; set; }
    
        public virtual ICollection<Employee_Attendance> Employee_Attendance { get; set; }
    }
}