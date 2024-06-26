﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using StudentAPI2.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StudentAPI2.Controllers
{
    public class TokenController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public TokenController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            this._context = context;
            this._userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string username, string password)
        {
            if (await IsValidUsernameAndPassword(username, password))
            {
                return new ObjectResult(await GenerateToken(username));
            }
            else
                return BadRequest();
        }

        private async Task<bool> IsValidUsernameAndPassword(string username, string password)
        {
            if (string.IsNullOrEmpty(username))
                return false;
            else if(string.IsNullOrEmpty(password))
                return false;

            var user = await _userManager.FindByNameAsync(username);
            return await _userManager.CheckPasswordAsync(user, password);
        }

        private async Task<dynamic> GenerateToken(string username)
        { 
            var user = await _userManager.FindByEmailAsync(username);
            var roles = from ur in _context.UserRoles
                        join r in _context.Roles on ur.RoleId equals r.Id
                        where ur.UserId == user.Id
                        select new { ur.UserId, ur.RoleId, r.Name };
            
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(JwtRegisteredClaimNames.Nbf, 
                        new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, 
                        new DateTimeOffset(DateTime.Now.AddDays(1)).ToUnixTimeSeconds().ToString())
            };
            bool isAdmin = false;
            foreach (var role in roles)
            {
                if(role.Name.Equals("Admin"))
                    isAdmin = true;

                claims.Add(new Claim(ClaimTypes.Role, role.Name));
            }

            var token = new JwtSecurityToken(
                new JwtHeader(
                    new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SecretKeyForOneDayCustomJwtBearer")),
                        SecurityAlgorithms.HmacSha256)),
                new JwtPayload(claims));

            var output = new
            {
                Access_Token = new JwtSecurityTokenHandler().WriteToken(token),
                UserId = user.Id,
                Username = username,
                IsAdmin = isAdmin
            };

            return output;
        }
    }
}
