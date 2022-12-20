using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace cv8aspnet
{
    public class BrowserAuthMiddleware
    {
        private RequestDelegate next;
        public BrowserAuthMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            string ua = context.Response.Headers["User-Agent"].ToString();

            if (ua.Contains("Chrome") && !ua.Contains("Edg/"))
            {
                await next.Invoke(context);
            }
            else
            {
                context.Response.ContentType = "text/html; charset=utf-8";
                await context.Response.WriteAsync("Použijte chrome!");
            }
        }
    }
}
