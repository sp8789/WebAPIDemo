using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ShoppingCart_WebAPI.Models;

namespace ShoppingCart_WebAPI.Controllers
{
    public class OrderController : ApiController
    {
        private DemoDatabaseEntities db = new DemoDatabaseEntities();

        // GET: api/OrderDetails
        public IQueryable<OrderDetail> GetOrderDetails()
        {
            return db.OrderDetails;
        }

        // GET: api/OrderDetails/5
        [ResponseType(typeof(OrderDetail))]
        public async Task<IHttpActionResult> GetOrderDetail(int id)
        {
            OrderDetail orderDetail = await db.OrderDetails.FindAsync(id);
            if (orderDetail == null)
            {
                return NotFound();
            }

            return Ok(orderDetail);
        }

        [ResponseType(typeof(OrderSummary))]
        [Route("api/ProcessOrder")]
        public async Task<IHttpActionResult> ProcessOrder(CommonModel.tblShoppingProductViewModel objcommonmodel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            List<OrderDetail> orderDetails = new List<OrderDetail>();
            OrderDetail objod = new OrderDetail();
            OrderSummary objsumm = new OrderSummary();
            objsumm.OrderTotal = 0;
            foreach (CommonModel.tblShoppingProduct objProduct in objcommonmodel.Products)
            {
                objod = new OrderDetail();
                if(objProduct.ProductId==1)
                {
                    objod.Quantity = objProduct.Quantity;
                    objod.ProductId = objProduct.ProductId;
                    objod.Total = (objProduct.Quantity - (objProduct.Quantity/3)) * objProduct.Price;
                }
                else
                {
                    objod.Quantity = objProduct.Quantity;
                    objod.ProductId = objProduct.ProductId;
                    objod.Total = objProduct.Quantity * objProduct.Price;
                }
                objsumm.OrderTotal += objod.Total;
                orderDetails.Add(objod);
            }
            CouponMaster objcm = new CouponMaster();
            objsumm.OrderNumber = Guid.NewGuid().ToString();
            objsumm.CouponId = objcommonmodel.CouponId;
            objsumm.UserId = objcommonmodel.UserId;
            //db.CouponMasters.First(c => c.CouponId == objcommonmodel.CouponId);
            CouponMaster cm =
                (from CouponMaster in db.CouponMasters
                 where CouponMaster.CouponId == objsumm.CouponId
                 select CouponMaster).FirstOrDefault();
            if (objcommonmodel.CouponCode == cm.CouponCode)
            {
                //objsumm.OrderTotal = objod.Sum(item => item.Total);
                decimal? percentoff = objsumm.OrderTotal * cm.Percentage / 100;
                objsumm.OrderTotal = objsumm.OrderTotal - percentoff;
            }
            else
            {
                objsumm.OrderTotal = orderDetails.Sum(item => item.Total);
            }
            db.OrderSummaries.Add(objsumm);
            await db.SaveChangesAsync();
            foreach (OrderDetail objorderdetail in orderDetails)
            {
                objorderdetail.OrderId = objsumm.OrderId;

                db.OrderDetails.Add(objorderdetail);
            }
            await db.SaveChangesAsync();
            return CreatedAtRoute("DefaultApi", new { id = objsumm.OrderId }, objsumm);
        }


        [ResponseType(typeof(OrderSummary))]
        [Route("api/DeleteOrders")]
        public  IHttpActionResult DeleteOrderSummary(int id)
        {
            var Order = db.OrderSummaries.Where(x => x.OrderId == id);
            var OrderDetail = db.OrderDetails.Where(x => x.OrderId == id);
            if (Order == null)
            {
                return NotFound();
            }
            db.OrderSummaries.RemoveRange(Order);
            db.OrderDetails.RemoveRange(OrderDetail);
            db.SaveChanges();
            return Ok(id);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrderDetailExists(int id)
        {
            return db.OrderDetails.Count(e => e.OrderDetailId == id) > 0;
        }
    }
}