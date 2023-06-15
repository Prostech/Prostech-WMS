namespace Prostech.WMS.API.Models
{
    public class ReceiveEvent
    {
        public string Caller { get; set; } = String.Empty;
        public string EventType { get; set; } = String.Empty;
        public string EventTime { get; set; } = String.Empty;
    }
}
