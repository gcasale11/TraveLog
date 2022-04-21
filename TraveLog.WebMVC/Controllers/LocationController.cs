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
    public class LocationController : Controller
    {
        // GET: Location
        public ActionResult Index()
        {
            string UserId = User.Identity.GetUserId();
            var service = new LocationService(UserId);
            var model = service.GetLocation();
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LocationCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateLocationService();

            if (service.CreateLocation(model))
            {
                TempData["SaveResult"] = "The City you added has been created";
                return RedirectToAction("Create", "Country");
            };

            ModelState.AddModelError("", "Sorry the City you tried to add has not been created");
            return View(model);
        }

        public ActionResult Details(int id)
        {
            var svc = CreateLocationService();
            var model = svc.GetLocationById(id);

            return View(model);
        }
        private LocationService CreateLocationService()
        {
            var userId = User.Identity.GetUserId();
            var service = new LocationService(userId);
            return service;
        }

        public ActionResult Edit(int id)
        {
            var service = CreateLocationService();
            var detail = service.GetLocationById(id);
            var model =
                new LocationEdit
                {
                    LocationId = detail.LocationId,
                    Cities = detail.Cities
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit(int id, LocationEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if(model.LocationId != id)
            {
                ModelState.AddModelError("", "Hmm, the ID was not a match to the City you are looking for");
                return View(model);
            }
            var service = CreateLocationService();

            if (service.UpdateLocation(model))
            {
                TempData["SaveResult"] = "Your City has been updated";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your City has not been updated");
            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateLocationService();
            var model = svc.GetLocationById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public ActionResult DeletePost(int id)
        {
            var service = CreateLocationService();

            service.DeleteLocation(id);

            TempData["SaveResult"] = "Your City has been deleted from your list";

            return RedirectToAction("Index");
        }

    }
}