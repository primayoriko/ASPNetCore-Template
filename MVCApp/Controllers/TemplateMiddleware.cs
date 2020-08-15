using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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

        // Or this is an example of Invoke Implementation
        // This implementation goal is printing HTTP request and response into the console
        /*public async Task Invoke(HttpContext context)
        {
            var request = await FormatRequest(context.Request);

            System.Diagnostics.Debug.WriteLine(request);

            var originalBodyStream = context.Response.Body;

            using (var responseBody = new MemoryStream())
            {
                context.Response.Body = responseBody;

                await _next(context);

                var response = await FormatResponse(context.Response);

                System.Diagnostics.Debug.WriteLine(response);

                await responseBody.CopyToAsync(originalBodyStream);
            }
        }

        public async Task<string> FormatRequest(HttpRequest request)
        {
            var body = request.Body;

            request.EnableBuffering();

            var buffer = new Byte[Convert.ToInt32(request.ContentLength)];

            await request.Body.ReadAsync(buffer, 0, buffer.Length);

            var bodyAsText = Encoding.UTF8.GetString(buffer);

            request.Body = body;

            return $"{request.Scheme} {request.Host}{request.Path} {request.QueryString} {bodyAsText}";
        }

        public async Task<string> FormatResponse(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);

            string text = await new StreamReader(response.Body).ReadToEndAsync();

            response.Body.Seek(0, SeekOrigin.Begin);

            return $"{response.StatusCode}: {text}";
        }*/
    }
}
