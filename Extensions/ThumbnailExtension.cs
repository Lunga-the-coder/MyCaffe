using MyCaffe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyCaffe.Extensions
{
    public static class ThumbnailExtension
    {
        public static IEnumerable<ThumbnailModel> GetItemThumbnail(this List<ThumbnailModel> thumbnails, ApplicationDbContext db = null)
        {
            try
            {
                if (db == null) db = ApplicationDbContext.Create();

                thumbnails = (from b in db.FoodItems
                              select new ThumbnailModel
                              {
                                  FoodItemId = b.FoodItemId,
                                  Name = b.Name,
                                  ImageUrl = "/Uploads/" + b.ImageUrl,
                                  Price = b.Price
                              }).ToList();
            }
            catch (Exception ex)
            {

            }
            return thumbnails.OrderBy(t => t.Name);

        }
    }
}