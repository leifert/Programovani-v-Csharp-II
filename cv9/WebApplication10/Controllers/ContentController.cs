using Microsoft.AspNetCore.Mvc;
using System.Xml.Serialization;
using WebApplication10.Models;

namespace WebApplication10.Controllers
{
    public class ContentController : Controller
    {
        public IActionResult GetJson([FromServices] ProductService productService, int limit = 2)
        {
            // return Json(productService.GetProducts());
            return new JsonResult(productService.GetProducts().Take(limit));
        }

        public IActionResult GetXml([FromServices] ProductService productService)
        {
            MemoryStream stream = new MemoryStream();
            XmlSerializer xSer = new XmlSerializer(typeof(List<Product>));
            xSer.Serialize(stream, productService.GetProducts());

            stream.Seek(0, SeekOrigin.Begin);

            return new FileStreamResult(stream, "text/xml");
        }
    }
}
