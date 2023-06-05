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
    public class ActionHistoryRepository : IActionHistoryRepository
    {
        private IWMSGenericRepository<ActionHistory> _wmsRepository;

        public ActionHistoryRepository(IWMSGenericRepository<ActionHistory> wmsRepository)
        {
            _wmsRepository = wmsRepository;
        }

        public async Task<ActionHistory> AddActionHistoryAsync(ActionHistory actionHistory)
        {
            return await _wmsRepository.InsertAsync(actionHistory);
        }

        public async Task<List<ActionHistory>> GetActionHistoriesListAsync()
        {
            return await _wmsRepository.Table
                .Include(_ => _.ActionType)
                .Include(_ => _.ActionHistoryDetails)
                .ThenInclude(_ => _.ProductItem)
                .ThenInclude(_ => _.Product)
                .ThenInclude(_ => _.Brand)
                .Include(_ => _.ActionHistoryDetails)
                .ThenInclude(_ => _.ProductItem)
                .ThenInclude(_ => _.Product)
                .ThenInclude(_ => _.Category)
                .Include(_ => _.ActionHistoryDetails)
                .ThenInclude(_ => _.ProductItem)
                .ThenInclude(_ => _.ProductItemStatus)
                .Where(_ => _.IsActive == true)
                .OrderByDescending(_ => _.CreatedTime)
                .ToListAsync();
        }

        public async Task<List<ActionHistory>> GetActionHistoriesByProductGUIDAsync(Guid[] guids)
        {
            return await _wmsRepository.Table
                .Include(_ => _.ActionType)
                .Include(_ => _.ActionHistoryDetails)
                .ThenInclude(_ => _.ProductItem)
                .ThenInclude(_ => _.Product)
                .ThenInclude(_ => _.Brand)
                .Include(_ => _.ActionHistoryDetails)
                .ThenInclude(_ => _.ProductItem)
                .ThenInclude(_ => _.Product)
                .ThenInclude(_ => _.Category)
                .Include(_ => _.ActionHistoryDetails)
                .ThenInclude(_ => _.ProductItem)
                .ThenInclude(_ => _.ProductItemStatus)
                .Where(a => a.IsActive && a.ActionHistoryDetails.Any(d => guids.Contains(d.ProductItem.Product.GUID)))
                .OrderByDescending(_ => _.CreatedTime)
                .ToListAsync();
        }
    }
}
