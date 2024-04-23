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
    [Authorize(Roles = RoleName.SuperAdminsAndAdminsAndSellers)]
    public class CategoriesController : Controller
    {
        private ApplicationDbContext _context = new ApplicationDbContext();
        private readonly int elementInPage = 20;

        [AllowAnonymous]
        public ActionResult Details(int id, int pageNumber = 1)
        {
            var categoryInDb = _context.Categories
                                            .Include(c => c.Products)
                                            .SingleOrDefault(c => c.Id == id);

            if (categoryInDb == null)
                return HttpNotFound();

            var productsGroup = categoryInDb.Products
                                                .OrderByDescending(m => m.Id)
                                                .Skip((pageNumber - 1) * elementInPage)
                                                .Take(elementInPage)
                                                .ToList();

            var viewModel = new CategoryDetailsViewModel
            {
                Category = categoryInDb,
                Products = productsGroup,
                CurrentPageNumber = pageNumber
            };

            return View("Details", viewModel);
        }

        public ActionResult New()
        {
            return View("CategoryForm", new Category());
        }

        public ActionResult Update(int id)
        {
            var categoryInDb = _context.Categories.Find(id);

            if (categoryInDb == null)
                return HttpNotFound();

            if (User.IsInRole(RoleName.SuperAdmins) || User.IsInRole(RoleName.Admins) || User.IsInRole(RoleName.Sellers) && categoryInDb.UserId == User.Identity.GetUserId())
            {
                return View("CategoryForm", categoryInDb);
            }

            return RedirectToAction("Details", new { id = id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Category category)
        {
            if (!ModelState.IsValid)
                return View("CategoryForm", category);

            if (category.Id == 0) // True new Category
            {
                category.UserId = User.Identity.GetUserId();
                _context.Categories.Add(category);
            }
            else // id  == 1 or more  So its Exist Category
            {
                var categoryInDb = _context.Categories.Find(category.Id);
                if (categoryInDb == null)
                    return HttpNotFound();

                if (User.IsInRole(RoleName.SuperAdmins) || User.IsInRole(RoleName.Admins) || User.IsInRole(RoleName.Sellers) && categoryInDb.UserId == User.Identity.GetUserId())
                    categoryInDb.Name = category.Name;
                    category.UserId = User.Identity.GetUserId();
            }

            _context.SaveChanges();
            return RedirectToAction("Details", new { id = category.Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var categoryInDb = _context.Categories
                                                .Include(c => c.Products)
                                                .SingleOrDefault(c => c.Id == id);

            if (categoryInDb == null)
                return HttpNotFound();

            if (categoryInDb.Products.Count() == 0)
            {
                _context.Categories.Remove(categoryInDb);
                _context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Details", new { id = id });
        }
    }
}