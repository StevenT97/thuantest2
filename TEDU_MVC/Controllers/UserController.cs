using Models;
using Models.FrameWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TEDU_MVC.Code;

namespace TEDU_MVC.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Send(string name, string email, string password, string phone, string address)
        {
           var user = new USER();
            user.FullName = name;
            user.Email = email;

            var encryptedMd5Pas = MaHoa.MD5Hash(password);
            user.Password = encryptedMd5Pas;
            user.Phone = phone;
            user.Address = address;
            user.GroupID = "AGENCY";
            user.Role = "2";
            user.Status = false;

            var id = new AccountModel().Insert(user);
            if (id >= 0)
            {
            return Json(new
            {
                status = true
            });
            //send mail
        }

            else
                return Json(new
                {
                    status = false
                });
 
        }
        [HttpPost]
        public ActionResult Register(string Username, string Email)
        {
            return View();
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
