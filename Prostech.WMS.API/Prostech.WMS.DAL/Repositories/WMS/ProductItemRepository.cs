using Microsoft.EntityFrameworkCore;
using Prostech.WMS.DAL.Models;
using Prostech.WMS.DAL.Repositories.WMS.Base;
using Prostech.WMS.DAL.Repositories.WMS.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prostech.WMS.DAL.Repositories.WMS
{
    public class ProductItemRepository : IProductItemRepository
    {
        private IWMSGenericRepository<ProductItem> _wmsRepository;

        public ProductItemRepository(IWMSGenericRepository<ProductItem> wmsRepository)
        {
            _wmsRepository = wmsRepository;
        }

        public async Task<List<ProductItem>> GetProductItemsListAsync()
        {
            return await _wmsRepository.Table
                .Include(pi => pi.Product)
                .ThenInclude(p => p.Brand)
                .Include(pi => pi.Product)
                .ThenInclude(p => p.Category)
                .Include(pi => pi.ProductItemStatus)
                .OrderBy(pi => pi.SKU)
                .ToListAsync();
        }

        public List<ProductItem> GetProductItemsByProductIdAsync(int productId)
        {
            return _wmsRepository.Table
                .Where(_ => _.ProductId == productId && _.IsActive == true)
                .ToList();
        }
    }
}
