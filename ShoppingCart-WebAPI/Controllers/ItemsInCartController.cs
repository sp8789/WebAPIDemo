using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ShoppingCart_WebAPI.Models;

namespace ShoppingCart_WebAPI.Controllers
{
    public class ItemsInCartController : ApiController
    {
        private DemoDatabaseEntities db = new DemoDatabaseEntities();

        // GET: api/ItemsInCart
        [Route("api/GetCartItems")]
        public IQueryable<CartDetail> GetCartDetails()
        {
            return db.CartDetails;
        }

        // GET: api/ItemsInCart/5
        [ResponseType(typeof(CartDetail))]
        [Route("api/GetCartItem")]
        public IHttpActionResult GetCartDetail(int id)
        {
            CartDetail cartDetail = db.CartDetails.Find(id);
            if (cartDetail == null)
            {
                return NotFound();
            }

            return Ok(cartDetail);
        }

        //// PUT: api/ItemsInCart/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutCartDetail(int id, CartDetail cartDetail)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != cartDetail.CartId)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(cartDetail).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!CartDetailExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        // POST: api/ItemsInCart
        [ResponseType(typeof(CartDetail))]
        //public IHttpActionResult PostCartDetail(CartDetail cartDetail)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    db.CartDetails.Add(cartDetail);
        //    db.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = cartDetail.CartId }, cartDetail);
        //}


        [ResponseType(typeof(CartDetail))]
        [Route("api/PostCartItems")]
        public IHttpActionResult PostCartDetail(CartDetail cartDetail)
        {
              
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            using (var db = new DemoDatabaseEntities())
            {
                db.CartDetails.Add(new CartDetail()
                {
                    CartId=cartDetail.CartId,
                    ItemID=cartDetail.ItemID,
                    UserName=cartDetail.UserName,                 
                    Qty = cartDetail.Qty,
                    TotalAmount = cartDetail.TotalAmount,
                    ItemDetail=cartDetail.ItemDetail
                });
                cartDetail.TotalAmount = cartDetail.ItemDetail.ItemPrice * cartDetail.Qty;
                db.SaveChanges();
            }
            return CreatedAtRoute("DefaultApi", new { id = cartDetail.CartId }, cartDetail);
        }


        // DELETE: api/ItemsInCart/5
        [ResponseType(typeof(CartDetail))]
        [Route("api/DeleteCartItems")]
        public IHttpActionResult DeleteCartDetail(int id)
        {
            CartDetail cartDetail = db.CartDetails.Find(id);
            if (cartDetail == null)
            {
                return NotFound();
            }

            db.CartDetails.Remove(cartDetail);
            db.SaveChanges();

            return Ok(cartDetail);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CartDetailExists(int id)
        {
            return db.CartDetails.Count(e => e.CartId == id) > 0;
        }
    }
}