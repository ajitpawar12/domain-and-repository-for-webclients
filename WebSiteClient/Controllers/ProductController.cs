using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Models;
using Domain.Storage.Context;
using WebSiteClient.ViewModels.Product;

namespace WebSiteClient.Controllers
{
    public class ProductController : Controller
    {
        DataContext context=new DataContext();
        // GET: Product
        public ActionResult Index()
        {
            var productList = context.Products.Include("Category").Include( "SubCategory").ToList();
            return View(productList);
        }
        [HttpGet]
        public ActionResult Create()
        {
            var vm=new CreateProductVM();
            //Filling Required DropdownList's
            Populatelookups(vm);
            return View(vm);
        }
        [HttpPost]
        public ActionResult Create(CreateProductVM vm)
        {
            if (!ModelState.IsValid)
            {
                //Filling Required DropdownList's
                Populatelookups(vm);
                return View(vm);
            }
            
            var product = new Product
            {
                Name = vm.Name,
                AddDate = DateTime.Now,
                Description = vm.Description,
                Price = vm.Price,
                Quantity = vm.Quantity,
                CategoryId = vm.CategoryId,
                SubCategoryId = vm.SubCategoryId,
            };
            context.Products.Add(product);
            context.SaveChanges();
            return RedirectToAction("Index","Product");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var pDetails = context.Products.SingleOrDefault(x => x.ProductId == id);
            var vm = new EditProductVM();
            //Filling Required DropdownList's
            Populatelookups(vm);
            //This is Subcategory list form  pDetails which is got from pDetails
            var sList = context.SubCategories.Where(x => x.CategoryId == pDetails.CategoryId).Select(x=>new SelectListItem
            {
                Text = x.Name,
                Value = x.SubCategoryId.ToString()
            }).ToArray();
            vm.SubcategoryList = sList;
            if (pDetails != null)
            {
                vm.ProductId = pDetails.ProductId;
                vm.Name = pDetails.Name;
                vm.Description = pDetails.Description;
                vm.Quantity = pDetails.Quantity;
                vm.Price = pDetails.Price;
                vm.CategoryId = pDetails.CategoryId;
                vm.SubCategoryId = pDetails.SubCategoryId;
            }
            return View(vm);
        }
        [HttpPost]
        public ActionResult Edit(EditProductVM vm)
        {
            if (!ModelState.IsValid)
            {
                //Filling Required DropdownList's
                Populatelookups(vm);
                return View(vm);
            }
            var pDetails = context.Products.SingleOrDefault(x => x.ProductId == vm.ProductId);
            if (pDetails != null)
            {
               pDetails.Name = vm.Name;
                pDetails.Description= vm.Description;
                pDetails.Quantity= vm.Quantity;
                pDetails.Price= vm.Price;
                pDetails.CategoryId= vm.CategoryId;
                pDetails.SubCategoryId= vm.SubCategoryId;
                pDetails.ModifyDate=DateTime.Now;
                context.SaveChanges();
                return RedirectToAction("Index", "Product");
            }
            else
            {
                Populatelookups(vm);
                return View(vm);
            }
            
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var pDetails = context.Products.SingleOrDefault(x => x.ProductId == id);
            if (pDetails != null)
                context.Products.Remove(pDetails);
            context.SaveChanges();
            return RedirectToAction("Index", "Product");
        }

        public JsonResult GetSubcategories(int? cId)
        {
            var cList = context.SubCategories.Where(x=>x.CategoryId==cId).Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.SubCategoryId.ToString()
            }).ToArray();
            return Json(new { status = "Success",clist= cList }, JsonRequestBehavior.AllowGet);
        }

        public void Populatelookups(ProductBaseModel model)
        {
            //Fill CategoryList
            var cList = context.Categories.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.CategoryId.ToString()
            }).ToArray();
            //Fill SubCategoryList
            var scList = context.SubCategories.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.SubCategoryId.ToString()
            }).ToArray();

            model.CategoryList = cList;
            model.SubcategoryList = scList;
        }
    }
}