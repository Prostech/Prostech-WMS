using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Prostech.WMS.DAL.DBContext
{
    public enum ActionTypeEnum
    {
        [EnumMember(Value = "Inbound")]
        Inbound = 1,
        [EnumMember(Value = "Outbound")]
        Outbound = 2
    }
}
