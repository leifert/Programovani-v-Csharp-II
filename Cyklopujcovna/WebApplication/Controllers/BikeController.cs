using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication.Models;
using WebApplication.Services;

namespace WebApplication.Controllers
{
    public class BikeController : Controller
    {
        private readonly BikeService _bikeService;

        public BikeController(BikeService bikeService)
        {
            _bikeService = bikeService;
        }
        public IActionResult Index()
        {
            string login = HttpContext.Session.GetString("Username");
            if (login == null)
            {
                return RedirectToAction("ErrorForm", "Home");
            }
            else
            {
                var bikes = _bikeService.SelectBike();
                ViewBag.Bikes = bikes;
                return View();
            }
        }

        public IActionResult Create()
        {
            string login = HttpContext.Session.GetString("Username");
            if (login == null)
            {
                return RedirectToAction("ErrorForm", "Home");
            }
            else
            {
                List<SelectListItem> typ = new List<SelectListItem>()
                {
                    new SelectListItem("Horské", "Horské"),
                    new SelectListItem("Silniční", "Silniční"),
                    new SelectListItem("Cyklokrosové", "Cyklokrosové"),
                };
                ViewBag.typ = typ;
                return View();
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Bike bike)
        {
            
            if (ModelState.IsValid)
            {
                _bikeService.AddBike(bike);
                return RedirectToAction("Index");
            }
            return View();
        }


        public ActionResult Edit(int id)
        {
            string login = HttpContext.Session.GetString("Username");
            if (login == null)
            {
                return RedirectToAction("ErrorForm", "Home");
            }
            else
            {
                List<SelectListItem> typ = new List<SelectListItem>()
                {
                    new SelectListItem("Horské", "Horské"),
                    new SelectListItem("Silniční", "Silniční"),
                    new SelectListItem("Cyklokrosové", "Cyklokrosové"),
                };
                ViewBag.typ = typ;
                Bike tmpBike = new Bike() {Id = id, BikeName = "test", BikePrice = 100, BikeType = "Horské"};

                Bike b = _bikeService.SelectBikeById(tmpBike);
                if (b == null)
                {
                    return HttpNotFound();
                }

                return View(b);
            }
        }

        private ActionResult HttpNotFound()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Bike bike)
        {
            if (ModelState.IsValid)
            {
                _bikeService.UpdateBike(bike);
                return RedirectToAction("Index");
            }
            return View(bike);
        }


        public ActionResult Delete(int id)
        {
            string login = HttpContext.Session.GetString("Username");
            if (login == null)
            {
                return RedirectToAction("ErrorForm", "Home");
            }
            else
            {
                Bike tmpBike = new Bike() {Id = id, BikeName = "test", BikePrice = 100, BikeType = "Horské"};

                Bike b = _bikeService.SelectBikeById(tmpBike);
                if (b == null)
                {
                    return HttpNotFound();
                }

                return View(b);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Bike bike)
        {
     
            _bikeService.DeleteBike(bike);
            return RedirectToAction("Index");
            
        }
    }
}
