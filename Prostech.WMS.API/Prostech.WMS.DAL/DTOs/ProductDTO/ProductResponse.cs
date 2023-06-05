using Prostech.WMS.DAL.Base;
using Prostech.WMS.DAL.DTOs.ProductItemDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prostech.WMS.DAL.DTOs.ProductDTO
{
    public class ProductResponse : BaseEntity
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int BrandId { get; set; }
        public string BrandName { get; set;}
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public Guid GUID { get; set; }
        public int ActionHistoryId { get; set; }
        public int? ProductItemStatusId { get; set; }
        public string ProductItemStatusName { get; set; }
        public decimal? Price { get; set; }
        public List<ProductItemResponse> ProductItems { get; set; }
    }
}
