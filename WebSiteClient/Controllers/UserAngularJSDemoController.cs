using Domain.Interfaces;
using Domain.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebSiteClient.Controllers
{
    public class UserAngularJSDemoController : Controller
    {
        public readonly IDataRepository _Repository;
        public UserAngularJSDemoController()
        {
            _Repository = new DataRepository();
        }
        // GET: UserAngularJSDemo
        public ActionResult Index()
        {
            return View();
        }
    }
}