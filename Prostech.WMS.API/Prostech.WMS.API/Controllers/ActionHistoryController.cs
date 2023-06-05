using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Prostech.WMS.API.Models;
using Prostech.WMS.BLL.Helpers.ExceptionStatusCode;
using Prostech.WMS.BLL.Helpers.ValueChecker;
using Prostech.WMS.BLL.Interface;
using Prostech.WMS.DAL.DBContext;
using Prostech.WMS.DAL.DTOs.ActionHistoryDTO;
using Prostech.WMS.DAL.DTOs.UserAccount;
using System.ComponentModel.DataAnnotations;

namespace Prostech.WMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActionHistoryController : ControllerBase
    {
        private readonly IActionHistoryService _actionHistoryService;
        public ActionHistoryController(IActionHistoryService actionHistoryService)
        {
            _actionHistoryService = actionHistoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetActionHistoriesListAsync([FromQuery] ActionHistoryCriteria request)
        {
            try
            {
                List<ActionHistoryResponse> actionHistoryResponse = await _actionHistoryService.GetActionHistoriesListAsync(request.Page, request.PageSize);

                return new JsonResult(actionHistoryResponse);
            }
            catch (Exception ex)
            {
                ExceptionStatusCodeHelper.SetStatusCode(HttpContext, ex);
                return new JsonResult(ex.Message);
            }
        }

        [HttpPost("stock-movement")]
        public async Task<IActionResult> StockMovement([FromBody] [Required] ActionHistoryPost request)
        {
            try
            {
                if (!Enum.IsDefined(typeof(ActionTypeEnum), request.ActionTypeId))
                    throw new BadHttpRequestException("The action is not exist");

                if(ValueCheckerHelper.IsNullOrEmpty(request.Products))
                    throw new BadHttpRequestException("The products is null so that can not update");

                return new JsonResult(await _actionHistoryService.StockMovement(request));
            }
            catch (Exception ex)
            {
                ExceptionStatusCodeHelper.SetStatusCode(HttpContext, ex);
                return new JsonResult(ex.Message);
            }
        }
    }
}
