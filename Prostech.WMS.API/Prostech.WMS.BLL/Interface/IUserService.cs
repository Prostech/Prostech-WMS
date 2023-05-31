using Prostech.WMS.DAL.DTOs.ProductItemDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prostech.WMS.BLL.Interface
{
    public interface IUserService
    {
        Task<string> GenerateToken(string userName, string password);
        Task<bool> ValidateToken(string token);
    }
}
