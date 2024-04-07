using Student.Library.Data.Interface;
using Student.Library.Internal.DataAccess;
using Student.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student.Library.Data
{
    public class FinesDetailData : IFinesDetailData
    {
        private readonly ISqlDataAccess _dataAccess;

        public FinesDetailData(string connectionString)
        {
            _dataAccess = new SqlDataAccess(connectionString);
        }

        public List<FinesDetail> GetFinesDetail() =>
           _dataAccess.LoadData<FinesDetail, dynamic>("dbo.spFinesDetails_GetAll", null).ToList();

        public void SaveFinesDetail(FinesDetail finesModel) =>
           _dataAccess.SaveData("dbo.spFinesDetails_Insert", new { finesModel.Fine, finesModel.FineName, finesModel.FineDescription });

        public void UpdateFinesDetail(FinesDetail finesModel) =>
           _dataAccess.SaveData("dbo.spFinesDetails_Update", new { finesModel.Id, finesModel.Fine, finesModel.FineName, finesModel.FineDescription });
    }
}
