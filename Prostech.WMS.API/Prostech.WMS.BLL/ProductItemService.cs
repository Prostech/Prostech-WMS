using Prostech.WMS.BLL.Interface;
using Prostech.WMS.DAL.DTOs.ProductItemDTO;
using Prostech.WMS.DAL.Models;
using Prostech.WMS.DAL.Repositories.WMS.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prostech.WMS.BLL
{
    public class ProductItemService : IProductItemService
    {
        private IProductItemRepository _productItemService;

        public ProductItemService(IProductItemRepository productItemService)
        {
            _productItemService = productItemService;
        }

        public async Task<List<ProductItemCriteriaDTO>> GetProductItemsListAsync()
        {
            List<ProductItem> productItems = new List<ProductItem>();

            productItems = await _productItemService.GetProductItemsListAsync();

            List<ProductItemCriteriaDTO> result = productItems.Select(pi => new ProductItemCriteriaDTO
            {
                SKU = pi.SKU,
                ProductId = pi.ProductId,
                ProductName = pi.Product.ProductName,
                BrandId = pi.Product.BrandId,
                BrandName = pi.Product.Brand.BrandName,
                CategoryId = pi.Product.CategoryId,
                CategoryName = pi.Product.Category.CategoryName,
                Price = pi.Price,
                IsStock = pi.IsStock,
                CreatedTime = pi.CreatedTime,
                CreatedBy = pi.CreatedBy,
                ModifiedTime = pi.ModifiedTime,
                ModifiedBy = pi.ModifiedBy,
            }).ToList();

            return result;
        }
    }
}
