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
    public class CountryController : Controller
    {
        // GET: Country
        public ActionResult Index()
        {
            string UserId = User.Identity.GetUserId();
            var service = new CountryService(UserId);
            var model = service.GetCountry();
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CountryCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateCountryService();

            if (service.CreateCountry(model))
            {
                TempData["SaveResult"] = "The Country you added has been created";
                return RedirectToAction("Create", "Visited");
            };

            ModelState.AddModelError("", "Sorry the Country you tried to add has not been created");
            return View(model);
        }

        public ActionResult Details(int id)
        {
            var svc = CreateCountryService();
            var model = svc.GetCountryById(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var service = CreateCountryService();
            var detail = service.GetCountryById(id);
            var model =
                new CountryEdit
                {
                    CountryId = detail.CountryId,
                    CountryName = detail.CountryName,
                    Continent = detail.Continent
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit(int id, CountryEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if(model.CountryId != id)
            {
                ModelState.AddModelError("", "Oh my, the ID was not a match to the Country you seek");
                return View(model);
            }
            var service = CreateCountryService();

            if (service.UpdateCountry(model))
            {
                TempData["SaveResult"] = "Your Country has been updated";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your Country has not been updated");
            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateCountryService();
            var model = svc.GetCountryById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public ActionResult DeletePost(int id)
        {
            var service = CreateCountryService();

            service.DeleteCountry(id);

            TempData["SaveResult"] = "Your Country has been deleted from your list";

            return RedirectToAction("Index");
        }

        private CountryService CreateCountryService()
        {
            var userId = User.Identity.GetUserId();
            var service = new CountryService(userId);
            return service;
        }
    }
}