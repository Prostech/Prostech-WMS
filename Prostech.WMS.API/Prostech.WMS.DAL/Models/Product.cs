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
        }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedTime { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedTime { get; set; }
        public int? ModifiedBy { get; set; }
        public virtual ICollection<ProductItem> ProductItems { get;}
    }
}
