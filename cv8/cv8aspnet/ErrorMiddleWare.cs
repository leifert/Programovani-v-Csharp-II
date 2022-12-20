using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace cv8aspnet
{
    public class ErrorMiddleWare
    {
        private RequestDelegate next;
        public ErrorMiddleWare(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context, ExceptionHandler handler)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (Exception e)
            {
                if (!context.Response.HasStarted)
                {
                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "text/plain; charset=utf-8";
                }
               
                await context.Response.WriteAsync("Došlo k chybě");
                await File.AppendAllTextAsync("log.txt", e.Message + "\n");
                await handler.Handle(e);
            }

        }
    }
}
