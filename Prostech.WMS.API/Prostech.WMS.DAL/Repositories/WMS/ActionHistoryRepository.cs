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
    }
}
