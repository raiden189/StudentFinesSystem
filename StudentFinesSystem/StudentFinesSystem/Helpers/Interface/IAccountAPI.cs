using StudentFinesSystem.Models;

namespace StudentFinesSystem.Helpers.Interface
{
    public interface IAccountAPI
    {
        Task<T> Register<T, U>(U accountViewModel);

        Task<T> ChangePassword<T, U>(U accountViewModel);
    }
}