using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Prostech.WMS.API.Models;
using Prostech.WMS.BLL.Helpers.ExceptionStatusCode;
using Prostech.WMS.BLL.Interface;
using Prostech.WMS.DAL.DTOs.UserAccount;
using System.ComponentModel.DataAnnotations;

namespace Prostech.WMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RCSController : ControllerBase
    {
        private readonly AppSettings _appSettings;
        private ILogger Logger { get; }

        public RCSController(IOptions<AppSettings> appSettings, ILoggerFactory loggerFactory)
        {
            _appSettings = appSettings.Value;
            this.Logger = loggerFactory.CreateLogger("AwesomeLogger");
        }

        [HttpPost("receive-agv-event")]
        public async Task<IActionResult> ReceiveAGVEvent()
        {
            string token = new string("");
            try
            {
                DateTime utcTime = DateTime.UtcNow;
                TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
                DateTime cstTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, cstZone);
                this.Logger.LogDebug("Receive AGV event successfully --- " + cstTime.ToString("dd/MM/yyyy HH:MM:ss:FF"));
                return new JsonResult(
                        new
                        {
                            Id = 1,
                            Message = "Receive AGV event successfully --- " + cstTime.ToString("dd/MM/yyyy HH:MM:ss:FF"),
                        });
            }
            catch (Exception ex)
            {
                ExceptionStatusCodeHelper.SetStatusCode(HttpContext, ex);
                this.Logger.LogDebug(ex.Message);
                return new JsonResult(ex.Message);
            }
        }
    }
}
