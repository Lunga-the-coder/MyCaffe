using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyCaffe.Models
{
    public class FoodItem
    {
        [Key]
        public int FoodItemId { get; set; }

        [Required(ErrorMessage = "Please enter Food Item Name")]
        [DisplayName("Food Item")]
        public string Name { get; set; }

        //Foreign Key
        public virtual Category Category { get; set; }
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Please enter Food Price")]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        public double Price { get; set; }

        [Required(ErrorMessage = "Please enter Food Quantity")]
        public int Quantity { get; set; }

        [Display(Name = "Image")]
        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }
    }
}