﻿using Prostech.WMS.DAL.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prostech.WMS.DAL.Models
{
    public class ProductItem : BaseEntity
    {
        public int SKU { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public decimal Price { get; set; }
        public bool IsStock { get; set; }
        public DateTime? InboundDate { get; set; }
        public DateTime? OutboundDate { get; set; }
        public DateTime? CreatedTime { get; set; }
        public int? CreatedBy { get; set; } = 1;
        public DateTime? ModifiedTime { get; set;}
        public int? ModifiedBy { get; set; }
        public int ProductItemStatusId { get; set; }
        public ProductItemStatus ProductItemStatus { get; set; }
    }
}
