using Prostech.WMS.DAL.Base;
using Prostech.WMS.DAL.DTOs.ActionHistoryDetailDTO;
using Prostech.WMS.DAL.DTOs.ProductDTO;
using Prostech.WMS.DAL.DTOs.ProductItemDTO;
using Prostech.WMS.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prostech.WMS.DAL.DTOs.ActionHistoryDTO
{
    public class ActionHistoryResponse : BaseEntity
    {
        public int ActionHistoryId { get; set; }
        public int ActionTypeId { get; set; }
        public string ActionTypeName { get; set; }
        public List<ProductResponse> Products { get; set; } 
    }
}
