using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using XBInsaat.Core.Entites;
using XBInsaat.Service.CustomExceptions;
using System.Data;
using XBInsaat.Services.Services.Interfaces.Area;
using XBInsaat.Services.Dtos.Area;

namespace XBInsaat.Mvc.Areas.manage.Controllers
{
    [Area("manage")]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IAdminLoginServices _adminLoginServices;
        //private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager, IAdminLoginServices adminLoginServices)
        {
            _userManager = userManager;
            _adminLoginServices = adminLoginServices;
            //_roleManager = roleManager;
        }
        public async Task<IActionResult> Login()
        {
            AppUser user = User.Identity.IsAuthenticated ? await _userManager.FindByNameAsync(User.Identity.Name) : null;

            if (user != null && user.IsAdmin == true)
            {
                return RedirectToAction("index", "dashboard");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(AdminLoginPostDto adminLoginPostDto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                await _adminLoginServices.Login(adminLoginPostDto);
            }
            catch (UserNotFoundException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }

            return RedirectToAction("index", "dashboard");
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        public IActionResult Logout()
        {
            _adminLoginServices.Logout();
            return RedirectToAction("login", "account");
        }


        public async Task<IActionResult> CreateRole()
        {
            //var role1 = await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
            //var role2 = await _roleManager.CreateAsync(new IdentityRole("Admin"));
            //var role3 = await _roleManager.CreateAsync(new IdentityRole("Member"));
            //var role4 = await _roleManager.CreateAsync(new IdentityRole("Editor"));
            //var role5 = await _roleManager.CreateAsync(new IdentityRole("Viewer"));

            AppUser SuperAdmin = new AppUser { FullName = "Name", UserName = "Username" };
            var admin = await _userManager.CreateAsync(SuperAdmin, "password");
            var resultRole = await _userManager.AddToRoleAsync(SuperAdmin, "Admin");
            return Ok(resultRole);
        }

    }
}
