using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using StudentFinesSystem.Helpers.Base;
using StudentFinesSystem.Helpers.Interface;
using StudentFinesSystem.Models;
using StudentFinesSystem.Models.Interface;

namespace StudentFinesSystem.Helpers
{
    public class StudentAPI : APIBase
    {
        public StudentAPI(IConfiguration configuration, ILoginUser loginUser) : base(configuration, loginUser)
        { }

        public async Task<List<StudentModel>> GetAllStudents()
        {
            try
            {
                List<StudentModel> respStudents = new List<StudentModel>();

                var students = await _api.GetRequest<List<StudentModel>>("api/User/GetAllStudentUser");
                //var fines = await _api.GetRequest<dynamic>("api/Fines/GetAllFines");
                //var finesDetail = await _api.GetRequest<dynamic>("api/Fines/GetAllFines");

                return students;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task SaveUser(CreateNameModel createNameModel)
        {
            try
            {
                var authUser = await _api.PostJsonRequest<dynamic, CreateNameModel>("api/User/CreateUser", createNameModel);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
