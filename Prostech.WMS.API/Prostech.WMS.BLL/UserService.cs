using LinqToDB.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Prostech.WMS.BLL.Helpers.JWT;
using Prostech.WMS.BLL.Helpers.ValueChecker;
using Prostech.WMS.BLL.Interface;
using Prostech.WMS.DAL.Models;
using Prostech.WMS.DAL.Repositories.WMS.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Prostech.WMS.BLL
{
    public class UserService : IUserService
    {
        private readonly IUserAccountRepository _userAccountRepository;
        private readonly IConfiguration _configuration;
        public UserService(IUserAccountRepository userAccountRepository, IConfiguration configuration) 
        {
            _userAccountRepository = userAccountRepository;
            _configuration = configuration;
        }

        public async Task<string> GenerateToken(string username, string password) 
        {
            UserAccount account = new UserAccount();

            account = await _userAccountRepository.GetUserAccountByUsernameAndPassword(username, password);

            if (ValueCheckerHelper.IsNull(account))
            {
                throw new NullReferenceException("Username or password is invalid");
            }

            IConfigurationSection jwtSettings = _configuration.GetSection("JwtSettings");

            string token = JwtUtility.GenerateToken(account.GUID.ToString(), "admin", jwtSettings);

            return token;
        }

        public async Task<bool> ValidateToken(string token)
        {
            try 
            {
                if (ValueCheckerHelper.IsNullOrEmpty(token))
                    throw new NullReferenceException("Token is null");

                IConfigurationSection jwtSettings = _configuration.GetSection("JwtSettings");

                ClaimsPrincipal principal = JwtUtility.ValidateToken(token, jwtSettings);

                if (ValueCheckerHelper.IsNull(principal) || !principal.Identity.IsAuthenticated)
                    throw new UnauthorizedAccessException("Token is invalid");

                Claim expirationClaim = principal.Claims.FirstOrDefault(c => c.Type == "exp");

                if (expirationClaim == null || !long.TryParse(expirationClaim.Value, out long expirationValue))
                    throw new UnauthorizedAccessException("Invalid expiration claim");

                long currentTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

                if (currentTimestamp >= expirationValue)
                    throw new UnauthorizedAccessException("Token has expired");

                Claim? guidClaim = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                Claim? userRoleClaim = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
                UserAccount userAccount = await _userAccountRepository.GetUserAccountByGUID(Guid.Parse(guidClaim.Value));

                if (ValueCheckerHelper.IsNull(guidClaim) || ValueCheckerHelper.IsNull(userRoleClaim) ||
                    !string.Equals(userRoleClaim?.Value, "admin") ||
                    ValueCheckerHelper.IsNull(await _userAccountRepository.GetUserAccountByGUID(Guid.Parse(guidClaim.Value))))
                {
                    throw new UnauthorizedAccessException("User not found");
                }

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
