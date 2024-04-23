using Dokana.AppMethods;
using Dokana.Models;
using Dokana.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Dokana.Controllers
{
    [Authorize(Roles = RoleName.SuperAdminsAndAdminsAndSellers)]
    public class UsersController : Controller
    {
        private readonly int elementInPage = 12;
        private ApplicationDbContext _context = new ApplicationDbContext();
        private UserManager<ApplicationUser> _userManager;
        public UsersController()
        {
            _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_context));
        }


        public ActionResult Index(string getOnly, int pageNumber = 1)
        {
            var groupOfUsers = new List<ApplicationUser>();

            switch (getOnly)
            {
                case RoleName.SuperAdmins:
                    groupOfUsers = GetUsersInRole(RoleName.SuperAdmins);
                    break;

                case RoleName.Admins:
                    groupOfUsers = GetUsersInRole(RoleName.Admins);
                    break;

                case RoleName.Sellers:
                    groupOfUsers = GetUsersInRole(RoleName.Sellers);
                    break;

                case RoleName.ShippingStaff:
                    groupOfUsers = GetUsersInRole(RoleName.ShippingStaff);
                    break;

                default:
                    groupOfUsers = _context.Users.ToList();
                    break;
            }

            var viewModel = new CPanelUsersViewModel
            {
                // create pagenation
                Users = groupOfUsers
                                .OrderBy(u => u.SignDate)
                                .Skip((pageNumber - 1) * elementInPage)
                                .Take(elementInPage)
                                .ToList(),

                CurrentPageNumber = pageNumber,
                CountOfUsers = groupOfUsers.Count()
            };

            return View(viewModel);
        }

        public ActionResult UserAccount(string id)
        {
            var userInDb = _context.Users
                                        .Include(u => u.Orders)
                                        .Include(u => u.Favorites.Select(f => f.Product))
                                        .Include(u => u.CartItems.Select(i => i.Product))
                                        .SingleOrDefault(u => u.Id == id);

            if (userInDb == null)
                return HttpNotFound();

            var viewModel = new UserAccountViewModel
            {
                User = userInDb,
                IsSuperAdmin = _userManager.IsInRole(userInDb.Id, RoleName.SuperAdmins),
                IsAdmin = _userManager.IsInRole(userInDb.Id, RoleName.Admins),
                IsSeller = _userManager.IsInRole(userInDb.Id, RoleName.Sellers),
                IsShippingStaff = _userManager.IsInRole(userInDb.Id, RoleName.ShippingStaff),
            };



            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.SuperAdminsAndAdmins)]
        public ActionResult AddUserToRole(string id, string roleName)
        {
            var userInDb = _context.Users.Find(id);
            if (userInDb != null && roleName != null)
            {
                if (roleName != RoleName.SuperAdmins)
                {
                    _userManager.AddToRole(id, roleName);
                }
                else if (roleName == RoleName.SuperAdmins && User.IsInRole(RoleName.SuperAdmins))
                {
                    _userManager.AddToRole(id, roleName);
                }
            }

            return RedirectToAction("UserAccount", new { id = id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.SuperAdminsAndAdmins)]
        public ActionResult RemoveUserFromRole(string id, string roleName)
        {
            var userInDb = _context.Users.Find(id);
            if (userInDb != null && roleName != null)
            {
                if (roleName != RoleName.SuperAdmins)
                {
                    _userManager.RemoveFromRole(id, roleName);
                }
                else if (roleName == RoleName.SuperAdmins && User.IsInRole(RoleName.SuperAdmins))
                {
                    _userManager.RemoveFromRole(id, roleName);
                }
            }
            return RedirectToAction("UserAccount", new { id = id });
        }


        //public ActionResult CreateRole()
        //{
        //    IdentityRole role = new IdentityRole
        //    {
        //        Name = RoleName.ShippingStaff
        //    };

        //    _context.Roles.Add(role);
        //    _context.SaveChanges();

        //    return RedirectToAction("Index");
        //}

        public List<ApplicationUser> GetUsersInRole(string roleName)
        {
            var role = _context.Roles
                                .Include(r => r.Users)
                                .SingleOrDefault(r => r.Name == roleName);

            var usersInRole = new List<ApplicationUser>();

            if (role != null)
            {
                foreach (var userRole in role.Users)
                {
                    var userInDb = _context.Users.Find(userRole.UserId);
                    usersInRole.Add(userInDb);
                }
            }

            return usersInRole;
        }

    }
}


