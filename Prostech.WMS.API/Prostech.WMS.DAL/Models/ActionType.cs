using Prostech.WMS.DAL.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prostech.WMS.DAL.Models
{
    public class ActionType : BaseEntity
    {
        public ActionType() 
        {
            ActionHistories = new HashSet<ActionHistory>();
        }
        public int ActionTypeId { get; set; }
        public string ActionName { get; set; }
        public virtual ICollection<ActionHistory> ActionHistories { get; set; }
    }
}
