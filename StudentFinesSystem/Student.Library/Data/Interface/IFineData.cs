using Student.Library.Models;

namespace Student.Library.Data.Interface
{
    public interface IFineData
    {
        List<Fines> GetFines(string Id);
        void SaveFine(StudentFine finesModel);
        void UpdateFine(StudentFine finesModel);
        void DeleteFine(DeleteStudentFine finesModel);
    }
}