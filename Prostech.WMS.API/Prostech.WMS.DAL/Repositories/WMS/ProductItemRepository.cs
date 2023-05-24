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
                .OrderBy(pi => pi.SKU)
                .Select(pi => new ProductItem
                {
                    SKU = pi.SKU,
                    ProductId = pi.ProductId,
                    Price = pi.Price,
                    Product = new Product
                    {
                        ProductName = pi.Product.ProductName,
                        Description = pi.Product.Description,
                        BrandId = pi.Product.BrandId,
                        Brand = new Brand
                        {
                            BrandId = pi.Product.BrandId,
                            BrandName = pi.Product.Brand.BrandName,
                        },
                        Category = new Category
                        {
                            CategoryId = pi.Product.CategoryId,
                            CategoryName = pi.Product.Category.CategoryName
                        }
                    }
                }).ToListAsync();
        }
    }
}
