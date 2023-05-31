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

namespace Prostech.WMS.BLL.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        private readonly IBrandRepository _brandRepository;
        public AutoMapperProfile() 
        {
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
