using Microsoft.AspNetCore.Mvc;
using Prostech.WMS.BLL.Helpers.ValueChecker;
using Prostech.WMS.BLL.Interface;
using Prostech.WMS.DAL.DTOs.ProductItemDTO;
using Prostech.WMS.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace Prostech.WMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductItemController : ControllerBase
    {
        private readonly IProductItemService _productItemService;

        public ProductItemController(IProductItemService productItemService)
        {
            _productItemService = productItemService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductItemsAsync([FromQuery] ProductItemRequest request)
        {
            List<ProductItemResponse> productItem = new List<ProductItemResponse>();
            try
            {
                productItem = await _productItemService.GetProductItemsListAsync(request);

                if(ValueCheckerHelper.IsNullOrEmpty(productItem))
                {
                    HttpContext.Response.StatusCode = 400;
                    return new JsonResult("There is no product item");
                }

                return new JsonResult(productItem);
            }
            catch (Exception ex)
            {
                HttpContext.Response.StatusCode = 500;
                return new JsonResult(ex.Message);
            }
        }
    }
}
