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
    public class RentalController : Controller
    {
        private readonly BikeService _bikeService;

        public RentalController(BikeService bikeService)
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
                var rentals = _bikeService.SelectRentals();
                ViewBag.Rentals = rentals;
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
            List<Bike> bikes = _bikeService.SelectBike();
            ViewBag.Bikes = new SelectList(bikes,"Id","BikeName");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Rental rental)
        {
            
            if (ModelState.IsValid)
            {
                _bikeService.AddRental(rental);
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

            Rental tmpRent = new Rental() { Id = id};
            List<Bike> bikes = _bikeService.SelectBike();
            ViewBag.Bikes = new SelectList(bikes,"Id","BikeName");

            Rental b = _bikeService.SelectRentalById(tmpRent);
            if (b == null)
            {
                return new NotFoundResult();
            }
            return View(b);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Rental rental)
        {
            if (ModelState.IsValid)
            {
                _bikeService.UpdateRental(rental);
                return RedirectToAction("Index");
            }
            return View(rental);
        }

        public ActionResult Delete(int id)
        {
            string login = HttpContext.Session.GetString("Username");
            if (login == null)
            {
                return RedirectToAction("ErrorForm", "Home");
            }
            Rental tmpRent = new Rental() { Id = id};

            Rental r = _bikeService.SelectRentalById(tmpRent);
            if (r == null)
            {
                return new NotFoundResult();
            }
            return View(r);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Rental rental)
        {
     
            _bikeService.DeleteRental(rental);
            return RedirectToAction("Index");
            
        }

    }
}
