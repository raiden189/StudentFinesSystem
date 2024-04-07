using Microsoft.Extensions.Configuration;
using StudentFinesSystem.Helpers.Base;
using StudentFinesSystem.Helpers.Interface;
using StudentFinesSystem.Models;
using StudentFinesSystem.Models.Interface;
using System.Security.Cryptography.X509Certificates;

namespace StudentFinesSystem.Helpers
{
    public class AccountAPI : FinesAPI
    {
        public AccountAPI(IConfiguration configuration, ILoginUser loginUser) : base(configuration, loginUser)
        {
        }

        public async Task<T> Register<T, U>(U accountViewModel)
        {
            try
            {
                var response = await _api.PostJsonRequest<T, U>("api/Account/RegisterAccount", accountViewModel);
                return response;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<T> ChangePassword<T, U>(U accountViewModel)
        {
            try
            {
                var result = await _api.PostJsonRequest<T, U>("api/Account/UpdateAccount", accountViewModel);
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
