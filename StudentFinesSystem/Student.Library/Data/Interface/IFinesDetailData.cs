using Student.Library.Models;

namespace Student.Library.Data.Interface
{
    public interface IFinesDetailData
    {
        List<FinesDetail> GetFinesDetail();
        void SaveFinesDetail(FinesDetail finesModel);
        void UpdateFinesDetail(FinesDetail finesModel);
    }
}