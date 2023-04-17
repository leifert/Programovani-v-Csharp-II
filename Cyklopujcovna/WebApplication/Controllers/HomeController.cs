using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using WebApplication.Models;
using WebApplication.Services;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BikeService _bikeService;

        public HomeController(ILogger<HomeController> logger, BikeService bikeService)
        {
            _logger = logger;
            _bikeService = bikeService;
        }

        public IActionResult Index()
        {
            var bikes = _bikeService.SelectBike();
            ViewBag.Bikes = bikes;
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(Employee emp)
        {
            
            if (ModelState.IsValid)
            {
                if (_bikeService.Login(emp))
                {
                    HttpContext.Session.SetString("Username",emp.Name);
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(nameof(Employee.Name), "Nesprávné přihlašovací údaje!");
                    ModelState.AddModelError(nameof(Employee.Passwd), "Nesprávné přihlašovací údaje!");
                }
                
               
            }
            return View();
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        public IActionResult ErrorForm()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
