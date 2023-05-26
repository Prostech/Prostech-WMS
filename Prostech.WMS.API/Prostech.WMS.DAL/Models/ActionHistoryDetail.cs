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
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int ActionHistoryId { get; set; }
        public ActionHistory ActionHistory { get; set; }
    }
}
