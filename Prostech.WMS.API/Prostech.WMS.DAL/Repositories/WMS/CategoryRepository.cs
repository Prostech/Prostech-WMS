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
    public class CategoryRepository : ICategoryRepository
    {
        private IWMSGenericRepository<Category> _wmsRepository;

        public CategoryRepository(IWMSGenericRepository<Category> wmsRepository)
        {
            _wmsRepository = wmsRepository;
        }

        public Category GetcategoryById(int categoryId)
        {
            try
            {
                return _wmsRepository.Table
                     .Where(_ => _.CategoryId == categoryId)
                     .FirstOrDefault();
            }
            catch
            {
                throw new NullReferenceException("Category does not exists");
            }
        }
    }
}
