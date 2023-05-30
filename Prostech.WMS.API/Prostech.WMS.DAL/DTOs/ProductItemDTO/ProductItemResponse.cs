using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prostech.WMS.DAL.DTOs.ProductItemDTO
{
    public class ProductItemResponse
    {
        public int SKU { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int BrandId { get; set; }
        public string BrandName { get; set;}
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public bool IsStock { get; set; }
        public DateTime? CreatedTime { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedTime { get; set; }
        public int? ModifiedBy { get; set;}
        public DateTime? LatestInboundTime { get; set; }
        public DateTime? LatestOutboundTime { get; set; }
    }
}
