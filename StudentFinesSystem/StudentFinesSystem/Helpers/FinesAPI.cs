
using Microsoft.Extensions.Configuration;
using StudentFinesSystem.Helpers.Interface;
using StudentFinesSystem.Models;
using StudentFinesSystem.Models.Interface;

namespace StudentFinesSystem.Helpers
{
    public class FinesAPI : StudentAPI
    {
        public FinesAPI(IConfiguration configuration, ILoginUser loginUser) : base(configuration, loginUser)
        {
        }

        public async Task<List<FinesListModel>> GetAllFines()
        {
            try
            {
                var fines = await _api.GetRequest<List<FinesListModel>>("api/Fines/GetAllFines");

                return fines;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<StudentFinesModel>> GetStudentFineById(string userId)
        {
            try
            {
                var fines = await _api.GetRequest<List<StudentFinesModel>>($"api/Fines/GetAllFinesById?userId={userId}");

                return fines;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task AddStudentFine(Fines studentFine)
        {
            try
            {
                var fines = await _api.PostJsonRequest<dynamic, Fines>("api/Fines/AddStudentFine", studentFine);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task DeleteStudentFine(StudentFineDelete studentFine)
        {
            try
            {
                var fines = await _api.PostJsonRequest<dynamic, StudentFineDelete>("api/Fines/DeleteStudentFine", studentFine);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task CreateFine(FinesListModel finesListModel)
        {
            try
            {
                var fines = await _api.PostJsonRequest<dynamic, FinesListModel>("api/Fines/CreateFine", finesListModel);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task UpdateFine(FinesListModel finesListModel)
        {
            try
            {
                var fines = await _api.PostJsonRequest<dynamic, FinesListModel>("api/Fines/UpdateFine", finesListModel);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
