using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MVCApp.Controllers
{
    public class TemplateMiddleware
    {
        private readonly RequestDelegate _next;

        TemplateMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            // You can add action that should be done before transferred into next middleware in pipeline below


            // If this is non terminal middleware you can add this
            await _next(context); //or await _next.Invoke(context);

            // You can add action that should be done after the context received from the next middleware in pipeline below


        }
    }
}
