using Microsoft.AspNetCore.Mvc;
using Prostech.WMS.DAL.DTOs.ProductDTO;
using Prostech.WMS.DAL.DTOs.ProductItemDTO;
using Prostech.WMS.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prostech.WMS.BLL.Interface
{
    public interface IProductService
    {
        Task<List<ProductResponse>> GetProductsListAsync(ProductRequest request);
        Task<ProductResponse> GetProductByGUIDAsync(Guid guid);
        Task<ProductResponse> AddProductAsync(ProductPost request);
        Task<ProductResponse> UpdateProductAsync(ProductUpdate request);
    }
}
