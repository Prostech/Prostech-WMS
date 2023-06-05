using Prostech.WMS.DAL.DTOs.ActionHistoryDTO;
using Prostech.WMS.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prostech.WMS.BLL.Interface
{
    public interface IActionHistoryService
    {
        Task<List<ActionHistoryResponse>> GetActionHistoriesListAsync(int page, int paageSize);
        Task<ActionHistoryResponse> StockMovement(ActionHistoryPost actionHistoryPost);
        Task<ActionHistoryResponse> AddActionHistoryAsync(int createdBy, int actionTypeId);
    }
}
