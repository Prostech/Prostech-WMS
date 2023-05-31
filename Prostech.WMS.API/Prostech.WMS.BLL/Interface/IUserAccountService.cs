using Prostech.WMS.DAL.DTOs.ProductItemDTO;
using Prostech.WMS.DAL.DTOs.UserAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prostech.WMS.BLL.Interface
{
    public interface IUserAccountService
    {
        Task<UserAccountResponse> GetUserAccountByGUID(Guid guid);
    }
}
