using Microsoft.EntityFrameworkCore;
using Prostech.WMS.DAL.Models;
using Prostech.WMS.DAL.Repositories.WMS.Base;
using Prostech.WMS.DAL.Repositories.WMS.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prostech.WMS.DAL.Repositories.WMS
{
    public class UserAccountRepository : IUserAccountRepository
    {
        private IWMSGenericRepository<UserAccount> _wmsRepository;

        public UserAccountRepository(IWMSGenericRepository<UserAccount> wmsRepository)
        {
            _wmsRepository = wmsRepository;
        }

        public async Task<UserAccount> GetUserAccountByUsernameAndPassword(string username, string password)
        {
            return await _wmsRepository.Table
                                 .Where(_ => _.UserName == username && _.Password == password && _.IsActive == true)
                                 .FirstOrDefaultAsync();
        }

        public async Task<UserAccount> GetUserAccountByGUID(Guid guid)
        {
            return await _wmsRepository.Table
                                 .Where(_ => _.GUID == guid && _.IsActive == true)
                                 .FirstOrDefaultAsync();
        }
    }
}
