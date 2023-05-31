using Prostech.WMS.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prostech.WMS.DAL.Repositories.WMS.Interface
{
    public interface IBrandRepository
    {
        Brand GetBrandById(int brandId);
    }
}
