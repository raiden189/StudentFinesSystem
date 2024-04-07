using StudentFinesSystem.Models;

namespace StudentFinesSystem.Helpers.Interface
{
    public interface IStudentAPI
    {
        Task<List<StudentModel>> GetAllStudents();

        Task SaveUser(CreateNameModel createNameModel);
    }
}