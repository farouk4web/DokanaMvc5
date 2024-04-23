using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Dokana.Models;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Dokana.AppMethods
{
    public class ProjectMethods
    {
        private ApplicationDbContext _context = new ApplicationDbContext();

        // this function is just to check if this product in cureent user Favorites or not
        public bool IsFavoriteProduct(int productId, string currentUserId)
        {
            var favoriteItem = _context.Favorites
                                            .SingleOrDefault(m => m.ProductId == productId && m.UserId == currentUserId);

            if (favoriteItem != null)
                return true;

            return false;
        }


        public static bool IsSupportedFile(HttpPostedFileBase picture)
        {
            FileInfo info = new FileInfo(picture.FileName);
            string fileExtension = info.Extension;

            var supportedTypes = new[] { ".png", ".jpg", ".jpeg" };

            if (supportedTypes.Contains(fileExtension) && picture.ContentLength > 0)
                return true;

            return false;
        }

        public static string UploadPohtoToServer(HttpPostedFileBase picture, string path, int dimensions)
        {
            // upload photo to Cloudinary
            var apiSecret = ConfigurationManager.AppSettings["cloudinaryApiSecret"].ToString();
            var myAccount = new Account { ApiKey = "989919465398526", ApiSecret = apiSecret, Cloud = "files-store" };

            Cloudinary _cloudinary = new Cloudinary(myAccount);
            var pictureInUpload = new ImageUploadParams()
            {
                File = new FileDescription(picture.FileName, picture.InputStream),
                PublicId = path,
                Transformation = new Transformation().Width(dimensions).Height(dimensions)
            };
            var userPictureResult = _cloudinary.Upload(pictureInUpload);

            return userPictureResult.SecureUrl.AbsoluteUri;
        }
    }
}