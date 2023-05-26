using Microsoft.AspNetCore.Mvc;
using Prostech.WMS.BLL.Helpers.ValueChecker;
using Prostech.WMS.BLL.Interface;
using Prostech.WMS.DAL.DTOs.ProductDTO;
using Prostech.WMS.DAL.DTOs.ProductItemDTO;
using Prostech.WMS.DAL.Models;

namespace Prostech.WMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsAsync([FromQuery] ProductRequest request)
        {
            List<ProductResponse> products = new List<ProductResponse>();
            try
            {
                products = await _productService.GetProductsListAsync(request);

                if(ValueCheckerHelper.IsNullOrEmpty(products))
                {
                    HttpContext.Response.StatusCode = 400;
                    return new JsonResult("There is no product");
                }

                return new JsonResult(products);
            }
            catch (Exception ex)
            {
                HttpContext.Response.StatusCode = 500;
                return new JsonResult(ex.Message);
            }
        }

        [HttpGet("guid")]
        public async Task<IActionResult> GetProductByGUIDAsync([FromQuery] Guid guid)
        {
            ProductResponse product = new ProductResponse();
            try
            {
                product = await _productService.GetProductByGUIDAsync(guid);

                if (ValueCheckerHelper.IsNull(product))
                {
                    HttpContext.Response.StatusCode = 400;
                    return new JsonResult("There is no product");
                }

                return new JsonResult(product);
            }
            catch (Exception ex)
            {
                if(ex is System.NullReferenceException)
                {
                    HttpContext.Response.StatusCode = 500;
                    return new JsonResult("There is no product by the guid");
                }
                HttpContext.Response.StatusCode = 500;
                return new JsonResult(ex.Message);
            }
        }
    }
}
