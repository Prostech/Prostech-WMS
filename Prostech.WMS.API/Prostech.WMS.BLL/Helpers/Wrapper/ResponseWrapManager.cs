using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Prostech.WMS.BLL.Helpers.ValueChecker;
using System.Net;
using static LinqToDB.Common.Configuration;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Prostech.WMS.BLL.Helpers.Wrapper
{
    /// <summary>
    /// Response Wrap Manager to handle any customizations on result and return Custom Formatted Response.
    /// </summary>
    public static class ResponseWrapManager
    {
        /// <summary>
        /// The Response Wrapper method handles customizations and generate Formatted Response.
        /// </summary>
        /// <param name="result">The Result</param>
        /// <param name="context">The HTTP Context</param>
        /// <param name="exception">The Exception</param>
        /// <returns>Sample Response Object</returns>
        public static ApiResponse ResponseWrapper(object? result, HttpContext context, object? exception = null, int page = 0, int pageSize = 0)
        {
            try
            {
                var requestUrl = context.Request.GetDisplayUrl();
                var data = result;
                var error = exception != null ? exception.ToString() : null;
                var status = result != null;
                var httpStatusCode = (HttpStatusCode)context.Response.StatusCode;
                page = (ValueCheckerHelper.IsNotNullOrZero(page)) ? page : 1;
                pageSize = (ValueCheckerHelper.IsNotNullOrZero(pageSize)) ? pageSize : 10;
                var totalRecords = ServiceConstants.TotalRecords;
                var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

                // NOTE: Add any further customizations if needed here

                return new ApiResponse(requestUrl, data, error, status, httpStatusCode, page, pageSize, totalRecords, totalPages);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
