using Dokana.DTOs;
using Dokana.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Dokana.Controllers.API
{
    [Authorize]
    public class FavoritesController : ApiController
    {
        private ApplicationDbContext _context = new ApplicationDbContext();

        [HttpPost]
        public IHttpActionResult AddToMyFavorite(FavoriteDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var currentUserId = User.Identity.GetUserId();
            var itemInDb = _context.Favorites
                .SingleOrDefault(i => i.ProductId == dto.ProductId && i.UserId == currentUserId);

            if (itemInDb != null)
                return BadRequest();

            Favorite item = new Favorite()
            {
                ProductId = dto.ProductId,
                UserId = currentUserId
            };

            _context.Favorites.Add(item);
            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult DeleteFromFavorite(int id)
        {
            var productInDb = _context.Products.Find(id);
            if (productInDb == null)
                return BadRequest();

            var currentUserId = User.Identity.GetUserId();
            var dto = _context.Favorites.Single(f => f.ProductId == id && f.UserId == currentUserId);
            if (dto == null)
                return NotFound();

            _context.Favorites.Remove(dto);
            _context.SaveChanges();
            return Ok();
        }
    }
}
