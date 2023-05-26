using Prostech.WMS.DAL.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prostech.WMS.DAL.Models
{
    public class Product : BaseEntity
    {
        public Product()
        { 
            ProductItems = new HashSet<ProductItem>();
            ActionHistoryDetails = new HashSet<ActionHistoryDetail>();
        }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public virtual ICollection<ProductItem> ProductItems { get;}
        public virtual ICollection<ActionHistoryDetail> ActionHistoryDetails { get;}
    }
}
