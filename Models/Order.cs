using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyCaffe.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        [DisplayFormat(DataFormatString = "{0:R0.00}")]
        public double TotalPrice { get; set; }
        public string UserId { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [DataType(DataType.Time)]
        public DateTime Time { get; set; }

        [DataType(DataType.Time)]
        public DateTime EstimatedDelivery { get; set; }

        [DataType(DataType.Time)]
        public DateTime? ActualDelivery { get; set; }

        public OrderStatus Status { get; set; }

        public enum OrderStatus
        {
            [Display(Name = "Order Placed")]
            OrderPlaced,
            [Display(Name = "Preparing")]
            Preparing,
            [Display(Name = "Out for Delivery")]
            OutForDelivery,
            [Display(Name = "Delivered")]
            Delivered
        }
    }
}