using Microsoft.AspNetCore.Mvc.RazorPages;
using Prostech.WMS.BLL.Helpers.Wrapper;
using Prostech.WMS.BLL.Interface;
using Prostech.WMS.DAL.DTOs.ProductItemDTO;
using Prostech.WMS.DAL.Models;
using Prostech.WMS.DAL.Repositories.WMS;
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
        private IProductItemRepository _productItemRepository;

        public ProductItemService(IProductItemRepository productItemRepository)
        {
            _productItemRepository = productItemRepository;
        }

        public async Task<List<ProductItemResponse>> GetProductItemsListAsync(ProductItemRequest request)
        {
            List<ProductItem> productItems = new List<ProductItem>();

            productItems = await _productItemRepository.GetProductItemsListAsync();

            ServiceConstants.SetTotalRecords(productItems.Count);

            List<ProductItemResponse> result = productItems
                .Select(pi => new ProductItemResponse
                {
                    SKU = pi.SKU,
                    ProductId = pi.ProductId,
                    ProductName = pi.Product.ProductName,
                    BrandId = pi.Product.BrandId,
                    BrandName = pi.Product.Brand.BrandName,
                    CategoryId = pi.Product.CategoryId,
                    CategoryName = pi.Product.Category.CategoryName,
                    IsStock = pi.IsStock,
                    CreatedTime = pi.CreatedTime,
                    CreatedBy = pi.CreatedBy,
                    ModifiedTime = pi.ModifiedTime,
                    ModifiedBy = pi.ModifiedBy,
                    LatestInboundTime = pi.LatestInboundTime,
                    LatestOutboundTime = pi.LatestOutboundTime
                })
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            return result;
        }
    }
}
