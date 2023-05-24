using Prostech.WMS.BLL.Interface;
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

        public async Task<List<ProductItem>> GetProductItemsListAsync()
        {
            return await _productItemService.GetProductItemsListAsync();
        }
    }
}
