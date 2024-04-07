using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Student.Library.Data;
using Student.Library.Data.Interface;
using Student.Library.Models;
using System.Security.Claims;

namespace StudentAPI2.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles = "Admin, Student")]
    public class UserFinesController : ControllerBase
    {
        private IFineData _fineData;
        public UserFinesController(IConfiguration configuration)
        {
            Initialize(configuration);
        }

        private void Initialize(IConfiguration configuration)
        {
            _fineData = new FineData(configuration.GetConnectionString("Student"));
        }

        [Authorize(Roles = "Student")]
        public List<Fines> GetUserFine()
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return _fineData.GetFines(user);
        }

        [Authorize(Roles = "Admin")]
        public List<Fines> GetUserFineById(string id)
        {
            return _fineData.GetFines(id);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult AddStudentFine(StudentFine fines)
        {
            try
            {
                _fineData.SaveFine(fines);
                return Ok(fines);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult UpdateStudentFine(StudentFine fines)
        {
            try
            {
                _fineData.UpdateFine(fines);
                return Ok(fines);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
