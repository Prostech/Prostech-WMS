using System.Net;

namespace Prostech.WMS.BLL.Helpers.Wrapper
{
    [Serializable]
    public class ApiResponse
    {
        public ApiResponse(
            string requestUrl,
            object? data,
            string? error,
            bool status = false,
            HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError,
            int page = 0,
            int pageSize = 0,
            int totalRecord = 0,
            int totalPages = 0)
        {
            RequestUrl = requestUrl;
            Data = data;
            Error = error;
            Status = status;
            StatusCode = httpStatusCode;
            Page = page;
            PageSize = pageSize;
            TotalRecord = totalRecord;
            TotalPages = totalPages;
        }

        public string RequestUrl { get; set; }
        public object? Data { get; set; }
        public string? Error { get; set; }
        public bool Status { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecord { get; set; }
    }

}
