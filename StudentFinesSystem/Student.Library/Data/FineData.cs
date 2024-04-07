using Student.Library.Data.Interface;
using Student.Library.Internal.DataAccess;
using Student.Library.Models;

namespace Student.Library.Data
{
    public class FineData : IFineData
    {
        private readonly ISqlDataAccess _dataAccess;
        public FineData(string connectionString)
        {
            _dataAccess = new SqlDataAccess(connectionString);
        }

        public List<Fines> GetFines(string Id) =>
            _dataAccess.LoadData<Fines, dynamic>("dbo.spFines_Get", new { UserId = Id }).ToList();

        public void SaveFine(StudentFine finesModel) =>
           _dataAccess.SaveData("dbo.spFines_Insert", finesModel);

        public void UpdateFine(StudentFine finesModel) =>
            _dataAccess.SaveData("dbo.spFines_Update", finesModel);

        public void DeleteFine(DeleteStudentFine finesModel) =>
            _dataAccess.SaveData("dbo.spFines_Delete", finesModel);
    }
}
