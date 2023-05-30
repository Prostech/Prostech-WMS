using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prostech.WMS.DAL.DTOs.ProductDTO
{
    public class ProductPost
    {
        public string ProductName { get; set; }
        public string Description { get; set; }
        public int BrandId { get; set; }
        public int CategoryId { get; set; }
        public int CreatedBy { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int ProductItemStatusId { get; set; }
    }
}
