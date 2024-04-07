using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Student.Library.Data;
using Student.Library.Models;
using StudentAPI2.Models;

namespace StudentAPI2.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles = "Admin, Student")]
    public class FinesController : ControllerBase
    {
        private FinesDetailData _finesDetailData;
        private FineData _fineData;
        public FinesController(IConfiguration configuration)
        {
            Initialize(configuration);
        }

        private void Initialize(IConfiguration configuration)
        {
            _finesDetailData = new FinesDetailData(configuration.GetConnectionString("Student"));
            _fineData = new FineData(configuration.GetConnectionString("Student"));
        }

        [HttpGet]
        public List<FinesDetail> GetAllFines()
        {
            return _finesDetailData.GetFinesDetail();
        }

        [HttpGet]
        public List<StudentFines> GetAllFinesById(string userId)
        {
            var allFines = _finesDetailData.GetFinesDetail();
            var studentFines = _fineData.GetFines(userId);
            var joinedFines = from a in allFines
                              join s in studentFines on a.Id equals s.FineId
                              where s.UserId == userId
                              select new StudentFines 
                              {
                                  Id = s.Id,
                                  FineId = a.Id,
                                  Fine = a.Fine,
                                  FineName = a.FineName,
                                  FineDescription = a.FineDescription,
                                  CreatedBy = s.CreatedBy,
                                  CreatedDate = s.CreatedDate,
                              };

            return joinedFines.OrderBy(o => o.CreatedDate).ToList();
        }

        [HttpPost]
        public void AddStudentFine([FromBody] StudentFine studentFine)
        {
            _fineData.SaveFine(studentFine);
        }

        [HttpPost]
        public void UpdateStudentFine([FromBody] StudentFine studentFine)
        {
            _fineData.UpdateFine(studentFine);
        }

        [HttpPost]
        public void DeleteStudentFine([FromBody] DeleteStudentFine studentFine)
        {
            _fineData.DeleteFine(studentFine);
        }

        [HttpPost]
        public void CreateFine([FromBody] FinesDetail finesDetailsModel)
        {
            _finesDetailData.SaveFinesDetail(finesDetailsModel);
        }

        [HttpPost]
        public void UpdateFine([FromBody] FinesDetail finesDetailsModel)
        {
            _finesDetailData.UpdateFinesDetail(finesDetailsModel);
        }
    }
}
