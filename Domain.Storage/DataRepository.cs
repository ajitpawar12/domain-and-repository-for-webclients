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
            var userdetails = _context.Users.Single(x=>x.UserId==id);
            userdetails.FirstName=user.FirstName;
            userdetails.LastName = user.LastName;
            userdetails.Email = user.Email;
            _context.SaveChanges();

        }

        public void DeleteUser(int id)
        {
            var userdetails = _context.Users.Single(x => x.UserId == id);

            _context.Users.Remove(userdetails);
            _context.SaveChanges();
        }

        public User UserDetails(int id)
        {
            return _context.Users.Single(x => x.UserId == id);
        }

        public List<User> AllUsers()
        {
            return _context.Users.ToList();
        }
    }
}
