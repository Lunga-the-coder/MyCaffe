using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyCaffe.Models
{
    public class DeliveryDetail
    {
        [Key]
        public int DeliveryDetailId { get; set; }

        //Foreign Key
        public virtual Order Order { get; set; }
        public int OrderId { get; set; }

        [Display(Name = "Delivery Campus")]
        public Campus DeliveryCampus { get; set; }

        [Display(Name = "Delivery Price")]
        [DisplayFormat(DataFormatString = "{0:R0.00}")]
        public double DeliveryPrice { get; set; }

        [Display(Name = "Block Code")]
        [Required(ErrorMessage = "Please Enter the block code!")]
        public string DeliveryBlock { get; set; }

        [Display(Name = "Delivery Instructions")]
        public string DeliveryInstructions { get; set; }

        public double CalcDeliveryPrice()
        {
            double price = 0;

            if (DeliveryCampus == Campus.RitsonCampus)
            {
                price = 15;
            }
            else if (DeliveryCampus == Campus.SteveBikoCampus)
            {
                price = 20;
            }
            else
            {
                price = 25;
            }

            return price;
        }

    }

    public enum Campus
    {
        [Display(Name = "Ritson Campus")]
        RitsonCampus,
        [Display(Name = "Steve Biko Campus")]
        SteveBikoCampus,
        [Display(Name = "ML Sultan Campus")]
        MlSultanCampus
    }
}