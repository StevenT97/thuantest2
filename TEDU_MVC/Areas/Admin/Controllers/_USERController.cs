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
    public class _USERController : BaseController
    {
        // GET: Admin/_USER
        public ActionResult Index()
        {
            return View();
        }

        // GET: Admin/_USER/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/_USER/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/_USER/Create
        [HttpPost]
        public ActionResult Create(USER collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var db = new DemoPPCRentalEntities();

                    var encryptedMd5Pas = MaHoa.MD5Hash(collection.Password);
                    collection.Password = encryptedMd5Pas;
                    //db.USER_TABLE.Add(collection);
                    //db.SaveChangesAsync();
                    var dao = new AccountModel();
                    long id = dao.Insert(collection);
                    if (id > 0)
                    {
                        return RedirectToAction("Index", "_USER");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Thêm user không thành công");
                    }

                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View("Index");
            }
        }

        // GET: Admin/_USER/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/_USER/Edit/5
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

        // GET: Admin/_USER/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/_USER/Delete/5
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
