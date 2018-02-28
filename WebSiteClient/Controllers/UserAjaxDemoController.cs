using Domain.Interfaces;
using Domain.Models;
using Domain.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebSiteClient.Controllers
{
    public class UserAjaxDemoController : Controller
    {
        public readonly IDataRepository _Repository;
        public UserAjaxDemoController()
        {
            _Repository = new DataRepository();

        }
        // GET: UserAjax
        public ActionResult Index()
        {
            var allauser = _Repository.AllUsers();
            return View(allauser);
        }
        [HttpPost]
        public JsonResult Create(User user)
        {
            _Repository.AddUser(user);
            return Json(new {status="Success"},JsonRequestBehavior.AllowGet);
        }
    }
}