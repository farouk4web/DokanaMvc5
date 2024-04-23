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
    [Authorize]
    public class FavoritesController : Controller
    {
        private ApplicationDbContext _context = new ApplicationDbContext();

        // GET: Favorites
        public ActionResult Index()
        {
            var currentUserId = User.Identity.GetUserId();
            var favorites = _context.Favorites
                                        .Include(f => f.Product)
                                        .Where(f => f.UserId == currentUserId)
                                        .ToList();

            return View(favorites);
        }
    }
}