using XBInsaat.Core.Entites;
using XBInsaat.Mvc.Areas.manage.ViewModels;
using XBInsaat.Services.CustomExceptions;
using XBInsaat.Services.Dtos.Area;
using XBInsaat.Services.HelperService.Interfaces;
using XBInsaat.Services.Services.Interfaces.Area.RoleManagers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using XBInsaat.Service.Helper;
using XBInsaat.Service.CustomExceptions;

namespace XBInsaat.Mvc.Areas.manage.Controllers
{
    [Area("manage")]
    [Authorize(Roles = "SuperAdmin")]

    public class RoleManagerController : Controller
    {
        private readonly ILoggerServices _loggerServices;
        private readonly UserManager<AppUser> _userManager;
        private readonly IAdminRoleManagerIndexServices _adminRoleManagerIndexServices;
        private readonly IAdminRoleManagerDeleteServices _adminRoleManagerDeleteServices;
        private readonly IAdminRoleManagerEditServices _adminRoleManagerEditServices;
        private readonly IAdminRoleManagerCreateServices _adminRoleManagerCreateServices;

        public RoleManagerController(ILoggerServices loggerServices, UserManager<AppUser> userManager, IAdminRoleManagerIndexServices adminRoleManagerIndexServices, IAdminRoleManagerDeleteServices adminRoleManagerDeleteServices, IAdminRoleManagerEditServices adminRoleManagerEditServices, IAdminRoleManagerCreateServices adminRoleManagerCreateServices)
        {
            _loggerServices = loggerServices;
            _userManager = userManager;
            _adminRoleManagerIndexServices = adminRoleManagerIndexServices;
            _adminRoleManagerDeleteServices = adminRoleManagerDeleteServices;
            _adminRoleManagerEditServices = adminRoleManagerEditServices;
            _adminRoleManagerCreateServices = adminRoleManagerCreateServices;
        }
        public IActionResult Index(int page = 1, string name = null)
        {
            RoleManagerIndexViewModel RoleManagerIndexVM = new RoleManagerIndexViewModel();
            try
            {
                var roleManager = _adminRoleManagerIndexServices.GetRoleManager(name);
                RoleManagerIndexVM = new RoleManagerIndexViewModel
                {
                    RoleManagers = PagenetedList<IdentityRole>.Create(roleManager, page, 5),
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
            return View(RoleManagerIndexVM);
        }
        public IActionResult Create()
        {
            RoleManagerCreateDto roleManagerCreateDto = new RoleManagerCreateDto();

            return View(roleManagerCreateDto);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Create(RoleManagerCreateDto roleManagerCreateDto)
        {
            try
            {
                await _adminRoleManagerCreateServices.CreateRoleManager(roleManagerCreateDto);

                //Logger
                AppUser user = User.Identity.IsAuthenticated ? _userManager.Users.FirstOrDefault(x => x.UserName == User.Identity.Name && x.IsAdmin) : null;
                if (user == null)
                    throw new UserNotFoundException("Error bas verdi!");
                await _loggerServices.LoggerCreate("RoleManager", "Create", user.FullName, user.UserName, roleManagerCreateDto.Role);
            }
            catch (ItemNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(roleManagerCreateDto);
            }
            catch (ItemNotFoundException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(roleManagerCreateDto);
            }
            catch (UserNotFoundException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(roleManagerCreateDto);
            }
            catch (ValueFormatExpception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(roleManagerCreateDto);
            }
            catch (ItemAlreadyException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(roleManagerCreateDto);
            }


            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                //TempData["Error"] = ("Proses uğursuz oldu!");
                return View(roleManagerCreateDto);
            }

            return RedirectToAction("index", "RoleManager");

        }


        public async Task<IActionResult> Edit(string Id)
        {
            var roleManagerExist = new RoleManagerEditDto();
            try
            {
                var role = await _adminRoleManagerEditServices.GetRoleManager(Id);
                roleManagerExist = new RoleManagerEditDto
                {
                    Id = role.Id,
                    RoleName = role.Name
                };
            }
            catch (NotFoundException)
            {
                return RedirectToAction("Index", "notfound");
            }
            catch (ItemNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Index", roleManagerExist);
            }
            catch (ItemNotFoundException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Index", roleManagerExist);
            }
            catch (ItemAlreadyException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Index", roleManagerExist);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "notfound");
            }
            return View(roleManagerExist);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(RoleManagerEditDto roleManagerEditDto)
        {
            var roleManagerExist = new RoleManagerEditDto();

            try
            {
                var role = await _adminRoleManagerEditServices.GetRoleManager(roleManagerEditDto.Id);
                roleManagerExist = new RoleManagerEditDto
                {
                    Id = role.Id,
                    RoleName = role.Name
                };
                await _adminRoleManagerEditServices.EditRoleManager(roleManagerEditDto);

                //Logger
                var product = await _adminRoleManagerEditServices.GetRoleManager(roleManagerEditDto.Id);
                AppUser user = User.Identity.IsAuthenticated ? _userManager.Users.FirstOrDefault(x => x.UserName == User.Identity.Name && x.IsAdmin) : null;
                if (user == null)
                    throw new UserNotFoundException("Error bas verdi!");
                await _loggerServices.LoggerCreate("RoleManager", "Edit", user.FullName, user.UserName, product.Name);
            }

            catch (NotFoundException)
            {

                return RedirectToAction("Index", "notfound");
            }
            catch (ItemNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", roleManagerEditDto);
            }
            catch (UserNotFoundException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", roleManagerEditDto);
            }
            catch (ValueFormatExpception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", roleManagerEditDto);
            }
            catch (ItemNotFoundException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", roleManagerEditDto);

            }
            catch (ItemAlreadyException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", roleManagerEditDto);
            }
            catch (ItemUseException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", roleManagerEditDto);
            }

            catch (ValueAlreadyExpception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", roleManagerEditDto);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "notfound");
            }
            TempData["Success"] = ("Proses uğurlu oldu");
            return RedirectToAction("Index", "RoleManager");
        }

        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _adminRoleManagerDeleteServices.DeleteRoleManager(id);

                //Logger
                var product = await _adminRoleManagerEditServices.GetRoleManager(id);
                AppUser user = User.Identity.IsAuthenticated ? _userManager.Users.FirstOrDefault(x => x.UserName == User.Identity.Name && x.IsAdmin) : null;
                if (user == null)
                    throw new UserNotFoundException("Error bas verdi!");
                await _loggerServices.LoggerCreate("RoleManager", "Delete", user.FullName, user.UserName, product.Name);

            }
            catch (ItemNotFoundException ex)
            {
                TempData["Error"] = (ex.Message);
                return Ok();
            }
            catch (UserNotFoundException ex)
            {
                TempData["Error"] = (ex.Message);
                return Ok();
            }
            catch (ItemUseException ex)
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
