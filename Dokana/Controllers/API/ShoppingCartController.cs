using Dokana.DTOs;
using Dokana.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;

namespace Dokana.Controllers.API
{
    [Authorize]
    public class ShoppingCartController : ApiController
    {
        private ApplicationDbContext _context = new ApplicationDbContext();

        // ADD new item to the shopping cart
        [HttpPost]
        public IHttpActionResult AddNewItem(CartItemDto dto)
        {
            var currentUserId = User.Identity.GetUserId();
            var productInDb = _context.Products.Find(dto.ProductId);

            if (productInDb != null)
            {
                // check if this product are available to sale or not
                if (productInDb.AvailableToSale == false || productInDb.UnitsInStore < 1)
                    return BadRequest();

                // check if this product added before, or not
                var itemInDb = _context.CartItems
                    .SingleOrDefault(
                                        c => c.ProductId == productInDb.Id &&
                                        c.UserId == currentUserId &&
                                        c.OrderId == null);

                if (itemInDb != null)
                    return BadRequest();

                // create cart item and push it to DataBase
                CartItem item = new CartItem()
                {
                    Amount = 1,
                    ProductId = productInDb.Id,
                    UserId = currentUserId
                };

                _context.CartItems.Add(item);
                _context.SaveChanges();


                // populate dto by item data then send it
                dto.Id = item.Id;
                dto.Amount = item.Amount;
                dto.Name = productInDb.Name;
                dto.Price = productInDb.Price;
                dto.ImgSrc = productInDb.ImageSrc;

                return Ok(dto);
            }

            return BadRequest();
        }


        // CHANGE AMOUNT of cart item 
        [HttpPut]
        public IHttpActionResult ChangeAmount(int id, CartItemDto dto)
        {
            var itemInDb = _context.CartItems
                                    .Include(i => i.Product)
                                    .SingleOrDefault(i => i.Id == id);

            if (itemInDb == null)
                return NotFound();

            else if (itemInDb != null)
            {
                if (dto.Amount <= 0)
                {
                    _context.CartItems.Remove(itemInDb);
                    _context.SaveChanges();
                    return Ok(0);
                }
                else
                {
                    var unitsInStore = itemInDb.Product.UnitsInStore;
                    if (dto.Amount <= unitsInStore)
                    {
                        itemInDb.Amount = dto.Amount;
                        _context.SaveChanges();
                        return Ok(itemInDb.Amount);
                    }
                    return BadRequest();
                }
            }

            return BadRequest();
        }


        // REMOVE all items in user shopping cart
        [HttpDelete]
        public IHttpActionResult RemoveAll(int id)
        {
            var currentUserId = User.Identity.GetUserId();

            var userCartItems = _context.CartItems
                                            .Where(i => i.UserId == currentUserId && i.OrderId == null);

            _context.CartItems.RemoveRange(userCartItems);
            _context.SaveChanges();

            return Ok();
        }
    }

}
