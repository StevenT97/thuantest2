using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using Models.FrameWork;

namespace TEDU_MVC.Areas.Admin.Controllers
{
    public class Project_TypeController : BaseController
    {
        // GET: Admin/Project_TypeController
        public ActionResult Index()
        {
            var iplProjectType = new Project_TypeModel();
            var model = iplProjectType.ListAll();
            return View(model);
        }

        // GET: Admin/Project_TypeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/Project_TypeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Project_TypeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PROPERTY_TYPE collection)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    var model = new Project_TypeModel();
                    int res = model.Create(collection.CodeType, collection.Status);
                    if (res > 0)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("","Thêm mới không thành công");
                    }
                }
                return View(collection);
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Project_TypeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/Project_TypeController/Edit/5
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

        // GET: Admin/Project_TypeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Project_TypeController/Delete/5
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
