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
    public class ProductItemStatusRepository : IProductItemStatusRepository
    {
        private IWMSGenericRepository<ProductItemStatus> _wmsRepository;

        public ProductItemStatusRepository(IWMSGenericRepository<ProductItemStatus> wmsRepository)
        {
            _wmsRepository = wmsRepository;
        }

        public string GetProductItemStatusById(int productItemStatusId)
        {
            return _wmsRepository.Table
                                 .Where(_ => _.ProductItemStatusId == productItemStatusId)
                                 .Select(_ => _.ProductItemStatusName)
                                 .FirstOrDefault();
        }
    }
}
