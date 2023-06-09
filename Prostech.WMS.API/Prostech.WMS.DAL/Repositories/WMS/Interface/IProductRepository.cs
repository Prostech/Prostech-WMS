﻿using Microsoft.AspNetCore.Mvc;
using Prostech.WMS.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prostech.WMS.DAL.Repositories.WMS.Interface
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProductsListAsync();
        Task<Product> GetProductByGUIDAsync(Guid guid);
        Task<Product> CreateProductAsync(Product product);
        Task UpdateProductAsync(Product product);
    }
}
