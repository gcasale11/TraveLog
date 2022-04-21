using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TraveLog.Models;
using TraveLog.Services;

namespace TraveLog.WebMVC.Controllers
{
    [Authorize]
    public class BlogController : Controller
    {
        // GET: Blog
        public ActionResult Index()
        {
            string UserId = User.Identity.GetUserId();
            var service = new BlogService(UserId);
            var model = service.GetBlogs();
            return View(model);
        }

        //Get the create View
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BlogCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateBlogService();

            if (service.CreateBlog(model))
            {
                TempData["SaveResult"] = "Written memories of your trip had been created";
                return RedirectToAction("Details", "Visited");
            };

            ModelState.AddModelError("", "Sorry, unable to compute your written memories");
            return View(model);
        }

        public ActionResult Details(int id)
        {
            var svc = CreateBlogService();
            var model = svc.GetBlogById(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var service = CreateBlogService();
            var detail = service.GetBlogById(id);
            var model =
                new BlogEdit
                {
                    BlogId = detail.BlogId,
                    Thoughts = detail.Thoughts
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit(int id, BlogEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if(model.BlogId != id)
            {
                ModelState.AddModelError("", "Oof, mismatch ID number, can't find your written memory");
                return View(model);
            }
            var service = CreateBlogService();

            if (service.UpdateBlog(model))
            {
                TempData["SaveResult"] = "Your written memories have been updated";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your written memories have not been updated");
            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateBlogService();
            var model = svc.GetBlogById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public ActionResult DeletePost(int id)
        {
            var service = CreateBlogService();

            service.DeleteBlog(id);

            TempData["SaveResult"] = "Your written memories have been delete for this visit";

            return RedirectToAction("Index");
        }

        private BlogService CreateBlogService()
        {
            var userId = User.Identity.GetUserId();
            var service = new BlogService(userId);
            return service;
        }
    }
}