using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BANHSAURIENG.Models
{
    public class BillDetailModel
    {
        public long BillDetailID { get; set; }
        [DisplayName("Mã hóa đơn")]
        public long BillID { get; set; }
        [DisplayName("Mã sản phẩm")]
        public Nullable<int> ProductID { get; set; }
        [DisplayName("Tên sản phẩm")]
        public string ProductName { get; set; }
        [DisplayName("Số lượng")]
        public Nullable<short> ProductQuantity { get; set; }
        [DisplayName("Đơn giá")]
        public Nullable<decimal> ProductPrice { get; set; }
        [DisplayName("Mã combo")]
        public Nullable<int> ComboID { get; set; }
        [DisplayName("Tên combo")]
        public string ComboName { get; set; }
        [DisplayName("Số lượng")]
        public Nullable<short> ComboQUantity { get; set; }
        [DisplayName("Đơn giá")]
        public Nullable<decimal> ComboPrice { get; set; }
        [DisplayName("% chiết khấu")]
        public Nullable<int> ProductCompain { get; set; }
        [DisplayName("% chiết khấu")]
        public Nullable<int> ComboCompain { get; set; }
        [DisplayName("Ghi chú")]
        public string Note { get; set; }
    }
}