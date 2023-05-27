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
    public class BrandRepository : IBrandrepository
    {
        private IWMSGenericRepository<Brand> _wmsRepository;

        public BrandRepository(IWMSGenericRepository<Brand> wmsRepository)
        {
            _wmsRepository = wmsRepository;
        }

        public string GetBrandNameByIdAsync(int brandId)
        {
            return _wmsRepository.Table
                                       .Where(_ => _.BrandId == brandId)
                                       .Select(_ => _.BrandName)
                                       .FirstOrDefault();
        }
    }
}
