using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyCaffe.Models
{
    public class OrderItem
    {
        [Key]
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }

        //Foreign Key
        public FoodItem FoodItem { get; set; }
        public int FoodItemId { get; set; }

        public int Quantity { get; set; }

        [DisplayFormat(DataFormatString = "{0:R0.00}")]
        public double TotalPrice { get; set; }

        public string UserId { get; set; }

        public double CalcItemTotalPrice(double foodItemPrice)
        {
            double itemTotalPrice = 0;

            itemTotalPrice = foodItemPrice * Quantity;

            return itemTotalPrice;
        }
    }
}