using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentAPI.Models;

namespace StudentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger<AuthenticationController> logger;
        public AuthenticationController(ILogger<AuthenticationController> logger)
        {
            this.logger = logger;
        }

        public bool Authorize(LoginModel loginModel)
        { 
            int loginUserHash = loginModel.GetHashCode();

            return true;
        }
    }
}