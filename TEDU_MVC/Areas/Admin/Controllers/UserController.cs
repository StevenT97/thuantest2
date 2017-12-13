using Models;
using Models.FrameWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TEDU_MVC.Code;

namespace TEDU_MVC.Areas.Admin.Controllers
{
    [HasCredential(RoleID = "VIEW_USER")]
    public class UserController : BaseController
    {
        // GET: Admin/User
        DemoPPCRentalEntities db = new DemoPPCRentalEntities();
        public ActionResult Index(int page = 1, int pageSize = 5)
        {

            var propertymodel = new AccountModel();
            var model = propertymodel.ListAllPagingUser(page, pageSize);
            return View(model);
        }
        [HasCredential(RoleID = "ADD_USER")]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [HasCredential(RoleID = "ADD_USER")]
        public ActionResult Create(USER user)
        {

            if (ModelState.IsValid)
            {
                var dao = new AccountModel();
                var encryptedMd5Pas = MaHoa.MD5Hash(user.Password);
                user.Password = encryptedMd5Pas;
                long id = dao.Insert(user);
                if (id > 0)
                {
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm user không thành công");
                }
            }
            return View("Index");




        }
    }
}
