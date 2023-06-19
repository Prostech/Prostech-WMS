namespace Prostech.WMS.API.Models
{
    public class AppSettings
    {
        public string AllowOrigin { get; set; }
        public DatabaseConnection DatabaseConnection { get; set; }
        public JwtSettings JwtSettings { get; set; }
        public string RCSUrl { get; set; }
        public CobotElite CobotElite { get; set; }
        public string DbConnection { get; set; }
    }
}
