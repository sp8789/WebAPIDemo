using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCart_WebAPI.Models
{
    public class OrderDTO
    {
        public int? ProductId { get; set; }
        public string ProductName { get; set; }
        public Nullable<decimal> ProductPrice { get; set; }
        public int OrderId { get; set; }
        public string OrderNumber { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<int> CouponId { get; set; }
        public Nullable<decimal> OrderTotal { get; set; }

        public virtual CouponMaster CouponMaster { get; set; }
        public virtual UserMaster UserMaster { get; set; }
    }
}