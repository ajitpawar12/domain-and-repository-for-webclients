using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebSiteClient.ViewModels.Product
{
    //extends basemodel for Common things
    public class CreateProductVM:ProductBaseModel
    {
        public CreateProductVM()
        {
            
        }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public double Price { get; set; }
        //[Required]
        public DateTime? AddDate { get; set; }
       // [Required]
        public DateTime? ModifyDate { get; set; }
        public int? CategoryId { get; set; }
        public int? SubCategoryId { get; set; }

    }
}