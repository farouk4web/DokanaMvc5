using Dokana.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using Dokana.Resources;

namespace Dokana.Controllers.API
{
    public class OrdersController : ApiController
    {
        private ApplicationDbContext _context = new ApplicationDbContext();


        [HttpPost]
        [Route("Api/Orders/ConfirmOrder/{id}")]
        [Authorize(Roles = RoleName.SuperAdminsAndAdminsAndSellers)]
        public IHttpActionResult ConfirmOrder(int id)
        {
            var orderInDb = _context.Orders.Find(id);
            if (orderInDb == null)
                return NotFound();

            if (orderInDb.IsPaid == true)
            {
                orderInDb.IsConfirmed = true;
                _context.SaveChanges();

                return Ok();
            }

            return BadRequest();
        }

        [HttpPost]
        [Route("Api/Orders/ShippingOrder/{id}")]
        [Authorize(Roles = RoleName.SuperAdminsAndAdminsAndShippingStaff)]
        public IHttpActionResult ShippingOrder(int id)
        {
            var orderInDb = _context.Orders.Find(id);
            if (orderInDb == null)
                return NotFound();

            if (orderInDb.IsPaid == true && orderInDb.IsConfirmed == true)
            {
                orderInDb.IsShipping = true;
                _context.SaveChanges();
                return Ok();
            }

            return BadRequest();
        }

        [HttpPost]
        [Route("Api/Orders/DeliveredOrder/{id}")]
        [Authorize(Roles = RoleName.SuperAdminsAndAdminsAndShippingStaff)]
        public IHttpActionResult DeliveredOrder(int id)
        {
            var orderInDb = _context.Orders.Find(id);
            if (orderInDb == null)
                return NotFound();

            if (orderInDb.IsConfirmed == true && orderInDb.IsShipping == true)
            {
                orderInDb.IsDelivered = true;
                orderInDb.IsDone = true;
                orderInDb.DateOfDone = DateTime.UtcNow;

                _context.SaveChanges();
                return Ok();
            }

            return BadRequest();
        }
    }
}
