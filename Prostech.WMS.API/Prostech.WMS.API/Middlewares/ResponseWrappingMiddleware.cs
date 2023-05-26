﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Prostech.WMS.BLL.Helpers.Wrapper;
using System.Threading.Tasks;

namespace Prostech.WMS.API.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ResponseWrappingMiddleware
    {
        private readonly RequestDelegate _next;

        public ResponseWrappingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            // Storing Context Body Response
            var currentBody = context.Response.Body;

            // Using MemoryStream to hold Controller Response
            using var memoryStream = new MemoryStream();
            context.Response.Body = memoryStream;

            // Passing call to Controller
            await _next(context);

            // Resetting Context Body Response
            context.Response.Body = currentBody;

            // Setting Memory Stream Position to Beginning
            memoryStream.Seek(0, SeekOrigin.Begin);

            // Read Memory Stream data to the end
            var readToEnd = new StreamReader(memoryStream).ReadToEnd();

            // Deserializing Controller Response to an object
            var result = JsonConvert.DeserializeObject(readToEnd);

            var response = new object();

            if(context.Response.StatusCode == 200)
            {
                int page = Convert.ToInt32(context.Request.Query["Page"]);
                int pageSize = Convert.ToInt32(context.Request.Query["PageSize"]);
                // Invoking Customizations Method to handle Custom Formatted Response
                response = ResponseWrapManager.ResponseWrapper(result, context,  null, page, pageSize);
            }else
            {
                // Invoking Customizations Method to handle Custom Formatted Response
                response = ResponseWrapManager.ResponseWrapper(null, context, result);
            }    

            // returing response to caller
            await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder ResponseWrappingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ResponseWrappingMiddleware>();
        }
    }
}
