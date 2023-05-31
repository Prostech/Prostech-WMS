using Prostech.WMS.BLL.Helpers.ValueChecker;
using Prostech.WMS.BLL.Interface;
using Prostech.WMS.DAL.DTOs.UserAccount;
using Prostech.WMS.DAL.Models;
using Prostech.WMS.DAL.Repositories.WMS.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prostech.WMS.BLL
{
    public class UserAccountService : IUserAccountService
    {
        private readonly IUserAccountRepository _userAccountRepository;

        public UserAccountService(IUserAccountRepository userAccountRepository)
        {
            _userAccountRepository = userAccountRepository;
        }

        public async Task<UserAccountResponse> GetUserAccountByGUID (Guid guid)
        {
            UserAccount userAccount = await _userAccountRepository.GetUserAccountByGUID (guid);

            if(ValueCheckerHelper.IsNull(userAccount))
            {
                throw new NullReferenceException("UserAccount not found");
            }

            UserAccountResponse result = new UserAccountResponse
            {
                GUID = userAccount.GUID,
            };

            return result;
        }
    }
}
