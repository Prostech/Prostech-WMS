using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Prostech.WMS.API.Models;
using Prostech.WMS.BLL.Helpers.ExceptionStatusCode;
using Prostech.WMS.BLL.Helpers.ValueChecker;
using Prostech.WMS.BLL.Interface;
using Prostech.WMS.DAL.DTOs.ProductItemDTO;
using Prostech.WMS.DAL.DTOs.UserAccount;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Prostech.WMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly AppSettings _appSettings;

        public UserController(IUserService userService, IConfiguration configuration, IOptions<AppSettings> appSettings)
        {
            _userService = userService;
            _configuration = configuration;
            _appSettings = appSettings.Value;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] [Required] UserAccountPost userAccount)
        {
            string token = new string("");
            try
            {
                token = await _userService.GenerateToken(userAccount.UserName, userAccount.Password);

                return new JsonResult(token);
            }
            catch (Exception ex)
            {
                ExceptionStatusCodeHelper.SetStatusCode(HttpContext, ex);
                return new JsonResult(ex.Message);
            }
        }


        [HttpPost("validate-token")]
        public async Task<IActionResult> ValidateToken([FromBody] TokenCriteria request)
        {
            try
            {
                bool result = await _userService.ValidateToken(request.Token);

                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                ExceptionStatusCodeHelper.SetStatusCode(HttpContext, ex);
                return new JsonResult(ex.Message);
            }
        }
    }
}
