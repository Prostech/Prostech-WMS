using Prostech.WMS.DAL.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prostech.WMS.DAL.Models
{
    public class ActionHistoryDetail : BaseEntity
    {
        public int SKU { get; set; }
        public ProductItem ProductItem { get; set; }
        public int ActionHistoryId { get; set; }
        public ActionHistory ActionHistory { get; set; }
    }
}
