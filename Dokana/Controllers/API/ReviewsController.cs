using Dokana.DTOs;
using Dokana.Models;
using Dokana.Resources;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Dokana.Controllers.API
{
    [Authorize]
    public class ReviewsController : ApiController
    {
        private ApplicationDbContext _context = new ApplicationDbContext();

        [HttpPost]
        public IHttpActionResult AddNewReview(RateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var currentUserId = User.Identity.GetUserId();

            var userCartItems = _context.CartItems
                                            .Include(c => c.Order)
                                            .Where(i => i.UserId == currentUserId && i.ProductId == dto.ProductId)
                                            .ToList();

            var cartItemsWithDoneOrders = userCartItems.Where(i => i.OrderId != null && i.Order.IsDone == true).Count();

            if (userCartItems.Count == 0 || cartItemsWithDoneOrders == 0)
                return BadRequest(ResourceKeys.payProductFirstMsg);

            // check if user add review before now or not ------> just one review
            var reviewInDb = _context.Reviews.SingleOrDefault(m =>
                                m.UserId == currentUserId && m.ProductId == dto.ProductId);

            if (reviewInDb != null)
                return BadRequest(ResourceKeys.add2reviewsMsg);

            Review review = new Review()
            {
                ProductId = dto.ProductId,
                Comment = dto.Comment,
                StarsCount = dto.StarsCount,
                DateOfCreate = DateTime.Now,
                UserId = currentUserId
            };

            _context.Reviews.Add(review);
            _context.SaveChanges();

            var currentUser = _context.Users.Find(review.UserId);

            dto.Id = review.Id;
            dto.UserId = review.UserId;
            dto.PublishDate = review.DateOfCreate.ToShortDateString();

            dto.UserFullName = currentUser.FullName;
            dto.UserImageSrc = currentUser.ProfileImageSrc;

            return Ok(dto);
        }

    }
}
