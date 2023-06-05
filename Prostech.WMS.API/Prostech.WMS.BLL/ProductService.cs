using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Prostech.WMS.BLL.Helpers.ValueChecker;
using Prostech.WMS.BLL.Helpers.Wrapper;
using Prostech.WMS.BLL.Interface;
using Prostech.WMS.DAL.DBContext;
using Prostech.WMS.DAL.DTOs.ActionHistoryDTO;
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
        private readonly IActionHistoryService _actionHistoryService;
        private readonly IBrandRepository _brandRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductItemStatusRepository _productItemStatusRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository,
            IActionHistoryService actionHistoryService,
            IBrandRepository brandrepository,
            ICategoryRepository categoryRepository,
            IProductItemStatusRepository productItemStatusRepository,
            IMapper mapper)
        {
            _productRepository = productRepository;
            _brandRepository = brandrepository;
            _categoryRepository = categoryRepository;
            _productItemStatusRepository = productItemStatusRepository;
            _mapper = mapper;
            _actionHistoryService = actionHistoryService;
        }

        public async Task<List<ProductResponse>> GetProductsListAsync(ProductCriteria request)
        {
            List<Product> products = new List<Product>();

            products = await _productRepository.GetProductsListAsync();

            ServiceConstants.SetTotalRecords(products.Count);

            products =  products.Skip((request.Page - 1) * request.PageSize)
                                .Take(request.PageSize).ToList();

            List<ProductResponse> result = _mapper.Map<List<ProductResponse>>(products);

            return result;
        }

        public async Task<ProductResponse> GetProductByGUIDAsync(Guid guid)
        {
            Product product = new Product();

            product = await _productRepository.GetProductByGUIDAsync(guid);

            if (ValueCheckerHelper.IsNull(product))
            {
                throw new Exception("Product not found");
            }

            ServiceConstants.SetTotalRecords(1);
            ProductResponse result = _mapper.Map<Product, ProductResponse>(product);

            return result;
        }

        public async Task<ProductResponse> AddProductAsync(ProductPost request)
        {
            ActionHistoryResponse actionHistoryAddResult = await _actionHistoryService.AddActionHistoryAsync(request.CreatedBy, (int)ActionTypeEnum.Inbound);

            Product product = new Product
            {
                ProductName = request.ProductName,
                Description = request.Description,
                BrandId = request.BrandId,
                Brand = _brandRepository.GetBrandById(request.BrandId),
                CategoryId = request.CategoryId,
                Category = _categoryRepository.GetcategoryById(request.CategoryId),
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
                        ProductItemStatus = _productItemStatusRepository.GetProductItemStatusById(request.ProductItemStatusId),
                        ActionHistoryDetails = new List<ActionHistoryDetail>
                        {
                            new ActionHistoryDetail
                            {
                                ActionHistoryId = actionHistoryAddResult.ActionHistoryId,
                                IsActive = true,
                                CreatedTime = DateTime.UtcNow,
                                CreatedBy = request.CreatedBy
                            }
                        }
                    })
                    .ToList(),
            };
            
            Product productAddResult = await _productRepository.CreateProductAsync(product);

            ProductResponse result = _mapper.Map<Product, ProductResponse>(productAddResult);

            return result;
        }

        public async Task<ProductResponse> UpdateProductAsync(ProductUpdate request)
        {
            Product existingProduct = await _productRepository.GetProductByGUIDAsync(request.GUID);

            if (ValueCheckerHelper.IsNull(existingProduct))
            {
                throw new Exception("Product not found");
            }

            existingProduct.ProductName = ValueCheckerHelper.IsNotNullOrEmpty(request.ProductName) ? request.ProductName : existingProduct.ProductName;
            existingProduct.Description = ValueCheckerHelper.IsNotNullOrEmpty(request.Description) ? request.Description : existingProduct.Description;
            existingProduct.BrandId = request.BrandId != 0 ? request.BrandId : existingProduct.BrandId;
            existingProduct.CategoryId = request.CategoryId != 0 ? request.CategoryId : existingProduct.CategoryId;
            existingProduct.ModifiedBy = request.ModifiedBy != 0 ? request.ModifiedBy : existingProduct.ModifiedBy;
            existingProduct.ModifiedTime = DateTime.UtcNow;
            existingProduct.ProductItems = existingProduct.ProductItems.Select(_ =>
            {
                _.Price = request.Price != 0 ? request.Price : _.Price;
                _.ProductItemStatusId = request.ProductItemStatusId != 0 ? request.ProductItemStatusId : _.ProductItemStatusId;
                _.ModifiedBy = request.ModifiedBy;
                _.ModifiedTime = DateTime.UtcNow;
                return _;
            }).ToList();

            await _productRepository.UpdateProductAsync(existingProduct);

            ProductResponse result = _mapper.Map<Product, ProductResponse>(existingProduct);

            return result;
        }
        public async Task<ProductResponse> DeleteProductAsync(Guid guid)
        {
            Product existingProduct = await _productRepository.GetProductByGUIDAsync(guid);

            if (ValueCheckerHelper.IsNull(existingProduct))
            {
                throw new Exception("Product not found");
            }

            existingProduct.IsActive = false;
            existingProduct.ModifiedTime = DateTime.UtcNow;
            existingProduct.ProductItems = existingProduct.ProductItems.Select(_ =>
            {
                _.IsStock = false;
                _.ModifiedTime = DateTime.UtcNow;
                return _;
            }).ToList();

            await _productRepository.UpdateProductAsync(existingProduct);

            ProductResponse result = _mapper.Map<Product, ProductResponse>(existingProduct);

            return result;
        }
    }
}
