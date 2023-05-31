using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Prostech.WMS.API.Controllers;
using Prostech.WMS.BLL.Helpers.JWT;
using Prostech.WMS.BLL.Helpers.ValueChecker;
using Prostech.WMS.BLL.Interface;
using System.Security.Claims;
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

            if(ValueCheckerHelper.IsNullOrEmpty(token))
                throw new UnauthorizedAccessException("Token is null");

            if (!await userService.ValidateToken(token))
                throw new UnauthorizedAccessException("Token validate failed");

            await _next(httpContext);
        }
    }
}
