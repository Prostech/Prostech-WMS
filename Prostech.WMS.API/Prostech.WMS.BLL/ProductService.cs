using Microsoft.AspNetCore.Mvc;
using Prostech.WMS.BLL.Helpers.Wrapper;
using Prostech.WMS.BLL.Interface;
using Prostech.WMS.DAL.DBContext;
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
        private readonly IProductRepository _productRepository;
        private readonly IActionHistoryRepository _actionHistoryRepository;
        private readonly IBrandrepository _brandRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductService(IProductRepository productRepository,
            IActionHistoryRepository actionHistoryRepository,
            IBrandrepository brandrepository,
            ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _actionHistoryRepository = actionHistoryRepository;
            _brandRepository = brandrepository;
            _categoryRepository = categoryRepository;
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

        public async Task<ProductResponse> CreateProductAsync(ProductPost request)
        {
            Product product = new Product
            {
                ProductName = request.ProductName,
                Description = request.Description,
                BrandId = request.BrandId,
                CategoryId = request.CategoryId,
                IsActive = true,
                CreatedBy = request.CreatedBy,
                CreatedTime = DateTime.UtcNow,
                ProductItems = Enumerable.Range(1, request.Quantity)
                    .Select(_ => new ProductItem
                    {
                        Price = request.Price,
                        IsStock = true,
                        CreatedBy = request.CreatedBy,
                        CreatedTime = DateTime.UtcNow,
                        ProductItemStatusId = request.ProductItemStatusId,
                        LatestInboundTime = DateTime.UtcNow,
                    })
                    .ToList(),
            };
            
            Product productAddResult = await _productRepository.CreateProductAsync(product);

            ActionHistory actionHistory = new ActionHistory
            {
                ActionTypeId = (int)ActionTypeEnum.Inbound,
                IsActive = true,
                CreatedTime = DateTime.UtcNow,
                CreatedBy = request.CreatedBy,
                ActionHistoryDetails = product.ProductItems.Select(_ => new ActionHistoryDetail
                {
                    SKU = _.SKU,
                    IsActive = true,
                    CreatedTime = DateTime.UtcNow,
                    CreatedBy = 1
                })
                .ToList()
            };

            ActionHistory actionHistoryAddResult = await _actionHistoryRepository.AddActionHistoryAsync(actionHistory);

            ProductResponse result = new ProductResponse
            {
                ProductId = productAddResult.ProductId,
                ProductName = productAddResult.ProductName,
                BrandId = productAddResult.BrandId,
                BrandName = _brandRepository.GetBrandNameByIdAsync(productAddResult.BrandId),
                CategoryId = productAddResult.CategoryId,
                Description = _categoryRepository.GetCategoryNameByIdAsync(productAddResult.CategoryId),
                Quantity = productAddResult.ProductItems.Count,
                GUID = productAddResult.GUID,
                ActionHistoryId = actionHistoryAddResult.ActionHistoryId,
                IsActive = productAddResult.IsActive,
                CreatedBy = productAddResult.CreatedBy,
                CreatedTime = productAddResult.CreatedTime,
                ModifiedBy = productAddResult.ModifiedBy,
                ModifiedTime = productAddResult.ModifiedTime,
                ProductItems = productAddResult.ProductItems.Select(_ => new ProductItemResponse { 
                    SKU = _.SKU,
                    ProductId = _.ProductId,
                    ProductName = productAddResult.ProductName,
                    BrandId = productAddResult.BrandId,
                    BrandName = _brandRepository.GetBrandNameByIdAsync(productAddResult.BrandId),
                    CategoryId = productAddResult.CategoryId,
                    CategoryName = _categoryRepository.GetCategoryNameByIdAsync(productAddResult.CategoryId),
                    Price = _.Price,
                    IsStock = _.IsStock,
                    CreatedTime = _.CreatedTime,
                    CreatedBy = _.CreatedBy,
                    ModifiedTime = _.ModifiedTime,
                    ModifiedBy = _.ModifiedBy,
                    LatestInboundTime = _.LatestInboundTime,
                    LatestOutboundTime = _.LatestOutboundTime,
                })
                .ToList()
            };

            return result;
        }
    }
}
