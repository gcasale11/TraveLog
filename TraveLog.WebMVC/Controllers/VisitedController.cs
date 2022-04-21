using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TraveLog.Models;
using TraveLog.Services;

namespace TraveLog.Web.Controllers
{
    [Authorize]
    public class VisitedController : Controller
    {
        // GET: Visited
        public ActionResult Index()
        {
            string UserId = User.Identity.GetUserId();
            var service = new VisitedService(UserId);
            var model = service.GetVisit();
            return View(model);
        }

        //Get the create View
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VisitedCreate model) //verifies model is valid then grabs userId & calls on VisitedCreate & returns the user back to index view

        {

            if (!ModelState.IsValid) return View(model);

            var service = CreateVisitedService();

            if (service.CreateVisited(model))
            {
                TempData["SaveResult"] = "You're visit had been created, and your passport has been stamped";
                return RedirectToAction("Create", "Blog");
            };

            ModelState.AddModelError("", "Sorry, You're visit could not be created, and your passport could not be stamped");
            return View(model);
        }

        public ActionResult Details(int id)
        {
            var svc = CreateVisitedService();
            var model = svc.GetVisitedById(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var service = CreateVisitedService();
            var detail = service.GetVisitedById(id);
            var model =
                new VisitedEdit
                {
                    VisitedId = detail.VisitedId,
                    DateVisited = detail.DateVisited,
                    InitialThoughts = detail.InitialThoughts
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, VisitedEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if(model.VisitedId != id)
            {
                ModelState.AddModelError("", "Mismatch, Cannot verify Your ID");
                return View(model);
            }

            var service = CreateVisitedService();

            if (service.UpdateVisited(model))
            {
                TempData["SaveResult"] = "Your visit has been updated";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your visit has not been updated, feel free to try again");
            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateVisitedService();
            var model = svc.GetVisitedById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateVisitedService();

            service.DeleteVisited(id);

            TempData["SaveResult"] = "Your visit has been deleted, as if you'd never been";

            return RedirectToAction("Index");
        }

        private VisitedService CreateVisitedService()
        {
            var userId = User.Identity.GetUserId();
            var service = new VisitedService(userId);
            return service;
        }
    }
}