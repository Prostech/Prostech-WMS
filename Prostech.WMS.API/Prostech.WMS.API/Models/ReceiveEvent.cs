namespace Prostech.WMS.API.Models
{
    public class ReceiveEvent
    {
        public string CallerName { get; set; } = String.Empty;
        public string TaskType { get; set; } = String.Empty;
    }

    public class CobotElite
    {
        public string ServerAddress { get; set; }
        public int ServerPort { get; set; }
    }
}
