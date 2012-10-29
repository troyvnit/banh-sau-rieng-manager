using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
namespace BANHSAURIENG.Models
{
    public class BillModel
    {
        public long BillID { get; set; }
        [DisplayName("Khách hàng")]
        public Nullable<long> CustomerID { get; set; }
        [DisplayName("Người tạo")]
        public long CreateBy { get; set; }
        [DisplayName("Ngày tạo")]
        public System.DateTime CreateDate { get; set; }
        [DisplayName("Chiết khấu ?")]
        public Nullable<bool> isDiscount { get; set; }
        [DisplayName("Khuyến mãi")]
        public Nullable<long> CampainID { get; set; }
        [DisplayName("Tổng tiền")]
        public decimal TotalMoney { get; set; }
        [DisplayName("Mã Voucher")]
        public string VoucherCode { get; set; }
    }
}