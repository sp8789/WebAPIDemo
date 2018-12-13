using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCart_WebAPI.Models
{
    public class CommonModel
    {

        //public class tblShoppingProduct
        //{
        //    public int ProductId { get; set; }
        //    public int Quantity { get; set; }
        //    public decimal Price { get; set; }
        //    public List<OrderSummary> OrderSummary { get; set; }
        //}


        public class tblShoppingProduct
        {
            public int ProductId { get; set; }
            public int Quantity { get; set; }
            public decimal Price { get; set; }
        }

        public class tblShoppingProductViewModel
        {
            public List<tblShoppingProduct> Products { get; set; }
            public int CouponId { get; set; }
            public string CouponCode { get; set; }
            public int UserId { get; set; }
        }

    }
}