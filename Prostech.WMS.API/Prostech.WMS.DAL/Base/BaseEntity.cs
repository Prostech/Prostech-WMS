using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prostech.WMS.DAL.Base
{
    /// <summary>
    /// Represents the base class for entities
    /// </summary>
    public abstract partial class BaseEntity
    {
        public bool IsActive { get; set; }
        public DateTime? CreatedTime { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedTime { get; set; }
        public int? ModifiedBy { get; set; }
        public Guid GUID { get; set; }
    }
}
