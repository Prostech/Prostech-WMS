using Prostech.WMS.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prostech.WMS.DAL.Repositories.WMS.Interface
{
    public interface IUserAccountRepository
    {
        Task<UserAccount> GetUserAccountByUsernameAndPassword(string username, string password);
        Task<UserAccount> GetUserAccountByGUID (Guid guid);
    }
}
