using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace cv8aspnet
{
    public class FormMiddleware
    {
        private RequestDelegate next;
        public FormMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            string path = context.Request.Path;

            if (path == "/form")
            {
                context.Response.Headers.Add("Content-Type","text/html");
                context.Response.WriteAsync(@"
                <form method=""post"">
                    <input name = ""inputName""/>
                    <button type=""submit"">Odeslat</button>
                   </form>");
                if (context.Request.Method == "POST")
                {
                    string val = context.Request.Form["inputName"];
                    await context.Response.WriteAsync(WebUtility.HtmlEncode(val));
                }
            }
            else
            {
                await next.Invoke(context);
            }
        }
    }
}
