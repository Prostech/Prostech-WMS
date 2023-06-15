using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Prostech.WMS.API.Models;
using Prostech.WMS.BLL.Helpers.ExceptionStatusCode;
using Prostech.WMS.BLL.Helpers.Time;
using Prostech.WMS.BLL.Interface;
using Prostech.WMS.DAL.DTOs.UserAccount;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Prostech.WMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RCSController : ControllerBase
    {
        private readonly AppSettings _appSettings;
        private readonly ILogger<RCSController> _logger;

        public RCSController(IOptions<AppSettings> appSettings, ILogger<RCSController> logger)
        {
            _appSettings = appSettings.Value;
            _logger = logger;
        }

        [HttpPost("receive-event")]
        public async Task<IActionResult> ReceiveAGVEvent([FromBody] ReceiveEvent events)
        {
            string token = new string("");
            try
            {
                _logger.LogInformation(JsonConvert.SerializeObject(events));
                _logger.LogInformation("Receive event successfully -- " + TimeHelper.CurrentTime.ToString());

                return new JsonResult(new
                {
                    Id = 1,
                    Message = "Receive event successfully -- " + TimeHelper.CurrentTime.ToString(),
                    Event = events,
                });
            }
            catch (Exception ex)
            {
                ExceptionStatusCodeHelper.SetStatusCode(HttpContext, ex);
                _logger.LogError(ex.Message);
                return new JsonResult(ex.Message);
            }
        }


        private async Task<AGVReturnMessage> genAgvSchedulingTask(string Pod, string FromPosition, string ToPosition)
        {
            try
            {
                AGVReturnMessage result = new AGVReturnMessage();

                using (var client = new HttpClient()) // Create an HTTP client to make API call
                {
                    Uri endpoint = new Uri($"{_appSettings.RCSUrl}/rcms/services/rest/hikRpcService/genAgvSchedulingTask"); // API endpoint URL

                    var paramObj = new // Create an anonymous object to hold request parameters
                    {
                        reqCode = GenerateRandomString(), // Request code
                        taskTyp = 1,
                        positionCodePath = new[]
                        {
                            new
                            {
                                positionCode = FromPosition,
                                type = "00"
                            },
                            new
                            {
                                positionCode = ToPosition,
                                type = "00"
                            }
                        },
                        podCode = Pod
                    };
                    var dataJson = JsonConvert.SerializeObject(paramObj); // Serialize request parameters to JSON
                    var payload = new StringContent(dataJson, Encoding.UTF8, "application/json"); // Create a StringContent object with serialized JSON as payload

                    var resultString = client.PostAsync(endpoint, payload).Result.Content.ReadAsStringAsync().Result; // Send POST request to API, read response content as string and store in 'result' variable
                    result = JsonConvert.DeserializeObject<AGVReturnMessage>(resultString);
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }
        private string GenerateRandomString()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            int limit = random.Next(8, 33); // Generates a random number between 8 and 32 (inclusive)
            string randomString = new string(Enumerable.Repeat(chars, limit)
                .Select(s => s[random.Next(s.Length)])
                .ToArray());
            return randomString;
        }
    }
}
