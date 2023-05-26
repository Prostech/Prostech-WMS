using Prostech.WMS.BLL.Helpers.Wrapper;
using Prostech.WMS.BLL.Interface;
using Prostech.WMS.DAL.DTOs.ProductDTO;
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
    public class ProductService : IProductService
    {
        private IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<ProductResponse>> GetProductsListAsync(ProductRequest request)
        {
            List<Product> products = new List<Product>();

            products = await _productRepository.GetProductsListAsync();

            ServiceConstants.SetTotalRecords(products.Count);

            List<ProductResponse> result = products.Select(_ => new ProductResponse
            {
                ProductId = _.ProductId,
                ProductName = _.ProductName,
                BrandId = _.BrandId,
                BrandName = _.Brand.BrandName,
                CategoryId = _.CategoryId,
                CategoryName = _.Category.CategoryName,
                Description = _.Description,
                CreatedBy = _.CreatedBy,
                CreatedTime = _.CreatedTime,
                ModifiedBy = _.ModifiedBy,
                ModifiedTime = _.ModifiedTime,
                IsActive = _.IsActive,
                GUID = _.GUID,
                ProductItems = _.ProductItems.Select(_ => new ProductItemResponse
                {
                    SKU = _.SKU,
                    ProductId = _.ProductId,
                    ProductName = _.Product.ProductName,
                    BrandId = _.Product.BrandId,
                    BrandName = _.Product.Brand.BrandName,
                    CategoryId = _.Product.CategoryId,
                    CategoryName = _.Product.Category.CategoryName,
                    Price = _.Price,
                    IsStock = _.IsStock,
                    CreatedTime = _.CreatedTime,
                    CreatedBy = _.CreatedBy,
                    ModifiedTime = _.ModifiedTime,
                    ModifiedBy = _.ModifiedBy,
                    LatestInboundTime = _.LatestInboundTime,
                    LatestOutboundTime = _.LatestOutboundTime
                })
                .Where(_ => _.IsStock == true)
                .OrderBy(_ => _.SKU)
                .ToList(),
                Quantity = _.ProductItems.Count(_ => _.IsStock),
            })
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToList();

            return result;
        }

        public async Task<ProductResponse> GetProductByGUIDAsync(Guid guid)
        {
            Product product = new Product();

            product = await _productRepository.GetProductByGUIDAsync(guid);

            ServiceConstants.SetTotalRecords(1);

            ProductResponse result = new ProductResponse
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                BrandId = product.BrandId,
                BrandName = product.Brand.BrandName,
                CategoryId = product.CategoryId,
                CategoryName = product.Category.CategoryName,
                Description = product.Description,
                CreatedBy = product.CreatedBy,
                CreatedTime = product.CreatedTime,
                ModifiedBy = product.ModifiedBy,
                ModifiedTime = product.ModifiedTime,
                IsActive = product.IsActive,
                GUID = product.GUID,
                ProductItems = product.ProductItems.Select(_ => new ProductItemResponse {
                    SKU = _.SKU,
                    ProductId = _.ProductId,
                    ProductName = _.Product.ProductName,
                    BrandId = _.Product.BrandId,
                    BrandName = _.Product.Brand.BrandName,
                    CategoryId = _.Product.CategoryId,
                    CategoryName = _.Product.Category.CategoryName,
                    Price = _.Price,
                    IsStock = _.IsStock,
                    CreatedTime = _.CreatedTime,
                    CreatedBy = _.CreatedBy,
                    ModifiedTime = _.ModifiedTime,
                    ModifiedBy = _.ModifiedBy,
                    LatestInboundTime = _.LatestInboundTime,
                    LatestOutboundTime = _.LatestOutboundTime
                })
                .Where(_ => _.IsStock == true)
                .OrderBy(_ => _.SKU)
                .ToList(),
                Quantity = product.ProductItems.Count(_ => _.IsStock)
            };
            return result;
        }
    }
}
