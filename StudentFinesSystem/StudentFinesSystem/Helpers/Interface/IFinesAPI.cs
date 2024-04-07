using StudentFinesSystem.Models;

namespace StudentFinesSystem.Helpers.Interface
{
    public interface IFinesAPI
    {
        Task CreateFine(FinesListModel finesListModel);
        Task<List<FinesListModel>> GetAllFines();
        Task<List<StudentFinesModel>> GetStudentFineById(string userId);
        Task UpdateFine(FinesListModel finesListModel);
        Task AddStudentFine(Fines studentFine);
        Task DeleteStudentFine(StudentFineDelete studentFine);
    }
}