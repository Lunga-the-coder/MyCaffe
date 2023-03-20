using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyCaffe.Models
{
    public class ThumbnailModel
    {
        public int FoodItemId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string ImageUrl { get; set; }
        public string Link { get; set; }
    }
}