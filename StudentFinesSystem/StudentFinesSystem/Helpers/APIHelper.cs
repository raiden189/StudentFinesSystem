using Microsoft.Extensions.Configuration;
using StudentFinesSystem.Helpers.Interface;
using StudentFinesSystem.Models;
using StudentFinesSystem.Models.Interface;

namespace StudentFinesSystem.Helpers
{
    public class APIHelper : AccountAPI, IAPIHelper
    {
        private ILoginUser loginUser;
        public APIHelper(IConfiguration configuration, ILoginUser loginUser) : base(configuration, loginUser)
        {
            this.loginUser = loginUser;
        }

        public async Task<AuthenticatedUser> Authenticate(string username, string password)
        {
            try
            {
                var data = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("username", username),
                    new KeyValuePair<string, string>("password", password)
                });

                var authUser = await _api.PostRequest<AuthenticatedUser>("Token/Create", data);
                return authUser;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task GetLoginUser(AuthenticatedUser authenticatedUser)
        {
            try
            {
                if (!string.IsNullOrEmpty(authenticatedUser.Access_Token))
                {
                    _api.AddAuthorization(authenticatedUser.Access_Token);
                    var loginUser = await _api.GetRequest<LoginUser>($"api/User/GetUser?id={authenticatedUser.UserId}");
                    _loginUser.IsAdmin = authenticatedUser.IsAdmin;
                    _loginUser.UserId = loginUser.UserId;
                    _loginUser.Username = loginUser.Username;
                    _loginUser.FirstName = loginUser.FirstName;
                    _loginUser.MiddleName = loginUser.MiddleName;
                    _loginUser.LastName = loginUser.LastName;
                    _loginUser.Gender = loginUser.Gender;
                    _loginUser.CreateDate = loginUser.CreateDate;
                    _loginUser.Token = authenticatedUser.Access_Token;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
