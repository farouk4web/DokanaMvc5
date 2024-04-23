using Dokana.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dokana.Controllers
{
    public class ShoppingCartController : Controller
    {
        private ApplicationDbContext _context = new ApplicationDbContext();

        // GET: ShoppingCart
        public ActionResult Index(bool showMsg = false)
        {
            var currentUserId = User.Identity.GetUserId();

            var userCartItems = _context.CartItems
                                    .Include(i => i.Product)
                                    .Where(m => m.UserId == currentUserId && m.OrderId == null)
                                    .OrderByDescending(c => c.Id).ToList();

            decimal totalPrice = 0;
            foreach (var item in userCartItems)
            {
                totalPrice += item.Product.Price * item.Amount;
            }
            ViewBag.totalPrice = totalPrice;

            ViewBag.showMsg = showMsg;

            return View(userCartItems);
        }

    }
}