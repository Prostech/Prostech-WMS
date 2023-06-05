using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prostech.WMS.DAL.DTOs.ActionHistoryDTO
{
    public class ActionHistoryPost
    {
        public List<ProductEntry> Products { get; set; }
        public int ActionTypeId { get; set; }
    }

    public class ProductEntry
    {
        public Guid GUID { get; set; }
        public int Quantity { get; set; }
    }
}
