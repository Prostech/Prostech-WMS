using Prostech.WMS.DAL.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prostech.WMS.DAL.Models
{
    public class Category : BaseEntity
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedTime { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedTime { get; set; }
        public int? ModifiedBy { get; set; }
        public virtual ICollection<Product> Products { get; set; }

    }
}
