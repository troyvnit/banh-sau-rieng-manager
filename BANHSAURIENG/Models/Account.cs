using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BANHSAURIENG.Models
{
    public class Account
    {
        public long ObjectID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool isEnable { get; set; }
        public System.DateTime LastLogin { get; set; }
        public System.DateTime CreateDate { get; set; }
        public System.DateTime LastUpdate { get; set; }
        public bool isSystem { get; set; }
        public bool isDelete { get; set; }
        public bool isHidden { get; set; }
    }
}