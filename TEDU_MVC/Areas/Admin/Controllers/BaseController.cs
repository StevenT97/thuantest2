using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using TEDU_MVC.Areas.Admin.Code;
using TEDU_MVC.Code;

namespace TEDU_MVC.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        // GET: Admin/Base
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var sess = (UserSession)Session[CommonConstant.USER_SESSION];

            if (sess == null)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(new { controller = "Login", action="Index", Area="Admin"})
                    );
            }
            base.OnActionExecuting(filterContext);
        }
    }
}