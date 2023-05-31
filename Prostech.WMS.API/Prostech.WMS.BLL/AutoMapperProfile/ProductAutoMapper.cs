using Prostech.WMS.DAL.DTOs.ProductDTO;
using Prostech.WMS.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Prostech.WMS.BLL.AutoMapperProfile
{
    public class ProductAutoMapper : Profile
    {
        public ProductAutoMapper() 
        {
            CreateMap<Product, ProductResponse>().ReverseMap();
        }
    }
}
