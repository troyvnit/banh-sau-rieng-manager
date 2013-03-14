using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BANHSAURIENG.ViewModels
{
    public class ComboViewModel
    {
        public int ComboID { get; set; }
        public string ComboName { get; set; }
        public string ShortcutKey { get; set; }
        public string Avatar { get; set; }
        public bool isDelete { get; set; }
        public bool isHidden { get; set; }
        public decimal Price { get; set; }
    }
}