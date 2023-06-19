using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Npgsql;
using Prostech.WMS.API.Models;
using Prostech.WMS.BLL.Helpers.ExceptionStatusCode;
using Prostech.WMS.BLL.Helpers.Time;
using Prostech.WMS.BLL.Interface;
using Prostech.WMS.DAL.DTOs.UserAccount;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Diagnostics;
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

        [HttpPost("cobot1")]
        public async Task<IActionResult> Cobot1([FromBody] ReceiveEvent events)
        {
            string token = new string("");
            try
            {
                _logger.LogInformation("Receive event successfully || " + TimeHelper.CurrentTime.ToString());

                if (events.CallerName == "Cobot1")
                {
                    if (events.TaskType == "1")
                    {
                        _logger.LogInformation("Cobot 1 ||" + TimeHelper.CurrentTime.ToString());
                        await genAgvSchedulingTask(null, "A", "B ", null);
                    }
                };

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

        [HttpPost("cobot2")]
        public async Task<IActionResult> Cobot2([FromBody] ReceiveEvent events)
        {
            string token = new string("");
            try
            {
                _logger.LogInformation("Receive event successfully || " + TimeHelper.CurrentTime.ToString());

                if (events.CallerName == "Cobot2")
                {
                    if (events.TaskType == "1")
                    {
                        _logger.LogInformation("Cobot 2 ||" + TimeHelper.CurrentTime.ToString());
                        await genAgvSchedulingTask(null, "A", "B ", null);
                    }
                }

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

        [HttpPost("cobot3")]
        public async Task<IActionResult> Cobot3([FromBody] ReceiveEvent events)
        {
            string token = new string("");
            try
            {
                _logger.LogInformation("Receive event successfully || " + TimeHelper.CurrentTime.ToString());
                if (events.CallerName == "Cobot3")
                {
                    if (events.TaskType == "1")
                    {
                        _logger.LogInformation("Cobot 3 ||" + TimeHelper.CurrentTime.ToString());
                        await genAgvSchedulingTask(null, "A", "B ", null);
                    }
                }

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

        private async Task<AGVReturnMessage> genAgvSchedulingTask(string Pod, string FromPosition, string ToPosition, string taskTyp)
        {
            try
            {
                AGVReturnMessage result = new AGVReturnMessage();

                using (var client = new HttpClient()) // Create an HTTP client to make API call
                {
                    Uri endpoint = new Uri($"http://{_appSettings.RCSUrl}/rcms/services/rest/hikRpcService/genAgvSchedulingTask"); // API endpoint URL

                    var paramObj = new // Create an anonymous object to hold request parameters
                    {
                        reqCode = GenerateRandomString(), // Request code
                        taskTyp = taskTyp,
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

        [HttpPost("AGVCallBackCobot")]
        public async Task<IActionResult> AGVCallbackCobot([FromBody] AGVCallBack req)
        {
            try
            {
                bool res = false;
                _logger.LogInformation(req.method);
                _logger.LogInformation("Receive call back");
                if (req.method == "end")
                {
                    res = await SendMessageToClient(req.method);
                }

                if(res)
                {
                    ReceiveEvent events = new ReceiveEvent
                    {
                        CallerName = "Cobot1",
                        TaskType = "1"
                    };

                    await Cobot1(events);
                }
                return new JsonResult(new
                {
                    code = "0",
                    message = "Rozitek's software received. Thank you !!!",
                    reqCode = req.reqCode.ToString(),
                    data = ""
                });
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                ExceptionStatusCodeHelper.SetStatusCode(HttpContext, ex);
                throw new Exception(ex.Message);
            }
        }


        [HttpPost("send-cobot-elite-task")]
        public async Task<bool> SendMessageToClient([FromBody] string message)
        {
            try
            {
                byte[] data = Encoding.UTF8.GetBytes(message);

                string result = await SocketHelper.SendAsync(data, 1024, _logger);
                return true;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                ExceptionStatusCodeHelper.SetStatusCode(HttpContext, ex);
                return false;
            }
        }

        [HttpPost("open-socket")]
        public async Task<ActionResult> OpenSocket()
        {
            try
            {
                await SocketHelper.OpenSocket("10.61.3.66", 4341);
                return new JsonResult("Open socket successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                ExceptionStatusCodeHelper.SetStatusCode(HttpContext, ex);
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("close-socket")]
        public async Task<ActionResult> CloseSocket()
        {
            try
            {
                SocketHelper.CloseSocket();
                return new JsonResult("Close socket successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                ExceptionStatusCodeHelper.SetStatusCode(HttpContext, ex);
                throw new Exception(ex.Message);
            }
        }

        private string FindPodAtArea(string area)
        {
            try
            {
                string query = @"SELECT tmd.pod_code
                        FROM tcs_map_data tmd
                        LEFT JOIN tcs_main_task ttt ON tmd.pod_code = ttt.pod_code AND ttt.task_status IN ('1', '2', '3')
                        WHERE tmd.area_code = @p_area
                        AND tmd.pod_code IS NOT NULL
                        AND tmd.pod_code <> ''
                        AND ttt.pod_code IS NULL
                        LIMIT 1;";

                DataTable table = new DataTable();
                string sqlDataSource = _appSettings.DbConnection;
                NpgsqlDataReader myReader;
                using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                    {
                        myCommand.Parameters.AddWithValue("@p_area", area);

                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);

                        myReader.Close();
                        myCon.Close();
                    }
                }

                // Convert DataTable to List<TCSPodResult>
                string result = string.Empty;
                foreach (DataRow row in table.Rows)
                {
                    result = row["pod_code"].ToString();
                    break; // Only need the first row
                }

                return result;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return string.Empty; // Handle the exception gracefully by returning an appropriate value
            }
        }

        private async Task<int> CountTaskByIsCreatedAsync(string Position)
        {
            try
            {
                // SQL query to call stored function
                string query = $@"SELECT count(*)
                                FROM tcs_trans_task
                                WHERE task_status IN ('1', '2', '3')
                                    AND via like '%{Position}%';
                                ";

                // variable to hold the value of the quantity property
                int quantity = 0;

                // Connection string for database
                string sqlDataSource = _appSettings.DbConnection;

                // Create and open database connection
                using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
                {
                    await myCon.OpenAsync();

                    // Create and execute database command
                    using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                    {
                        // Add parameter for task status

                        // Add parameter for wb codes

                        // Execute query and read result into data reader
                        NpgsqlDataReader myReader = await myCommand.ExecuteReaderAsync();

                        // Check if there is a row in the result set
                        if (myReader.Read())
                        {
                            // Extract the value of the quantity property from the first column
                            quantity = myReader.GetInt32(0);
                        }

                        // Close data reader
                        myReader.Close();

                        // Close database connection
                        myCon.Close();
                    }
                }

                // Return quantity as result
                return quantity;
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
