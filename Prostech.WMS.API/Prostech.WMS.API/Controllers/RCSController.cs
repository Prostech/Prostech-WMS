using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Prostech.WMS.API.Models;
using Prostech.WMS.BLL.Helpers.ExceptionStatusCode;
using Prostech.WMS.BLL.Helpers.Time;
using Prostech.WMS.BLL.Interface;
using Prostech.WMS.DAL.DTOs.UserAccount;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Prostech.WMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RCSController : ControllerBase
    {
        private readonly AppSettings _appSettings;
        private readonly ILogger<RCSController> _logger;
        private IMemoryCache _cache;
        public RCSController(IOptions<AppSettings> appSettings, ILogger<RCSController> logger, IMemoryCache cache)
        {
            _appSettings = appSettings.Value;
            _logger = logger;
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        [HttpPost("receive-event")]
        public async Task<IActionResult> ReceiveAGVEvent([FromBody] ReceiveEvent events)
        {
            string token = new string("");
            try
            {
                _logger.LogInformation(JsonConvert.SerializeObject(events));
                _logger.LogInformation("Receive event successfully || " + TimeHelper.CurrentTime.ToString());


                string cacheKey = "myKey";
                string cacheValue = "Hello, world!";

                string cachedData = _cache.Get(cacheKey) as string;


                _cache.Set(cacheKey, cacheValue);

                cachedData = _cache.Get(cacheKey) as string;


                return new JsonResult(new
                {
                    Id = 1,
                    Message = "Receive event successfully || " + TimeHelper.CurrentTime.ToString(),
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
                    _logger.LogInformation(JsonConvert.SerializeObject(result));
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                ExceptionStatusCodeHelper.SetStatusCode(HttpContext, ex);
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

        [HttpPost("send-cobot-elite-task")]
        public async Task<ActionResult> SendMessageToServer([FromBody] string message)
        {
            _logger.LogInformation("Server address: " + _appSettings.CobotElite.ServerAddress);
            _logger.LogInformation("Server port: " + _appSettings.CobotElite.ServerPort);


            //IPEndPoint ipEndpoint = new IPEndPoint(192.1, 800);
            try
            {

                // Create a TCP/IP socket
                using (var clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                {
                    //clientSocket.Bind(ipEndpoint);
                    //clientSocket.Listen(10);

                    // Connect to the server
                    clientSocket.Connect(_appSettings.CobotElite.ServerAddress, _appSettings.CobotElite.ServerPort);

                    // Convert the message to bytes
                    var messageBytes = Encoding.ASCII.GetBytes(message);

                    // Send the message to the server
                    clientSocket.Send(messageBytes);

                    // Receive the response from the server
                    var responseBytes = new byte[1024];
                    var bytesRead = clientSocket.Receive(responseBytes);
                    var responseMessage = Encoding.ASCII.GetString(responseBytes, 0, bytesRead);

                    return new JsonResult(responseMessage);
                }
            }
            catch (Exception ex)
            {
                ExceptionStatusCodeHelper.SetStatusCode(HttpContext, ex);
                _logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}
