using XBInsaat.Core.Entites;
using XBInsaat.Mvc.Areas.manage.ViewModels;
using XBInsaat.Services.CustomExceptions;
using XBInsaat.Services.Dtos.Area;
using XBInsaat.Services.HelperService.Interfaces;
using XBInsaat.Services.Services.Interfaces.Area.UserManagers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using XBInsaat.Service.Helper;
using XBInsaat.Service.CustomExceptions;

namespace XBInsaat.Mvc.Areas.manage.Controllers
{

    [Area("manage")]
    [Authorize(Roles = "SuperAdmin")]
    public class UserManagerController : Controller
    {
        private readonly ILoggerServices _loggerServices;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAdminUserManagerIndexServices _adminUserManagerIndexServices;
        private readonly IAdminUserManagerDeleteServices _adminUserManagerDeleteServices;
        private readonly IAdminUserManagerEditServices _adminUserManagerEditServices;
        private readonly IAdminUserManagerCreateServices _adminUserManagerCreateServices;

        public UserManagerController(ILoggerServices loggerServices, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IAdminUserManagerIndexServices adminUserManagerIndexServices, IAdminUserManagerDeleteServices adminUserManagerDeleteServices, IAdminUserManagerEditServices adminUserManagerEditServices, IAdminUserManagerCreateServices adminUserManagerCreateServices)
        {
            _loggerServices = loggerServices;
            _userManager = userManager;
            _roleManager = roleManager;
            _adminUserManagerIndexServices = adminUserManagerIndexServices;
            _adminUserManagerDeleteServices = adminUserManagerDeleteServices;
            _adminUserManagerEditServices = adminUserManagerEditServices;
            _adminUserManagerCreateServices = adminUserManagerCreateServices;
        }

        public IActionResult Index(int page = 1, string name = null)
        {
            UserManagerIndexViewModel UserManagerIndexVM = new UserManagerIndexViewModel();
            try
            {
                var UserManager = _adminUserManagerIndexServices.GetUserManager(name);
                UserManagerIndexVM = new UserManagerIndexViewModel
                {
                    AppUsers = PagenetedList<AppUser>.Create(UserManager, page, 5),
                };
            }
            catch (NotFoundException)
            {
                return RedirectToAction("index", "notfound");
            }

            catch (Exception)
            {
                return RedirectToAction("index", "notfound");
            }
            return View(UserManagerIndexVM);
        }

        public async Task<IActionResult> Create()
        {
            UserManagerCreateViewModel userManagerCreateVM = new UserManagerCreateViewModel()
            {
                UserManagerCreateDto = new UserManagerCreateDto(),
                Roles = await _adminUserManagerCreateServices.GetRoles()
            };

            return View(userManagerCreateVM);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Create(UserManagerCreateDto userManagerCreateDto)
        {
            UserManagerCreateViewModel userManagerCreateVM = new UserManagerCreateViewModel();
            try
            {
                userManagerCreateVM = new UserManagerCreateViewModel()
                {
                    UserManagerCreateDto = new UserManagerCreateDto(),
                    Roles = await _adminUserManagerCreateServices.GetRoles()
                };
                await _adminUserManagerCreateServices.CreateUserManager(userManagerCreateDto);

                //Logger
                AppUser user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == User.Identity.Name && x.IsAdmin);
                await _loggerServices.LoggerCreate("UserManager", "Create", user.FullName, user.UserName, userManagerCreateDto.Username);
            }
            catch (ItemNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(userManagerCreateVM);
            }
            catch (ItemNotFoundException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(userManagerCreateVM);
            }
            catch (UserNotFoundException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(userManagerCreateVM);
            }
            catch (ValueFormatExpception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(userManagerCreateVM);
            }
            catch (ItemAlreadyException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(userManagerCreateVM);
            }


            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                //TempData["Error"] = ("Proses uğursuz oldu!");
                return View(userManagerCreateVM);
            }

            return RedirectToAction("index", "UserManager");

        }


        public async Task<IActionResult> Edit(string Id)
        {
            UserManagerEditViewModel userManagerCreateVM = new UserManagerEditViewModel();

            try
            {
                var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == Id);
                var role = await _roleManager.Roles.FirstOrDefaultAsync(x => x.Name == user.RoleName);
                UserManagerEditDto userManagerEditDto = new UserManagerEditDto()
                {
                    Fullname = user.FullName,
                    RoleId = role.Id,
                    Id = user.Id,
                    IsAdmin = user.IsAdmin,
                    Username = user.UserName,
                };

                userManagerCreateVM = new UserManagerEditViewModel()
                {
                    UserManagerEditDto = userManagerEditDto,
                    Roles = await _adminUserManagerCreateServices.GetRoles(),
                    RoleName = await _adminUserManagerEditServices.RoleName(Id)
                };

            }
            catch (NotFoundException)
            {
                return RedirectToAction("Index", "notfound");
            }
            catch (ItemNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Index", userManagerCreateVM);
            }
            catch (ItemNotFoundException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Index", userManagerCreateVM);
            }
            catch (ItemAlreadyException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Index", userManagerCreateVM);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "notfound");
            }
            return View(userManagerCreateVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserManagerEditDto UserManagerEditDto)
        {
            UserManagerEditViewModel userManagerCreateVM = new UserManagerEditViewModel();

            try
            {
                var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == UserManagerEditDto.Id);
                var role = await _roleManager.Roles.FirstOrDefaultAsync(x => x.Name == user.RoleName);
                UserManagerEditDto userManagerEditDto = new UserManagerEditDto()
                {
                    Fullname = user.FullName,
                    RoleId = role.Id,
                    Id = user.Id,
                    IsAdmin = user.IsAdmin,
                    Username = user.UserName,
                };

                userManagerCreateVM = new UserManagerEditViewModel()
                {
                    UserManagerEditDto = userManagerEditDto,
                    Roles = await _adminUserManagerCreateServices.GetRoles(),
                    RoleName = await _adminUserManagerEditServices.RoleName(UserManagerEditDto.Id)
                };

                await _adminUserManagerEditServices.EditUserManager(UserManagerEditDto);

                //Logger
                AppUser appUser = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == User.Identity.Name && x.IsAdmin);
                await _loggerServices.LoggerCreate("UserManager", "Edit", appUser.FullName, appUser.UserName, UserManagerEditDto.Username);
            }

            catch (NotFoundException)
            {

                return RedirectToAction("Index", "notfound");
            }
            catch (ItemNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", userManagerCreateVM);
            }
            catch (UserNotFoundException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", userManagerCreateVM);
            }
            catch (ValueFormatExpception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", userManagerCreateVM);
            }
            catch (ItemNotFoundException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", userManagerCreateVM);

            }
            catch (ItemAlreadyException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", userManagerCreateVM);
            }
            catch (ItemUseException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", userManagerCreateVM);
            }

            catch (ValueAlreadyExpception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", userManagerCreateVM);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "notfound");
            }
            TempData["Success"] = ("Proses uğurlu oldu");
            return RedirectToAction("Index", "UserManager");
        }

        public async Task<IActionResult> RestartLimitCount(string id, string? name, int page = 1)
        {
            UserManagerIndexViewModel UserManagerIndexVM = new UserManagerIndexViewModel();
            try
            {
                var UserManager = _adminUserManagerIndexServices.GetUserManager(name);
                UserManagerIndexVM = new UserManagerIndexViewModel
                {
                    AppUsers = PagenetedList<AppUser>.Create(UserManager, page, 5),
                };
                await _adminUserManagerEditServices.RestartLoginAttempCount(id);

                //Logger
                AppUser appUser = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == User.Identity.Name && x.IsAdmin);
                await _loggerServices.LoggerCreate("UserManager", "RestartLimitCount", appUser.FullName, appUser.UserName, appUser.UserName);
            }
            catch (NotFoundException)
            {
                return RedirectToAction("Index", "notfound");
            }
            catch (UserNotFoundException ex)
            {
                TempData["Error"] = (ex.Message);
                return Ok();
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "notfound");
            }
            TempData["Success"] = ("Limit yeniləndi");
            return Ok();
        }


        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _adminUserManagerDeleteServices.DeleteUserManager(id);

                //Logger
                var product = await _adminUserManagerEditServices.GetUserManager(id);
                AppUser user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == User.Identity.Name && x.IsAdmin);
                await _loggerServices.LoggerCreate("UserManager", "Delete", user.FullName, user.UserName, product.UserName);
            }
            catch (ItemNotFoundException ex)
            {
                TempData["Error"] = (ex.Message);
                return Ok();
            }
            catch (ItemUseException ex)
            {
                TempData["Error"] = (ex.Message);
                return Ok();
            }
            catch (UserNotFoundException ex)
            {
                TempData["Error"] = (ex.Message);
                return Ok();
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
                //return RedirectToAction("index", "notfound");
            }
            TempData["Success"] = ("Sənəd silindi!");
            return Ok();
        }
    }

}
