using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Interfaces;
using Domain.Models;
using Domain.Storage;

namespace WebSiteClient.Controllers
{
    public class UserWebDemoController : Controller
    {
        public readonly IDataRepository _Repository;
        public UserWebDemoController()
        {
            _Repository=new DataRepository();
        }
        // GET: User
        public ActionResult Index()
        {
            var alluser = _Repository.AllUsers();

            return View(alluser);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(User user)
        {
            _Repository.AddUser(user);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var userdetails=_Repository.UserDetails(id);

            return View(userdetails);
        }

        [HttpPost]
        public ActionResult Edit(User user)
        {
            _Repository.UpdateUser(user.UserId,user);

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            _Repository.DeleteUser(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var userDetails=_Repository.UserDetails(id);
            return View(userDetails);
        }

    }
}