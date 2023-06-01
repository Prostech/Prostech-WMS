using AutoMapper;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Prostech.WMS.BLL.Helpers.Wrapper;
using Prostech.WMS.BLL.Interface;
using Prostech.WMS.DAL.DTOs.ActionHistoryDTO;
using Prostech.WMS.DAL.DTOs.ProductDTO;
using Prostech.WMS.DAL.DTOs.ProductItemDTO;
using Prostech.WMS.DAL.Models;
using Prostech.WMS.DAL.Repositories.WMS;
using Prostech.WMS.DAL.Repositories.WMS.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prostech.WMS.BLL
{
    public class ActionHistoryService : IActionHistoryService
    {
        private readonly IActionHistoryRepository _actionHistoryRepository;
        private readonly IMapper _mapper;

        public ActionHistoryService(IActionHistoryRepository actionHistoryRepository, IMapper mapper)
        {
            _actionHistoryRepository = actionHistoryRepository;
            _mapper = mapper;
        }

        public async Task<List<ActionHistoryResponse>> GetActionHistoriesListAsync(int page, int paageSize)
        {
            List<ActionHistory> actionHistory = await _actionHistoryRepository.GetActionHistoriesListAsync();


            List<ActionHistoryResponse> result = _mapper.Map<List<ActionHistory>, List<ActionHistoryResponse>>(actionHistory);

            return result;
        }
    }
}
