using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebSiteClient.ViewModels.Product
{
    //Base model for Products common things.
    public class ProductBaseModel
    {
        public SelectListItem[] CategoryList { get; set; }
        public SelectListItem[] SubcategoryList { get; set; }
    }
}