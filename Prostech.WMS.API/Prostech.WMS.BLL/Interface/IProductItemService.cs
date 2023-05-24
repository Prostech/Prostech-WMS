using Prostech.WMS.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prostech.WMS.BLL.Interface
{
    public interface IProductItemService
    {
        Task<List<ProductItem>> GetProductItemsListAsync();
    }
}
