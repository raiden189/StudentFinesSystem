using StudentFinesSystem.Models;

namespace StudentFinesSystem.Helpers.Interface
{
    public interface IAPIHelper : IStudentAPI, IAccountAPI, IFinesAPI
    {
        Task<AuthenticatedUser> Authenticate(string username, string password);

        Task GetLoginUser(AuthenticatedUser authenticatedUser);
    }
}