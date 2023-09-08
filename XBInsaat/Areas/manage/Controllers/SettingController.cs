using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using XBInsaat.Core.Entites;
using XBInsaat.Data.Datacontext;
using XBInsaat.Mvc.Areas.manage.ViewModels;
using XBInsaat.Service.Helper;
using System.Data;
using XBInsaat.Services.Dtos.Area;
using XBInsaat.Services.Services.Interfaces.Area;
using XBInsaat.Service.CustomExceptions;
using XBInsaat.Services.HelperService.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace XBInsaat.Mvc.Areas.manage.Controllers
{
    [Area("manage")]
    //[Authorize(Roles = "SuperAdmin,Admin")]

    public class SettingController : Controller
    {
        private readonly ILoggerServices _loggerServices;
        private readonly UserManager<AppUser> _userManager;
        private readonly ISettingCreateServices _settingCreateServices;
        private readonly ISettingEditServices _SettingEditServices;
        private readonly ISettingIndexServices _SettingIndexServices;

        public SettingController(ILoggerServices loggerServices, UserManager<AppUser> userManager, ISettingCreateServices settingCreateServices, ISettingEditServices SettingEditServices, ISettingIndexServices SettingIndexServices)
        {
            _loggerServices = loggerServices;
            _userManager = userManager;
            _settingCreateServices = settingCreateServices;
            _SettingEditServices = SettingEditServices;
            _SettingIndexServices = SettingIndexServices;
        }
        public IActionResult Index(int page = 1, string search = null)
        {
            ViewBag.Page = page;
            ViewBag.Search = search;

            var Settings = _SettingIndexServices.SearchCheck(search);

            SettingIndexViewModel SettingIndexVM = new SettingIndexViewModel
            {
                PagenatedItems = PagenetedList<Setting>.Create(Settings, page, 6),
            };

            return View(SettingIndexVM);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SettingCreateDto settingCreateDto)
        {
            try
            {
                await _settingCreateServices.SettingCreate(settingCreateDto);
                AppUser user = User.Identity.IsAuthenticated ? _userManager.Users.FirstOrDefault(x => x.UserName == User.Identity.Name && x.IsAdmin) : null;
                if (user == null)
                    throw new UserNotFoundException("Error bas verdi!");
                await _loggerServices.LoggerCreate("Setting", "Edit", user.FullName, user.UserName, settingCreateDto.Key);
            }
            catch (ValueFormatExpception ex)
            {

                ModelState.AddModelError("", ex.Message);
                return View(settingCreateDto);
            }
            catch (ItemNotFoundException ex)
            {

                ModelState.AddModelError("", ex.Message);
                return View(settingCreateDto);
            }
            catch (UserNotFoundException)
            {
                return RedirectToAction("index", "notfound");
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", ex.Message);
                return View(settingCreateDto);
            }
            TempData["Success"] = ("Proses uğurlu oldu!");
            return RedirectToAction("index", "Setting");
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                await _SettingEditServices.IsExists(id);
                await _SettingEditServices.GetSearch(id);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "notfound");
            }

            return View(await _SettingEditServices.GetSearch(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SettingEditDto SettingEdit)
        {
            try
            {
                await _SettingEditServices.SettingEdit(SettingEdit);
                //Logger
                var product = await _SettingEditServices.GetSearch(SettingEdit.Id);
                AppUser user = User.Identity.IsAuthenticated ? _userManager.Users.FirstOrDefault(x => x.UserName == User.Identity.Name && x.IsAdmin) : null;
                if (user == null)
                    throw new UserNotFoundException("Error bas verdi!");
                await _loggerServices.LoggerCreate("Setting", "Edit", user.FullName, user.UserName, product.Key);
            }
            catch (ValueFormatExpception ex)
            {

                ModelState.AddModelError("", ex.Message);
                return View(SettingEdit);
            }
            catch (ItemNotFoundException ex)
            {

                ModelState.AddModelError("", ex.Message);
                return View(SettingEdit);
            }
            catch (UserNotFoundException)
            {
                return RedirectToAction("index", "notfound");
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", ex.Message);
                return View(SettingEdit);
            }
            TempData["Success"] = ("Proses uğurlu oldu!");
            return RedirectToAction("index", "Setting");
        }

    }
}
