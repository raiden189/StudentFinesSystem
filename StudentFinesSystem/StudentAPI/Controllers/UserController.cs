using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace StudentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        public UserController(UserManager<IdentityUser> userManager)
        {
            this._userManager = userManager;
        }

        //[Authorize(Roles = "")]
        //public void Get()
        //{
        //    string userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;


        //}
    }
}
