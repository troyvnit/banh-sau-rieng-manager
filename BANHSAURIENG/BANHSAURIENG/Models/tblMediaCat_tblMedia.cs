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
    
    public partial class tblMediaCat_tblMedia
    {
        public short MediaCatID { get; set; }
        public int MediaID { get; set; }
        public Nullable<byte> IndexASC { get; set; }
    
        public virtual tblMedia tblMedia { get; set; }
        public virtual tblMediaCat tblMediaCat { get; set; }
    }
}
