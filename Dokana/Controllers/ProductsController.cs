using Dokana.Models;
using Dokana.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dokana.Resources;
using CloudinaryDotNet;
using System.Configuration;
using CloudinaryDotNet.Actions;
using Dokana.AppMethods;

namespace Dokana.Controllers
{
    [Authorize(Roles = RoleName.SuperAdminsAndAdminsAndSellers)]
    public class ProductsController : Controller
    {
        private ApplicationDbContext _context = new ApplicationDbContext();

        // Product in details
        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            var productInDb = _context.Products
                                        .Include(p => p.Reviews.Select(r => r.User))
                                        .Include(p => p.Category)
                                        .Include(p => p.User)
                                        .SingleOrDefault(p => p.Id == id);

            if (productInDb == null)
                return HttpNotFound();

            //get product stars reviews
            double rateStars = 0;

            if (productInDb.Reviews.Count() != 0)
                rateStars = productInDb.Reviews.Average(r => r.StarsCount);


            // get the last 6 products published by current product`s seller
            var anotherProducts = _context.Products
                                        .Where(p => p.Id != productInDb.Id)
                                        .OrderByDescending(p => p.UnitsInStore)
                                        .Take(6)
                                        .ToList();

            // get last 6 products was populated in same category
            var productsFromSameCategory = _context.Products
                                        .Where(p => p.CategoryId == productInDb.CategoryId && p.Id != productInDb.Id)
                                        .OrderByDescending(p => p.Id)
                                        .Take(6)
                                        .ToList();

            var viewModel = new ProductDetailsViewModel
            {
                Product = productInDb,
                RateInStars = rateStars,
                AnotherProducts = anotherProducts,
                ProductsFromSameCategory = productsFromSameCategory
            };


            // check if this product is in user favorites and in shoppincart or not
            var isFavoriteItem = false;
            var isInCart = false;

            if (User.Identity.IsAuthenticated)
            {
                var currentUserId = User.Identity.GetUserId();

                // check if this product is in user favorites
                var favoriteItem = _context.Favorites.SingleOrDefault(m => m.ProductId == id && m.UserId == currentUserId);

                if (favoriteItem != null)
                    isFavoriteItem = true;

                // check if this product is in user shopping cart
                var cartItem = _context.CartItems
                                .SingleOrDefault(
                                    c => c.ProductId == productInDb.Id &&
                                    c.UserId == currentUserId &&
                                    c.OrderId == null);

                if (cartItem != null)
                    isInCart = true;
            }

            ViewBag.isInCart = isInCart;
            ViewBag.isFavoriteItem = isFavoriteItem;

            return View(viewModel);
        }

        public ActionResult New()
        {
            var viewModel = new ProductFormViewModel
            {
                Product = new Product(),
                Categories = _context.Categories.ToList()
            };

            return View("ProductForm", viewModel);
        }

        public ActionResult Update(int id)
        {
            var productInDb = _context.Products.Find(id);

            if (productInDb == null)
                return HttpNotFound();

            if (User.IsInRole(RoleName.SuperAdmins) || User.IsInRole(RoleName.Admins) || productInDb.UserId == User.Identity.GetUserId())
            {
                var viewModel = new ProductFormViewModel
                {
                    Product = productInDb,
                    Categories = _context.Categories.ToList()
                };

                return View("ProductForm", viewModel);
            }

            return RedirectToAction("Details", new { id = id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(ProductFormViewModel vm, HttpPostedFileBase productPicture)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new ProductFormViewModel
                {
                    Product = vm.Product,
                    Categories = _context.Categories.ToList()
                };

                return View("ProductForm", viewModel);
            }


            if (vm.Product.Id == 0) // True new product
            {
                // upload photo
                if (productPicture == null)
                {
                    var viewModel = new ProductFormViewModel
                    {
                        Product = vm.Product,
                        Categories = _context.Categories.ToList()
                    };

                    ModelState.AddModelError("", ResourceKeys.chooseProductImageErrMsg);
                    return View("ProductForm", viewModel);
                }

                var result = ProjectMethods.IsSupportedFile(productPicture);
                if (result == false)
                {
                    var viewModel = new ProductFormViewModel
                    {
                        Product = vm.Product,
                        Categories = _context.Categories.ToList()
                    };

                    ModelState.AddModelError("", ResourceKeys.chooseAnotherImageErrMsg);
                    return View("ProductForm", viewModel);
                }

                vm.Product.CountOfSale = 0;
                vm.Product.UserId = User.Identity.GetUserId();
                _context.Products.Add(vm.Product);
                _context.SaveChanges();

                // upload the photo
                vm.Product.ImageSrc = ProjectMethods.UploadPohtoToServer(productPicture, "kushk/products/" + vm.Product.Id, 400);
                _context.SaveChanges();
            }

            else // id  !=0 this is mean it`s Exist Product
            {
                var productInDb = _context.Products.Find(vm.Product.Id);
                if (productInDb == null)
                    return HttpNotFound();

                if (User.IsInRole(RoleName.SuperAdmins) || User.IsInRole(RoleName.Admins) ||
                    User.IsInRole(RoleName.Sellers) && productInDb.UserId == User.Identity.GetUserId())
                {
                    // update product
                    productInDb.Name = vm.Product.Name;
                    productInDb.Description = vm.Product.Description;
                    productInDb.CategoryId = vm.Product.CategoryId;
                    productInDb.UnitsInStore = vm.Product.UnitsInStore;
                    productInDb.Price = vm.Product.Price;
                    productInDb.AvailableToSale = vm.Product.AvailableToSale;
                    productInDb.ShowInSlider = vm.Product.ShowInSlider;

                    // if user upload new photo update it
                    if (productPicture != null)
                    {
                        // check if image is png, jpg, jpeg
                        var result = ProjectMethods.IsSupportedFile(productPicture);
                        if (result == false)
                        {
                            var viewModel = new ProductFormViewModel
                            {
                                Product = vm.Product,
                                Categories = _context.Categories.ToList()
                            };

                            ModelState.AddModelError("", ResourceKeys.chooseAnotherImageErrMsg);
                            return View("ProductForm", viewModel);
                        }

                        // upload the photo
                        productInDb.ImageSrc = ProjectMethods.UploadPohtoToServer(productPicture, "kushk/products/" + productInDb.Id, 400);
                        _context.SaveChanges();
                    }
                }

            }

            _context.SaveChanges();

            return RedirectToAction("Details", new { id = vm.Product.Id });
        }

        [AllowAnonymous]
        public ActionResult Reviews(int id)
        {
            var productInDb = _context.Products
                                            .Include(p => p.Reviews)
                                            .Include(p => p.Category)
                                            .Include(p => p.User)
                                            .SingleOrDefault(p => p.Id == id);
            if (productInDb == null)
                return HttpNotFound();

            var moreProducts = _context.Products
                                        .Where(p => p.CategoryId == productInDb.CategoryId && p.Id != productInDb.Id)
                                        .OrderByDescending(p => p.Reviews.Count()).Take(4);

            var viewModel = new ReviewsViewModel
            {
                Product = productInDb,
                AnotherProducts = moreProducts
            };

            return View(viewModel);
        }
    }
}