using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Interfaces;
using Domain.Models;
using Domain.Storage;
using Domain.Storage.Context;
using Microsoft.Ajax.Utilities;
using WebSiteClient.ViewModels.Product;

namespace WebSiteClient.Controllers
{
    public class ProductController : Controller
    {
        public readonly IDataRepository _repository;
        public ProductController()
        {
            _repository = new DataRepository();
        }
        // GET: Product
        public ActionResult Index()
        {
            var productList = _repository.AllProducts();
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
            _repository.AddProduct(product);
            return RedirectToAction("Index","Product");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var pDetails = _repository.ProductDetails(id);
            var vm = new EditProductVM();
            //Filling Required DropdownList's
            Populatelookups(vm);
            if (pDetails != null)
            {
                vm.ProductId = pDetails.ProductId;
                vm.Name = pDetails.Name;
                vm.Description = pDetails.Description;
                vm.Quantity = pDetails.Quantity;
                vm.Price = pDetails.Price;
                vm.CategoryId = pDetails.CategoryId;
                vm.SubCategoryId = pDetails.SubCategoryId;
                //This is Subcategory list form  pDetails which is got from pDetails
                vm.SubcategoryList = GetSubCategoryListByCategory(pDetails.CategoryId);
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
            var product = new Product
            {
                Name = vm.Name,
                AddDate = vm.AddDate,
                Description = vm.Description,
                Price = vm.Price,
                Quantity = vm.Quantity,
                CategoryId = vm.CategoryId,
                SubCategoryId = vm.SubCategoryId,
                ModifyDate = DateTime.Now
            };

            _repository.UpdateProduct(vm.ProductId, product);
            return RedirectToAction("Index", "Product");


        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            _repository.DeleteProduct(id);
            return RedirectToAction("Index", "Product");
        }

        public JsonResult GetSubcategories(int cId)
        {
            var subCList = _repository.GetSubCategoriesByCategoryId(cId);
            var scList = subCList.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.SubCategoryId.ToString()
            }).ToArray();
            return Json(new { status = "Success",clist= scList }, JsonRequestBehavior.AllowGet);
        }

        public void Populatelookups(ProductBaseModel model)
        {
            //Fill CategoryList
            var categoryList = _repository.AllCategories();
            var cList = categoryList.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.CategoryId.ToString()
            }).ToArray();

            //Fill SubCategoryList
            var subcategoryList = _repository.AllSubcateCategories();
            var scList = subcategoryList.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.SubCategoryId.ToString()
            }).ToArray();

            model.CategoryList = cList;
            model.SubcategoryList = scList;
        }

        public SelectListItem[] GetSubCategoryListByCategory(int? categoryId)
        {
            var subCList = _repository.GetSubCategoriesByCategoryId(categoryId);
            var sList = subCList.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.SubCategoryId.ToString()
            }).ToArray();
            return sList;
        }
    }
}