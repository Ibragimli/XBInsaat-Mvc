using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using XBInsaat.Core.Entites;
using XBInsaat.Mvc.Areas.manage.ViewModels;
using XBInsaat.Service.CustomExceptions;
using XBInsaat.Service.Helper;
using XBInsaat.Services.Dtos.Area;
using XBInsaat.Services.HelperService.Interfaces;
using XBInsaat.Services.Services.Interfaces.Area;

namespace XBInsaat.Mvc.Areas.manage.Controllers
{
    [Area("manage")]
    //[Authorize(Roles = "SuperAdmin")]

    public class RolePageController : Controller
    {
        private readonly ILoggerServices _loggerServices;
        private readonly UserManager<AppUser> _userManager;
        private readonly IRolePageEditServices _rolePageEditServices;
        private readonly IRolePageIndexServices _rolePageIndexServices;

        public RolePageController(ILoggerServices loggerServices, UserManager<AppUser> userManager, IRolePageEditServices RolePageEditServices, IRolePageIndexServices RolePageIndexServices)
        {
            _loggerServices = loggerServices;
            _userManager = userManager;
            _rolePageEditServices = RolePageEditServices;
            _rolePageIndexServices = RolePageIndexServices;
        }
        public async Task<IActionResult> Index(int page = 1, string search = null)
        {
            ViewBag.Page = page;

            var RolePages = _rolePageIndexServices.SearchCheck(search);

            RolePageIndexViewModel RolePageIndexVM = new RolePageIndexViewModel
            {
                PagenatedItems = PagenetedList<RolePage>.Create(RolePages, page, 10),
                RolePageIdentityRoles = await _rolePageEditServices.GetAllRolePageIdentityRoleIds(),

            };

            return View(RolePageIndexVM);
        }

        public async Task<IActionResult> Edit(int id)
        {
            RolePageEditViewModel rolePageEditVM = new RolePageEditViewModel();

            try
            {
                rolePageEditVM = new RolePageEditViewModel()
                {
                    Roles = await _rolePageEditServices.GetAllRoles(),
                    RolePageEditDto = await _rolePageEditServices.IsExists(id),
                    //RolePageIdentityRole = await _rolePageEditServices.GetRolePageIdentityRoleId(id)
                    RolePageIdentityRoles = await _rolePageEditServices.GetAllRolePageIdentityRoleIds(),
                };

            }
            catch (Exception)
            {
                return RedirectToAction("index", "notfound");

            }

            return View(rolePageEditVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(RolePageEditDto RolePageEditDto)
        {
            RolePageEditViewModel rolePageEditVM = new RolePageEditViewModel();

            try
            {
                rolePageEditVM = new RolePageEditViewModel()
                {
                    Roles = await _rolePageEditServices.GetAllRoles(),
                    RolePageEditDto = await _rolePageEditServices.IsExists(RolePageEditDto.Id),
                    RolePageIdentityRoles = await _rolePageEditServices.GetAllRolePageIdentityRoleIds(),
                };
                await _rolePageEditServices.RolePageEdit(RolePageEditDto);

                //Logger
                //var product = await _rolePageEditServices.GetSearch(RolePageEditDto.Id);
                //AppUser user = User.Identity.IsAuthenticated ? _userManager.Users.FirstOrDefault(x => x.UserName == User.Identity.Name && x.IsAdmin) : null;
                //if (user == null)
                //    throw new UserNotFoundException("Error bas verdi!");
                //await _loggerServices.LoggerCreate("RolePage", "Edit", user.FullName, user.UserName, product.Key);
            }
            catch (ValueFormatExpception ex)
            {

                ModelState.AddModelError("", ex.Message);
                return View(rolePageEditVM);
            }

            catch (ItemNotFoundException ex)
            {

                ModelState.AddModelError("", ex.Message);
                return View(rolePageEditVM);
            }
            catch (UserNotFoundException)
            {
                return RedirectToAction("index", "notfound");
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", ex.Message);
                return View(rolePageEditVM);
            }
            TempData["Success"] = ("Proses uğurlu oldu!");
            return RedirectToAction("index", "RolePage");
        }
    }
}
