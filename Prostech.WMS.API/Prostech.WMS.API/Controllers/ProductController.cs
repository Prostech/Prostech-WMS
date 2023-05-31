using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Prostech.WMS.BLL.Helpers.ExceptionStatusCode;
using Prostech.WMS.BLL.Helpers.ValueChecker;
using Prostech.WMS.BLL.Interface;
using Prostech.WMS.DAL.DTOs.ProductDTO;
using Prostech.WMS.DAL.DTOs.ProductItemDTO;
using Prostech.WMS.DAL.Models;
using System.ComponentModel.DataAnnotations;

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
        public async Task<IActionResult> GetProductsAsync([FromQuery] ProductCriteria request)
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
                ExceptionStatusCodeHelper.SetStatusCode(HttpContext, ex);
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
                ExceptionStatusCodeHelper.SetStatusCode(HttpContext, ex);
                return new JsonResult(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddProductAsync([FromBody] [Required] ProductPost request)
        {
            try
            {
                return new JsonResult(await _productService.AddProductAsync(request));
            }
            catch (Exception ex)
            {
                ExceptionStatusCodeHelper.SetStatusCode(HttpContext, ex);
                return new JsonResult(ex.Message);
            }
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateProductAsync([FromBody] ProductUpdate request)
        {
            if (ValueCheckerHelper.IsNull(request.GUID) || ValueCheckerHelper.IsNullOrZero(request.ModifiedBy))
            {
                throw new IOException("Please input GUID and ModifiedBy");
            }
            try
            {
                return new JsonResult(await _productService.UpdateProductAsync(request));
            }
            catch (Exception ex)
            {
                ExceptionStatusCodeHelper.SetStatusCode(HttpContext, ex);
                return new JsonResult(ex.Message);
            }
        }

        [HttpDelete("guid")]
        public async Task<IActionResult> DeleteProductAsync([FromQuery] [Required] Guid guid)
        {
            try
            {
                return new JsonResult(await _productService.DeleteProductAsync(guid));
            }
            catch (Exception ex)
            {
                ExceptionStatusCodeHelper.SetStatusCode(HttpContext, ex);
                return new JsonResult(ex.Message);
            }
        }
    }
}
