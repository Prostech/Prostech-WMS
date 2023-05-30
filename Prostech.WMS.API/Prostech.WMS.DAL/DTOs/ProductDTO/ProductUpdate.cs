using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prostech.WMS.DAL.DTOs.ProductDTO
{
    public class ProductUpdate
    {
        public Guid GUID { get; set; } 
        public string ProductName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int BrandId { get; set; } = 0;
        public int CategoryId { get; set; } = 0;
        public int ProductItemStatusId { get; set; } = 0;
        public int ModifiedBy { get; set; } = 0;
        public decimal Price { get; set; } = 0;
    }
}
