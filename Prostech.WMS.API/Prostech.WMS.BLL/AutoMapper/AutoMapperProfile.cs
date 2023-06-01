using Prostech.WMS.DAL.DTOs.ProductDTO;
using Prostech.WMS.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Prostech.WMS.DAL.DTOs.ProductItemDTO;
using Prostech.WMS.DAL.Repositories.WMS.Interface;
using Prostech.WMS.DAL.DTOs.ActionHistoryDTO;
using LinqToDB.Common;
using AutoMapper.QueryableExtensions;
using Microsoft.Extensions.Configuration;
using IConfigurationProvider = AutoMapper.IConfigurationProvider;

namespace Prostech.WMS.BLL.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ActionHistory, ActionHistoryResponse>()
                .ForMember(dest => dest.ActionTypeName, opt => opt.MapFrom(src => src.ActionType.ActionName))
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.ActionHistoryDetails
                    .GroupBy(d => d.ProductItem.Product.ProductId) // Grouping by ProductId
                    .Select(group => new ProductResponse
                    {
                        ProductId = group.Key,
                        ProductName = group.First().ProductItem.Product.ProductName,
                        BrandId = group.First().ProductItem.Product.BrandId,
                        BrandName = group.First().ProductItem.Product.Brand.BrandName,
                        CategoryId = group.First().ProductItem.Product.CategoryId,
                        CategoryName = group.First().ProductItem.Product.Category.CategoryName,
                        Description = group.First().ProductItem.Product.Description,
                        Quantity = group.First().ProductItem.Product.ProductItems.Count(),
                        GUID = group.First().ProductItem.Product.GUID,
                        ActionHistoryId = src.ActionHistoryId,
                        ProductItemStatusId = group.First().ProductItem.Product.ProductItems.FirstOrDefault().ProductItemStatusId,
                        ProductItemStatusName = group.First().ProductItem.Product.ProductItems.FirstOrDefault().ProductItemStatus.ProductItemStatusName,
                        Price = group.First().ProductItem.Product.ProductItems.FirstOrDefault().Price,
                        ProductItems = group.First().ProductItem.Product.ProductItems
                            .Select(item => new ProductItemResponse
                            {
                                SKU = item.SKU,
                                ProductId = item.ProductId,
                                ProductName = item.Product.ProductName,
                                BrandId = item.Product.BrandId,
                                BrandName = item.Product.Brand.BrandName,
                                CategoryId = item.Product.CategoryId,
                                CategoryName = item.Product.Category.CategoryName,
                                IsStock = item.IsStock,
                                CreatedTime = item.CreatedTime,
                                CreatedBy = item.CreatedBy,
                                ModifiedTime = item.ModifiedTime,
                                ModifiedBy = item.ModifiedBy,
                                LatestInboundTime = item.LatestInboundTime,
                                LatestOutboundTime = item.LatestOutboundTime,
                                GUID = item.GUID,
                            })
                            .OrderBy(item => item.SKU)
                            .ToList(),
                        IsActive = group.First().ProductItem.Product.IsActive,
                        CreatedTime = group.First().ProductItem.Product.CreatedTime,
                        CreatedBy = group.First().ProductItem.Product.CreatedBy,
                        ModifiedTime = group.First().ProductItem.Product.ModifiedTime,
                        ModifiedBy = group.First().ProductItem.Product.ModifiedBy
                    }).ToList()
                ));


            CreateMap<Product, ProductResponse>()
                .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Brand.BrandName))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.CategoryName))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.ProductItems.Count(_ => _.IsStock)))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.ProductItems.FirstOrDefault().Price))
                .ForMember(dest => dest.ProductItemStatusId, opt => opt.MapFrom(src => src.ProductItems.FirstOrDefault().ProductItemStatusId))
                .ForMember(dest => dest.ProductItemStatusName, opt => opt.MapFrom(src => src.ProductItems.FirstOrDefault().ProductItemStatus.ProductItemStatusName))
                .ForMember(dest => dest.ActionHistoryId, opt => opt.MapFrom(src => src.ProductItems.FirstOrDefault().ActionHistoryDetails.FirstOrDefault().ActionHistoryId))
                .ForMember(dest => dest.ProductItems, opt => opt.MapFrom(src => src.ProductItems.Where(_ => _.IsStock)
                    .Select(_ => new ProductItemResponse
                    {
                        SKU = _.SKU,
                        ProductId = _.ProductId,
                        ProductName = _.Product.ProductName,
                        BrandId = _.Product.BrandId,
                        BrandName = _.Product.Brand.BrandName,
                        CategoryId = _.Product.CategoryId,
                        CategoryName = _.Product.Category.CategoryName,
                        IsStock = _.IsStock,
                        CreatedTime = _.CreatedTime,
                        CreatedBy = _.CreatedBy,
                        ModifiedTime = _.ModifiedTime,
                        ModifiedBy = _.ModifiedBy,
                        LatestInboundTime = _.LatestInboundTime,
                        LatestOutboundTime = _.LatestOutboundTime,
                        GUID = _.GUID,
                    })
                    .OrderBy(_ => _.SKU)
                    .ToList()
                ))
                .ReverseMap();
        }
    }
}
