using Prostech.WMS.DAL.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prostech.WMS.DAL.Models
{
    public class Brand : BaseEntity
    {
        public Brand()
        {
            Products = new HashSet<Product>();
        }
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public virtual ICollection<Product> Products { get; set; }

    }
}
