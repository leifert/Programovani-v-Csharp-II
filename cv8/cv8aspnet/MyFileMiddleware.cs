using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace cv8aspnet
{
    public class MyFileMiddleware
    {
        public MyFileMiddleware(RequestDelegate next)
        {
        }

        public async Task Invoke(HttpContext context)
        {
            string path = context.Request.Path;
            string filePath = Path.GetFullPath("./files" + path);
            if (Path.GetExtension(path) == ".png")
            {
                throw new NotImplementedException();
            }

            if (File.Exists(filePath))
            {
                context.Response.Headers.Add("Content-Type","image/jpeg");
                //await context.Response.SendFileAsync(filePath);

                using FileStream fs = new FileStream(filePath, FileMode.Open);
                await fs.CopyToAsync(context.Response.Body);
            }
            else
            {
                context.Response.Headers.Add("Content-Type","text/html");
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync("Soubor neexistuje!");
            }
        }
    }
}
