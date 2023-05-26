using Prostech.WMS.DAL.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prostech.WMS.DAL.Models
{
    public class ProductItem : BaseEntity
    {
        public ProductItem() 
        {
            ActionHistoryDetails = new HashSet<ActionHistoryDetail>();
        }
        public int SKU { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public decimal Price { get; set; }
        public bool IsStock { get; set; }
        public DateTime? LatestInboundTime { get; set; }
        public DateTime? LatestOutboundTime { get; set; }
        public int ProductItemStatusId { get; set; }
        public ProductItemStatus ProductItemStatus { get; set; }
        public virtual ICollection<ActionHistoryDetail> ActionHistoryDetails { get; }

    }
}
