using Dokana.AppMethods;
using Dokana.Models;
using Dokana.Resources;
using Dokana.ViewModels;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Dokana.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private ApplicationDbContext _context = new ApplicationDbContext();

        // GET: Orders
        public ActionResult Index()
        {
            var currentUserId = User.Identity.GetUserId();
            var userOrders = _context.Orders
                                    .Where(o => o.UserId == currentUserId)
                                    .OrderByDescending(o => o.Id)
                                    .ToList();

            return View(userOrders);
        }

        public ActionResult OrderInDetails(int id)
        {
            var orderInDb = _context.Orders
                .Include(o => o.CartItems.Select(i => i.Product))
                .SingleOrDefault(o => o.Id == id);

            if (orderInDb == null || orderInDb.UserId != User.Identity.GetUserId())
                return HttpNotFound();

            return View(orderInDb);
        }

        public ActionResult New()
        {
            var currentUserId = User.Identity.GetUserId();
            var shippingFee = Convert.ToDecimal(ConfigurationManager.AppSettings["shippingFee"].ToString());
            var userCartItems = _context.CartItems
                                .Include(m => m.Product)
                                .Where(i =>
                                            i.UserId == currentUserId
                                            && i.OrderId == null
                                            && i.Product.AvailableToSale == true
                                            && i.Product.UnitsInStore > 0
                                            );

            if (userCartItems.Count() == 0)
            {
                return RedirectToAction("Index", "ShoppingCart", new { showMsg = true });
            }

            // count total price of all cart items 
            decimal total = 0;
            foreach (var item in userCartItems)
            {
                total += item.Amount * item.Product.Price;
            }

            var viewModel = new OrderFormViewModel()
            {
                Order = new Order()
                {
                    Total = total,
                    ShippingAmount = shippingFee,
                    GrandTotal = total + shippingFee
                },
                CartItems = userCartItems
            };

            return View("OrderForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(OrderFormViewModel vm)
        {
            var shippingFee = Convert.ToDecimal(ConfigurationManager.AppSettings["shippingFee"].ToString());
            var currentUserId = User.Identity.GetUserId();

            var userCartItems = _context.CartItems
                                .Include(m => m.Product)
                                .Where(i =>
                                            i.UserId == currentUserId
                                            && i.OrderId == null
                                            && i.Product.AvailableToSale == true
                                            && i.Product.UnitsInStore > 0
                                            );
            if (userCartItems.Count() == 0)
                return RedirectToAction("Index", "ShoppingCart");

            if (!ModelState.IsValid)
            {
                vm.CartItems = userCartItems;

                return View("OrderForm", vm);
            }

            // add new order to database
            decimal total = 0;
            foreach (var item in userCartItems)
            {
                total += item.Amount * item.Product.Price;
            }

            // update for security
            vm.Order.Total = total;
            vm.Order.ShippingAmount = shippingFee;
            vm.Order.GrandTotal = total + shippingFee; // without payment method fee

            // to make sure every thing is working iin a good way
            vm.Order.PaymentMethodId = null;
            vm.Order.IsDone = false;
            vm.Order.IsPaid = false;
            vm.Order.IsConfirmed = false;
            vm.Order.IsShipping = false;
            vm.Order.IsDelivered = false;

            // relate with current user
            vm.Order.UserId = currentUserId;

            // add utc date Time 
            vm.Order.DateOfCreate = DateTime.UtcNow;

            // insert the order to database
            _context.Orders.Add(vm.Order);
            _context.SaveChanges();

            // relate cartitems with this order
            userCartItems.ForEach(item => item.OrderId = vm.Order.Id);
            _context.SaveChanges();

            return RedirectToAction("Checkout", new { id = vm.Order.Id });
        }

        public ActionResult Checkout(int id)
        {
            var orderInDb = _context.Orders.Find(id);
            if (orderInDb == null || orderInDb.PaymentMethodId != null)
                return HttpNotFound();

            var viewModel = new CheckoutViewModel
            {
                OrderId = orderInDb.Id
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Checkout(CheckoutViewModel vm)
        {
            if (vm.PaymentMethodId != MethodsIDs.CashOnDelivaryId)
            {
                if (!ModelState.IsValid)
                    return View(vm);
            }

            var orderInDb = _context.Orders
                                        .Include(o => o.CartItems.Select(c => c.Product))
                                        .SingleOrDefault(o => o.Id == vm.OrderId);

            if (orderInDb == null || orderInDb.PaymentMethodId != null)
                return HttpNotFound();

            // update order payment method  
            // need to create another way 
            // code is not good 😢

            // now pay using choosed payment method
            var operationResult = false;

            if (vm.PaymentMethodId == MethodsIDs.CashOnDelivaryId)
            {
                operationResult = true;
            }
            else if (vm.PaymentMethodId == MethodsIDs.VodafoneCashId)
            {
                operationResult = CheckoutServices
                                        .PayUsingVodafoneCash(vm.VodafoneCash, orderInDb.GrandTotal.Value);
            }
            else if (vm.PaymentMethodId == MethodsIDs.VisaCardId)
            {
                operationResult = CheckoutServices
                                        .PayUsingVisaCard(vm.VisaCard, orderInDb.GrandTotal.Value);
            }
            else if (vm.PaymentMethodId == MethodsIDs.PaypalId)
            {
                operationResult = CheckoutServices
                                        .PayUsingPaypal(vm.Paypal, orderInDb.GrandTotal.Value);
            }
            else
            {
                return View(vm);
            }

            if (operationResult == true)
            {
                // update order and add pyment method fee
                orderInDb.IsPaid = true;

                // when populate api code of payment methods un comment the next line
                //orderInDb.PaymentMethodId = vm.PaymentMethodId; 
                orderInDb.PaymentMethodId = MethodsIDs.CashOnDelivaryId;

                orderInDb.PaymentMethodFee = orderInDb.PaymentMethodId == MethodsIDs.CashOnDelivaryId ? decimal.Parse(ConfigurationManager.AppSettings["cashOnDelivaryFee"].ToString()) : 0;
                orderInDb.GrandTotal += orderInDb.PaymentMethodFee;

                // now cut the amount from every product in unit in store field
                orderInDb.CartItems.ForEach(item => item.Product.UnitsInStore = item.Product.UnitsInStore - item.Amount);
                orderInDb.CartItems.ForEach(item => item.Product.CountOfSale = item.Product.CountOfSale + item.Amount);
                _context.SaveChanges();
            }

            return RedirectToAction("OrderInDetails", new { id = orderInDb.Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CancelOrder(int id, string returnUrl)
        {
            var orderInDB = _context.Orders
                                .Include(o => o.CartItems)
                                .SingleOrDefault(o => o.Id == id);

            if (orderInDB == null || orderInDB.IsPaid == true)
                return HttpNotFound();

            var currentUserId = User.Identity.GetUserId();

            if (orderInDB.UserId == currentUserId || User.IsInRole(RoleName.SuperAdmins) || User.IsInRole(RoleName.Admins))
            {
                _context.CartItems.RemoveRange(orderInDB.CartItems.ToList());
                _context.Orders.Remove(orderInDB);

                _context.SaveChanges();
                if (returnUrl.Contains("ControlPanel"))
                    return RedirectToAction("Orders", "ControlPanel");
                else
                    return RedirectToAction("Index");
            }

            return RedirectToAction("OrderInDetails", new { id = id });
        }
    }
}

