using Microsoft.AspNetCore.Mvc;
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
    public class ProductRepository : IProductRepository
    {
        private IWMSGenericRepository<Product> _wmsRepository;

        public ProductRepository(IWMSGenericRepository<Product> wmsRepository)
        {
            _wmsRepository = wmsRepository;
        }

        public async Task<List<Product>> GetProductsListAsync()
        {
            return await _wmsRepository.Table
                .Include(_ => _.Brand)
                .Include(_ => _.Category)
                .Include(_ => _.ProductItems)
                .OrderBy(_ => _.ProductId)
                .Where(_ => _.IsActive == true)
                .ToListAsync();
        }

        public async Task<Product> GetProductByGUIDAsync(Guid guid)
        {
            return await _wmsRepository.Table
                .Include(_ => _.Brand)
                .Include(_ => _.Category)
                .Include(_ => _.ProductItems)
                .Where(_ => _.GUID == guid && _.IsActive == true)
                .FirstOrDefaultAsync();
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            return await _wmsRepository.InsertAsync(product);
        }

        public async void UpdateProductAsync(Product product)
        {
            await _wmsRepository.UpdateAsync(product);
        }
    }
}
