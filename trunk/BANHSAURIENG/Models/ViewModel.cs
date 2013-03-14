using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BANHSAURIENG.Models
{
    public class ViewModel
    {
        public object Data { get; set; }
        public long? ItemCount { get; set; }
        public object SubData { get; set; }

        public ViewModel(object data, long? itemCount, object subData)
        {
            this.Data = data;
            this.ItemCount = itemCount;
            this.SubData = subData;
        }

        public ViewModel()
        {
        }
    }
}