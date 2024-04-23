using Dokana.Models;
using Dokana.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Configuration;
using System.Web.Configuration;
using Dokana.Resources;
using Microsoft.AspNet.Identity;
using System.IO;

namespace Dokana.Controllers
{
    [Authorize(Roles = RoleName.SuperAdminsAndAdminsAndSellersAndShippingStaff)]
    public class ControlPanelController : Controller
    {
        private ApplicationDbContext _context = new ApplicationDbContext();
        private readonly int elementInPage = 12;

        // statistics about the site
        public ActionResult Index()
        {
            var topSellingProducts = _context.Products.OrderByDescending(p => p.CountOfSale).Take(3);
            var lastSignedUsers = _context.Users.OrderByDescending(u => u.SignDate).Take(4);

            var viewModel = new DashboardViewModel
            {
                ProductsCount = _context.Products.Count(),
                OrdersCount = _context.Orders.Count(),
                ReviewsCount = _context.Reviews.Count(),
                UsersCount = _context.Users.Count(),
                LastUsers = lastSignedUsers,
                TopSellingProducts = topSellingProducts
            };

            return View(viewModel);
        }

        public ActionResult Orders(string getOnly, int pageNumber = 1)
        {
            var groupOfOrders = new List<Order>();

            switch (getOnly)
            {
                case "IsNotPaid":
                    groupOfOrders = _context.Orders
                                                .Where(o => o.IsPaid == false)
                                                .ToList();
                    break;

                case "IsPaid":
                    groupOfOrders = _context.Orders
                                                .Where(o => o.IsPaid == true && o.IsConfirmed == false && o.IsDone == false)
                                                .ToList();
                    break;

                case "IsConfirmed":
                    groupOfOrders = _context.Orders
                                                .Where(o => o.IsConfirmed == true && o.IsShipping == false && o.IsDone == false)
                                                .ToList();
                    break;

                case "IsShipping":
                    groupOfOrders = _context.Orders
                                                .Where(o => o.IsShipping == true && o.IsDelivered == false && o.IsDone == false)
                                                .ToList();
                    break;

                case "IsDelivered":
                    groupOfOrders = _context.Orders
                                                .Where(o => o.IsDelivered == true && o.IsDone == true)
                                                .ToList();
                    break;

                default:
                    groupOfOrders = _context.Orders.ToList();
                    break;
            }

            var viewModel = new CPanelOrdersViewModel
            {
                // create pagenation
                Orders = groupOfOrders
                                    .OrderByDescending(o => o.Id)
                                    .Skip((pageNumber - 1) * elementInPage)
                                    .Take(elementInPage)
                                    .ToList(),

                CountOfOrders = groupOfOrders.Count(),
                CurrentPageNumber = pageNumber,
                GetOnly = getOnly
            };

            return View(viewModel);
        }

        public ActionResult OrderInDetails(int id)
        {
            var orderInDb = _context.Orders
                            .Include(o => o.CartItems.Select(i => i.Product))
                            .Include(o => o.User)
                            .SingleOrDefault(o => o.Id == id);

            if (orderInDb == null)
                return HttpNotFound();

            return View(orderInDb);
        }



        [Authorize(Roles = (RoleName.SuperAdminsAndAdmins))]
        public ActionResult SiteSetting()
        {
            var viewModel = new SiteSettingViewModel
            {
                SiteLanguage = ConfigurationManager.AppSettings["defaultLanguage"].ToString(),
                CurrencyInArabic = ConfigurationManager.AppSettings["ar_currency"].ToString(),
                CurrencyInEnglish = ConfigurationManager.AppSettings["en_currency"].ToString(),
                ShippingFee = decimal.Parse(ConfigurationManager.AppSettings["shippingFee"]),
                CashOnDelivaryFee = decimal.Parse(ConfigurationManager.AppSettings["cashOnDelivaryFee"])
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = (RoleName.SuperAdminsAndAdmins))]
        public ActionResult SaveSiteSetting(SiteSettingViewModel vm)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("index");

            Configuration configFlie = WebConfigurationManager.OpenWebConfiguration("~");
            AppSettingsSection objAppSettings = (AppSettingsSection)configFlie.GetSection("appSettings");

            if (objAppSettings != null)
            {
                objAppSettings.Settings["defaultLanguage"].Value = vm.SiteLanguage;

                objAppSettings.Settings["ar_currency"].Value = vm.CurrencyInArabic;
                objAppSettings.Settings["en_currency"].Value = vm.CurrencyInEnglish;

                objAppSettings.Settings["shippingFee"].Value = vm.ShippingFee.ToString();
                objAppSettings.Settings["cashOnDelivaryFee"].Value = vm.CashOnDelivaryFee.ToString();

                objAppSettings.Settings["siteDomain"].Value = "kushak.smart.com";

                configFlie.Save();
            }

            return RedirectToAction("SiteSetting");
        }


    }
}