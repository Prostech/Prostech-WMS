using LinqToDB.SqlQuery;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prostech.WMS.BLL.Helpers.ExceptionStatusCode
{
    public static class ExceptionStatusCodeHelper
    {
        public static void SetStatusCode(HttpContext context, Exception ex)
        {
            switch (ex)
            {
                case ArgumentNullException:
                case ArgumentException:
                case InvalidOperationException:
                case FormatException:
                case BadHttpRequestException:
                    context.Response.StatusCode = 400; // Bad Request
                    break;
                case UnauthorizedAccessException:
                    context.Response.StatusCode = 401; // Unauthorized
                    break;
                case KeyNotFoundException:
                    context.Response.StatusCode = 404; // Not Found
                    break;
                case NotImplementedException:
                    context.Response.StatusCode = 501; // Not Implemented
                    break;
                case TimeoutException:
                    context.Response.StatusCode = 504; // Gateway Timeout
                    break;
                case IOException:
                case SqlException:
                case AggregateException:
                    context.Response.StatusCode = 500; // Internal Server Error
                    break;
                default:
                    context.Response.StatusCode = 500; // Internal Server Error (default)
                    break;
            }
        }

    }
}
