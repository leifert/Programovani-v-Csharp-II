using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using System.Text.Json;
using WebApplication10.Models;

namespace WebApplication10.Controllers
{
    public class HomeController : Controller
    {


        private readonly ProductService productService;

        public HomeController(ProductService productService)
        {
            this.productService = productService;
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Products(/* [FromServices] ProductService productService */)
        {
            var products = productService.GetProducts();
            
            ViewBag.Products = products;
            // ViewData["Products"] = products;

            return View();
        }


        public IActionResult Product(int id)
        {
            Product product = productService.GetProduct(id);
            if(product == null)
            {
                // return NotFound();
                return new NotFoundResult();
            }

            ViewBag.Product = product;

            return View(new AddToBasketForm()
            {
                Count = 1,
                ProductId = product.Id
            });
        }

        [HttpPost]
        public IActionResult Product(int id, AddToBasketForm form)
        {

            if (form.Count <= 0) {
                ModelState.AddModelError("Count", "Minimální počet je 1.");
            }

            if (ModelState.IsValid)
            {
                this.HttpContext.Session
                    .SetString(
                    "basket", 
                    JsonSerializer.Serialize(new List<AddToBasketForm>() { 
                        form
                    })
                    );

                return RedirectToAction("Basket", "Home");
            }

            Product product = productService.GetProduct(id);
            if (product == null)
            {
                // return NotFound();
                return new NotFoundResult();
            }

            ViewBag.Product = product;

            return View(form);
        }

        private void AddDeliveryOptions()
        {
            ViewBag.DeliveryOptions = new List<SelectListItem>()
            {
                new SelectListItem("PPL", "0"),
                new SelectListItem("Osobní převzetí", "1")
            };

        }

        public IActionResult Basket()
        {
            ViewBag.BasketData = this.HttpContext.Session.GetString("basket");
            AddDeliveryOptions();
            return View();
        }


        [HttpPost]
        public IActionResult Basket(OrderForm form)
        {
            if (ModelState.IsValid)
            {
                // TODO: uložit

                return RedirectToAction("Finished");
            }

            ViewBag.BasketData = this.HttpContext.Session.GetString("basket");
            AddDeliveryOptions();
            return View(form);
        }

        public IActionResult Finished()
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