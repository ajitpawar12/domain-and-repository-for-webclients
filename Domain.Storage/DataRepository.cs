using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;
using Domain.Storage.Context;

namespace Domain.Storage
{
    //Implementation of IDataRepository Interface
    public class DataRepository:IDataRepository
    {
        private readonly DataContext _context;
        public DataRepository()
        {
            _context=new DataContext();
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void UpdateUser(int id, User user)
        {
            var userdetails = _context.Users.SingleOrDefault(x=>x.UserId==id);
            if (userdetails != null)
            {
                userdetails.FirstName=user.FirstName;
                userdetails.LastName = user.LastName;
                userdetails.Email = user.Email;
            }
            _context.SaveChanges();

        }

        public void DeleteUser(int id)
        {
            var userdetails = _context.Users.SingleOrDefault(x => x.UserId == id);

            if (userdetails != null) _context.Users.Remove(userdetails);
            _context.SaveChanges();
        }

        public User UserDetails(int id)
        {
            return _context.Users.SingleOrDefault(x => x.UserId == id);
        }

        public List<User> AllUsers()
        {
            return _context.Users.ToList();
        }

        public void AddProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void UpdateProduct(int id, Product product)
        {
            var pDetails = _context.Products.SingleOrDefault(x => x.ProductId == id);
            if (pDetails != null)
            {
                pDetails.Name = product.Name;
                pDetails.Description = product.Description;
                pDetails.Quantity = product.Quantity;
                pDetails.Price = product.Price;
                pDetails.CategoryId = product.CategoryId;
                pDetails.SubCategoryId = product.SubCategoryId;
                pDetails.ModifyDate = DateTime.Now;
                _context.SaveChanges();
            }
        }

        public void DeleteProduct(int id)
        {
            var pDetails = _context.Products.SingleOrDefault(x => x.ProductId == id);
            if (pDetails != null)
                _context.Products.Remove(pDetails);
            _context.SaveChanges();
        }

        public Product ProductDetails(int id)
        {
            return _context.Products.SingleOrDefault(x => x.ProductId == id);
        }

        public List<Product> AllProducts()
        {
            return _context.Products.Include("Category").Include("SubCategory").ToList();
        }

        public List<Category> AllCategories()
        {
            return _context.Categories.ToList();
        }

        public List<SubCategory> AllSubcateCategories()
        {
            return _context.SubCategories.ToList();
        }

        public List<SubCategory> GetSubCategoriesByCategoryId(int? id)
        {
            return _context.SubCategories.Where(x => x.CategoryId == id).ToList();
        }
    }
}
