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
        public int ProductID { get; set; }
        [DisplayName("Số lượng")]
        public short Quantity { get; set; }
        [DisplayName("Đơn giá")]
        public decimal Price { get; set; }
    }
}