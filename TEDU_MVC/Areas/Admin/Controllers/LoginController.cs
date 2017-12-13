using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models.FrameWork;
using TEDU_MVC.Areas.Admin.Code;
using System.Web.Security;
using TEDU_MVC.Code;
using Models;
using System.IO;

namespace TEDU_MVC.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        List<SelectListItem> propertytype;
        DemoPPCRentalEntities model = new DemoPPCRentalEntities();
        // GET: Admin/Login
        DemoPPCRentalEntities db = new DemoPPCRentalEntities();
        public static int id = 0;
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Logout()
        {
            Session[CommonConstant.USER_SESSION] = null;
            return Redirect("/");
        }
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModels model)
        {
            //var result = new AccountModel().Login(model.UserName, model.PassWord);
           
            if (ModelState.IsValid)
            {
                var account = new AccountModel();
                var result = account.Login(model.UserName, MaHoa.MD5Hash(model.PassWord),true);

                if (result == 1)
                {
                    var user = account.GetID(model.UserName);
                    var userSession = new UserSession();
                    userSession.UserName = user.Email;
                    userSession.UserID = user.ID;
                    id = (int)(userSession.UserID);
                    userSession.GroupID = user.GroupID;
                    var listCredentials = account.GetListCredentials(model.UserName);

                    Session.Add(CommonConstant.SESSION_CREDENTIALS, listCredentials);

                    Session.Add(CommonConstant.USER_SESSION, userSession);
                   
                    return Redirect("/");
                }
                else if(result == -1)
                {
                    ModelState.AddModelError("", "Tài Khoản Đăng Nhập Bị Khóa");
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Tài Khoản Đăng Nhập Không Tồn Tại");
                }
                else if (result == -2)
                {
                    ModelState.AddModelError("", "Mật Khẩu Đăng Nhập Không Đúng");
                }
                else if (result == -3)
                {
                    ModelState.AddModelError("", "Tai Khoan Cua Ban Khong Co Quyen Dang Nhap");
                }
                else
                {
                    ModelState.AddModelError("", "Đăng Nhập Không Đúng");
                }
            }
            
            return View("Index");
        }
        public JsonResult GetStreet(int did)
        {
            var db = new DemoPPCRentalEntities();
            var streets = db.STREETs.Where(s => s.DISTRICT_Table.ID == did);
            return Json(streets.Select(s => new
            {
                id = s.ID,
                text = s.StreetName
            }), JsonRequestBehavior.AllowGet);
        }
        public ActionResult ListAgency()
        {
            var pro = db.PROPERTies.Where(x => x.UserID == id).ToList();
            return View(pro);
        }
        public ActionResult Create()
        {

            ListAll();
            return View();
        }
        public ActionResult Details(int id)
        {
            var pro = model.PROPERTies.Find(id);
            ViewBag.Images = Directory.EnumerateFiles(Server.MapPath("~/MultiImages"))
                             .Select(fn => "~/MultiImages/" + Path.GetFileName(fn));
            return View(pro);
        }

        // POST: Admin/Property/Create
        [HttpPost]
        public ActionResult Create(PROPERTy property, List<HttpPostedFileBase> files)
        {
            ListAll();

            try
            {

                // Xu ly Avatar

                string filename2 = Path.GetFileNameWithoutExtension(property.ImageFile2.FileName);
                string extension2 = Path.GetExtension(property.ImageFile2.FileName);
                filename2 = filename2 + DateTime.Now.ToString("yymmssfff") + extension2;
                property.Avatar = filename2;
                filename2 = Path.Combine(Server.MapPath("~/Avatar"), filename2);
                property.ImageFile2.SaveAs(filename2);



                // Images

                string filename = Path.GetFileNameWithoutExtension(property.ImageFile.FileName);
                string extension = Path.GetExtension(property.ImageFile.FileName);
                filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                property.Images = filename;
                filename = Path.Combine(Server.MapPath("~/Images"), filename);
                property.ImageFile.SaveAs(filename);





                property.Created_at = DateTime.Parse(DateTime.Now.ToShortDateString());
                property.UserID = id;
                property.Status_ID = 1;

                if (ModelState.IsValid)
                {
                    var model = new AccountModel();
                    long id = model.InsertProperty(property);

                    // SavemultiImage ----------------------------
                    var path = "";
                    foreach (var item in files)
                    {
                        if (item != null)
                        {
                            if (item.ContentLength > 0)
                            {
                                if (Path.GetExtension(item.FileName).ToLower() == ".jpg"
                                    || Path.GetExtension(item.FileName).ToLower() == ".png"
                                    || Path.GetExtension(item.FileName).ToLower() == ".gif"
                                    || Path.GetExtension(item.FileName).ToLower() == ".jpeg")
                                {
                                    var path0 = id + item.FileName;
                                    path = Path.Combine(Server.MapPath("~/MultiImages"), path0);

                                    item.SaveAs(path);
                                    ViewBag.UploadSuccess = true;

                                }
                            }
                        }
                    }
                    // End SaveMultiImage -------------------------

                    if (id > 0)
                    {
                        return RedirectToAction("ListAgency", "Login");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Create Property khong thanh cong");

                    }

                }

            }
            catch (NullReferenceException)
            {

                property.Created_at = DateTime.Parse(DateTime.Now.ToShortDateString());

                ListAll();
                property.UserID = id;
                property.Status_ID = 1;

                if (ModelState.IsValid)
                {
                    var model = new AccountModel();
                    long id = model.InsertProperty(property);
                    if (id > 0)
                    {
                        return RedirectToAction("ListAgency", "Login");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Create Property khong thanh cong");

                    }

                }
            }


            return View();
        }
        public void ListAll()
        {
           
            ViewBag.property_type = model.PROPERTY_TYPE.ToList();
            ViewBag.street = model.STREETs.OrderBy(x => x.StreetName).ToList();
            ViewBag.ward = model.WARDs.OrderBy(x => x.WardName).ToList();
            ViewBag.district = model.DISTRICT_Table.OrderBy(x => x.DistrictName).ToList();
            ViewBag.user = model.USERs.OrderBy(x => x.FullName).ToList();
            ViewBag.status = model.PROJECT_STATUS.OrderBy(x => x.Status_Name).ToList();
            //ViewBag.sale = model.Sla.ToList();

        }



    }
}