using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Student.Library.Data;
using Student.Library.Data.Interface;
using Student.Library.Models;
using StudentAPI2.Data;
using System.Runtime.CompilerServices;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StudentAPI2.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles = "Admin, Student")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;
        private IUserData _userData;
        public UserController(UserManager<IdentityUser> userManager, ApplicationDbContext context, IConfiguration configuration)
        {
            this._userManager = userManager;
            this._context = context;
            Initialize(configuration);
        }

        private void Initialize(IConfiguration configuration)
        {
            _userData = new UserData(configuration.GetConnectionString("Student"));
        }

        [HttpGet]
        public List<User> GetAllUser()
        {
            return _userData.GetAllUser();
        }

        [HttpGet]
        public async Task<List<UserModel>> GetAllStudentUser()
        {
            List<UserModel> students = new List<UserModel>();
            var users = _userData.GetAllUser();
            foreach (var user in users)
            {
                var student = await _userManager.FindByIdAsync(user.UserId);
                var roles = from ur in _context.UserRoles
                            join r in _context.Roles on ur.RoleId equals r.Id
                            where ur.UserId == student.Id
                            select new { ur.UserId, ur.RoleId, r.Name };

                if (roles.Any(a => a.Name.Equals("Student")))
                    students.Add(new UserModel
                    { 
                        UserId = student.Id,
                        FirstName = user.FirstName,
                        MiddleName = user.MiddleName,
                        LastName = user.LastName,
                        Gender = user.Gender,
                    });
            }
            return students;
        }
         
        [HttpGet]
        public async Task<User> GetUser(string id)
        {
            //var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return new User();

            var userData = _userData.GetUserById(id);
            if(userData == null)
                return new User { UserId = id };
            return new User 
            {
                UserId = user.Id,
                UserName = user.UserName,
                FirstName = userData.FirstName,
                MiddleName = userData.MiddleName,
                LastName = userData.LastName,
                Gender = userData.Gender,
            };
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await Task.Run(() => _userData.SaveUser(new DBUser
            {
                UserId = id,
                FirstName = user.FirstName,
                MiddleName = user.MiddleName,
                LastName = user.LastName,
                Gender = user.Gender
            }));
            return Ok();
        }
    }
}
