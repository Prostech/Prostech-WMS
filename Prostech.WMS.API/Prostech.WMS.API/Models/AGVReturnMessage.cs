namespace Prostech.WMS.API.Models
{
    public class AGVReturnMessage
    {
        public string Data { get; set; }
        public string Message { get; set; }
        public string ReqCode { get; set; }
        public string Code { get; set; }
        public string Interrupt { get; set; }
    }
    
    public class AGVCallBack
    {
        public string reqCode { get; set; } = String.Empty;
        public string method { get; set; } = String.Empty;
        public string podCode { get; set; } = String.Empty;
    }
}
