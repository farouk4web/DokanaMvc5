using Dokana.Models;
using Dokana.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dokana.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context = new ApplicationDbContext();
        private readonly int elementsInPage = 20;

        public ActionResult Index()
        {
            var viewModel = new HomeViewModel
            {
                BestSeller = _context.Products
                                        .OrderByDescending(p => p.CountOfSale)
                                        .Take(6)
                                        .ToList(),

                LastProducts = _context.Products
                                        .OrderByDescending(p => p.Id)
                                        .Take(6)
                                        .ToList(),

                SliderProducts = _context.Products
                                        .Where(p => p.ShowInSlider == true)
                                        .OrderByDescending(p => p.Id)
                                        .ToList()
            };
            return View(viewModel);
        }


        [ChildActionOnly]
        public ActionResult GetNavbar()
        {
            List<CartItem> cartItems = new List<CartItem>();

            if (User.Identity.IsAuthenticated)
            {
                var currentUserId = User.Identity.GetUserId();
                cartItems = _context.CartItems
                                                .Where(m => m.UserId == currentUserId && m.OrderId == null)
                                                .OrderByDescending(c => c.Id)
                                                .Include(p => p.Product)
                                                .ToList();
            }

            var viewModel = new NavbarViewModel
            {
                Categories = _context.Categories.ToList(),
                CartItems = cartItems
            };

            return PartialView("_Navbar", viewModel);
        }


        [HttpGet]
        public ActionResult Search(string q, int pageNumber = 1)
        {
            if (q == null || string.IsNullOrEmpty(q))
                return RedirectToAction("index");

            var products = _context.Products
                                         .Where(p => p.Name.Contains(q))
                                         .OrderBy(p => p.Id);


            var pagedList = products
                                    .Skip((pageNumber - 1) * elementsInPage)
                                    .Take(elementsInPage)
                                    .ToList();

            var viewModel = new SearchResultViewModel
            {
                CountOfResults = products.Count(),
                Products = pagedList,
                CurrentPageNumber = pageNumber,
                Query = q
            };

            return View("SearchResult", viewModel);
        }
    }
}