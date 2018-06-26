using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebSiteClient.CustomFilters
{
    public class ActionLogFilter:ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            filterContext.Controller.ViewBag.GreetMessage = "Hello Foo";
            base.OnResultExecuting(filterContext);
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var controllerName = filterContext.RouteData.Values["controller"];
            var actionName = filterContext.RouteData.Values["action"];
            var message = String.Format("{0} controller:{1} action:{2}", "onactionexecuting", controllerName, actionName);
            Debug.WriteLine(message,"Action Filter Log");
            base.OnActionExecuting(filterContext);
        }
    }
}