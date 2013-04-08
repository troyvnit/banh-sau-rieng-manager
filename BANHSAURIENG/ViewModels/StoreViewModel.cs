using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BANHSAURIENG.ViewModels
{
    public class StoreViewModel
    {
        public short StoreID { get; set; }
        public string Address { get; set; }
        public Nullable<int> CityID { get; set; }
        public Nullable<int> WardID { get; set; }
        public Nullable<int> DistrictID { get; set; }
        public string Phone { get; set; }
        public string Phone1 { get; set; }
        public string Fax { get; set; }
        public string Fax1 { get; set; }
        public bool isHidden { get; set; }
        public bool isDelete { get; set; }
    }
}