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
    public class BrandRepository : IBrandRepository
    {
        private IWMSGenericRepository<Brand> _wmsRepository;

        public BrandRepository(IWMSGenericRepository<Brand> wmsRepository)
        {
            _wmsRepository = wmsRepository;
        }

        public Brand GetBrandById(int brandId)
        {
            try
            {
                return _wmsRepository.Table
                     .Where(_ => _.BrandId == brandId)
                     .FirstOrDefault();
            }
            catch
            {
                throw new Exception("Brand does not exists");
            }
        }
    }
}
