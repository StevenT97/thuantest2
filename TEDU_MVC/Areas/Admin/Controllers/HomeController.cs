using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TEDU_MVC.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Admin/Home

        // khong can dang nhap van vao dc
        // [AllowAnonymous]
        // chi dang nhap moi vao dc
       // [Authorize]
        public ActionResult Index()
        {
            return View();
        }
    }
}