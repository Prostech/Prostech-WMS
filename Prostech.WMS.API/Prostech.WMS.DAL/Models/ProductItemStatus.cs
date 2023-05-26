using Prostech.WMS.DAL.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prostech.WMS.DAL.Models
{
    public class ProductItemStatus : BaseEntity
    {
        public ProductItemStatus()
        {
            ProductItems = new HashSet<ProductItem>();
        }
        public int ProductItemStatusId { get; set; }
        public string ProductItemStatusName { get; set; }
        public virtual ICollection<ProductItem> ProductItems { get; set; }
    }
}
