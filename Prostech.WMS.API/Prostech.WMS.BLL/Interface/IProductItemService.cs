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
    public interface IProductItemService
    {
        Task<List<ProductItemResponse>> GetProductItemsListAsync(ProductItemCriteria request);
    }
}
