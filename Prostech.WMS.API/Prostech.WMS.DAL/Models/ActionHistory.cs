using Prostech.WMS.DAL.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prostech.WMS.DAL.Models
{
    public class ActionHistory : BaseEntity
    {
        public ActionHistory() 
        {
            ActionHistoryDetails = new HashSet<ActionHistoryDetail>();
        }
        public int ActionHistoryId { get; set; }
        public int ActionTypeId { get; set; }
        public ActionType ActionType { get; set; }
        public virtual ICollection<ActionHistoryDetail> ActionHistoryDetails { get; set; }
    }
}
