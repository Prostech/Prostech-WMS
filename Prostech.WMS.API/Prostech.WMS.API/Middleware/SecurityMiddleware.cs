using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Prostech.WMS.API.Controllers;
using Prostech.WMS.BLL.Helpers.ExceptionStatusCode;
using Prostech.WMS.BLL.Helpers.JWT;
using Prostech.WMS.BLL.Helpers.ValueChecker;
using Prostech.WMS.BLL.Interface;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Prostech.WMS.API.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class SecurityMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        private readonly Dictionary<string, string[]> _excludedRoutes;
        public SecurityMiddleware(RequestDelegate next,
            IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
            _excludedRoutes = _configuration.GetSection("ExcludedRoutes").Get<Dictionary<string, string[]>>();
        }

        public async Task Invoke(HttpContext httpContext, IUserService userService)
        {
            try
            {
                string path = httpContext.Request.Path.Value;
                string method = httpContext.Request.Method;

                if (ValueCheckerHelper.IsNotNull(_excludedRoutes) &&
                    _excludedRoutes.ContainsKey(path) &&
                    _excludedRoutes[path].Contains(method))
                {
                    // Skip the middleware and proceed to the next middleware in the pipeline
                    await _next(httpContext);
                    return;
                }

                string token = httpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

                if (ValueCheckerHelper.IsNullOrEmpty(token))
                    throw new UnauthorizedAccessException("Token is null");

                await userService.ValidateToken(token);

                await _next(httpContext);
            }
            catch (Exception e)
            {
                var errorResponse = e.Message;

                // Serialize the error response to JSON
                var jsonResponse = JsonConvert.SerializeObject(errorResponse);

                // Set the response status code and write the JSON to the response body
                ExceptionStatusCodeHelper.SetStatusCode(httpContext, e);
                httpContext.Response.ContentType = "application/json";
                await httpContext.Response.WriteAsync(jsonResponse, Encoding.UTF8);
            }
        }
    }
}
