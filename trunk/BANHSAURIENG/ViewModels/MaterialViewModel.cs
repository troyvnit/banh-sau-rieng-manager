using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BANHSAURIENG.ViewModels
{
    public class MaterialViewModel
    {
        public long MaterialID { get; set; }
        public string MaterialName { get; set; }
        public string Description { get; set; }
        public string Unit { get; set; }
        public string Avatar { get; set; }
        public bool isHidden { get; set; }
        public bool isDelete { get; set; }
    }
}