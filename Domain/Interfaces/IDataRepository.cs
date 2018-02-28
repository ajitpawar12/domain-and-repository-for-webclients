using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Interfaces
{
   public interface IDataRepository:IDisposable
   {
        //User methods
        void AddUser(User user);
        void UpdateUser(int id, User user);
        void DeleteUser(int id);
        User UserDetails(int id);
        List<User> AllUsers();

        //Product Methods
        void AddProduct(Product product);
        void UpdateProduct(int id, Product product);
        void DeleteProduct(int id);
        Product ProductDetails(int id);
        List<Product> AllProducts();

        //Caterogy Methods
        List<Category> AllCategories();

        //Sub Categories
        List<SubCategory> AllSubcateCategories();
        List<SubCategory> GetSubCategoriesByCategoryId(int? id);
    }
}
