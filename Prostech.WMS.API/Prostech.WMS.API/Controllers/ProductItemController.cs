using Microsoft.AspNetCore.Mvc;
using Prostech.WMS.BLL.Interface;
using Prostech.WMS.DAL.DTOs.ProductItemDTO;
using Prostech.WMS.DAL.Models;

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

        [HttpGet("product-item")]
        public async Task<IActionResult> GetProductItemsAsync()
        {
            List<ProductItemCriteriaDTO> productItem = new List<ProductItemCriteriaDTO>();
            try
            {
                productItem = await _productItemService.GetProductItemsListAsync();
                return new JsonResult(productItem);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(
                    new
                    {
                        Id = -1,
                        Message = ex.Message,
                    });
            }
        }
    }
}
