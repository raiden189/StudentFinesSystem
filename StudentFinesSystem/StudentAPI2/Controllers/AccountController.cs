using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using StudentAPI2.Models;
using StudentAPI2.Models.API;

namespace StudentAPI2.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        public AccountController(UserManager<IdentityUser> userManager)
        {
            this._userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAccount([FromBody] AccountModel accountModel)
        {
            var user = await _userManager.FindByNameAsync(accountModel.Username);

            if (user != null)
                throw new UserAlreadyExistException();

            var newUser = new IdentityUser(accountModel.Username);
            newUser.Email = accountModel.Username;
            var result = await _userManager.CreateAsync(newUser, accountModel.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, accountModel.Role);
                return new ObjectResult(new APIResponse<string> { Data = newUser.Id });
                //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                //await _emailSender.SendEmailAsync(username, "Confirm your email",
                //    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");
            }
            else
            {
                List<Error> errors = new List<Error>();
                foreach (var error in result.Errors)
                {
                    errors.Add(new Error { Code = error.Code, Description = error.Description });
                }
                return new ObjectResult(new APIResponse<string> { ErrorList = errors });
            }

        }

        [HttpPost]
        public async Task<IActionResult> UpdateAccount([FromBody] AccountModel accountModel)
        {
            var user = await _userManager.FindByNameAsync(accountModel.Username);

            if (user == null)
            {
                return BadRequest();
            }

            var result = await _userManager.ChangePasswordAsync(user, accountModel.Password, accountModel.NewPassword);
            if (!result.Succeeded)
            {
                List<Error> errors = new List<Error>();
                foreach (var error in result.Errors)
                {
                    errors.Add(new Error { Code = error.Code, Description = error.Description });
                }
                return new ObjectResult(new APIResponse<string> { ErrorList = errors });
            }

            return new ObjectResult(new APIResponse<string> { Data = user.Id });
        }

        [HttpPost]
        public async Task DeleteAccount(string username)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
                throw new UserDoestNotExistException();

            await _userManager.DeleteAsync(user);
        }
    }
}
