using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student.Library.Models;
using StudentAPI2.Data;
using StudentAPI2.Models;
using System.Diagnostics;

namespace StudentAPI2.Controllers
{
    public class HomeController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger ,RoleManager<IdentityRole> roleManager ,UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            string[] roles = { "Admin", "Student" };

            foreach (var role in roles)
            {
                var roleExist = await _roleManager.RoleExistsAsync(role);
                if (!roleExist)
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            string adminEmail = "Admin@gmail.com";
            var adminUser = await _userManager.FindByNameAsync(adminEmail);
            if (adminUser == null)
            {
                var newUser = new IdentityUser(adminEmail);
                newUser.Email = adminEmail;
                var result = await _userManager.CreateAsync(newUser, "Admin!23");

                if(result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(newUser, "Admin");
                }
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
