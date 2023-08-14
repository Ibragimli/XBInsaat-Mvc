using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using XBInsaat.Core.Entites;
using XBInsaat.Data.Datacontext;
using XBInsaat.Mvc.Areas.manage.ViewModels;
using XBInsaat.Service.CustomExceptions;
using XBInsaat.Service.Helper;
using XBInsaat.Services.Dtos.Area;
using XBInsaat.Services.HelperService.Interfaces;
using XBInsaat.Services.Services.Implementations.Area;
using XBInsaat.Services.Services.Interfaces.Area;

namespace XBInsaat.Mvc.Areas.manage.Controllers
{

    [Area("manage")]
    [Authorize(Roles = "SuperAdmin")]
    public class ImageSettingController : Controller
    {
        private readonly ILoggerServices _loggerServices;
        private readonly UserManager<AppUser> _userManager;
        private readonly DataContext _context;
        private readonly IImageSettingEditServices _ImageSettingEditServices;
        private readonly IImageSettingIndexServices _ImageSettingIndexServices;

        public ImageSettingController(ILoggerServices loggerServices, UserManager<AppUser> userManager, DataContext context, IImageSettingEditServices ImageSettingEditServices, IImageSettingIndexServices ImageSettingIndexServices)
        {
            _loggerServices = loggerServices;
            _userManager = userManager;
            _context = context;
            _ImageSettingEditServices = ImageSettingEditServices;
            _ImageSettingIndexServices = ImageSettingIndexServices;
        }
        public IActionResult Index(int page = 1, string search = null)
        {
            ViewBag.Page = page;

            var ImageSettings = _ImageSettingIndexServices.SearchCheck(search);

            ImageSettingIndexViewModel ImageSettingIndexVM = new ImageSettingIndexViewModel
            {
                PagenatedItems = PagenetedList<ImageSetting>.Create(ImageSettings, page, 6),
            };

            return View(ImageSettingIndexVM);
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                await _ImageSettingEditServices.IsExists(id);
                await _ImageSettingEditServices.GetSearch(id);
            }
            catch (Exception)
            {
                return RedirectToAction("index", "notfound");

            }

            return View(await _ImageSettingEditServices.GetSearch(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ImageSettingEditDto ImageSettingEdit)
        {
            try
            {
                await _ImageSettingEditServices.ImageSettingEdit(ImageSettingEdit);

                //Logger
                var product = await _ImageSettingEditServices.GetSearch(ImageSettingEdit.Id);
                AppUser user = User.Identity.IsAuthenticated ? _userManager.Users.FirstOrDefault(x => x.UserName == User.Identity.Name && x.IsAdmin) : null;
                if (user == null)
                    throw new UserNotFoundException("Error bas verdi!");
                await _loggerServices.LoggerCreate("ImageSetting", "Edit", user.FullName, user.UserName, product.Key);
            }
            catch (ValueFormatExpception ex)
            {

                ModelState.AddModelError("", ex.Message);
                return View(ImageSettingEdit);
            }
            catch (ItemNotFoundException ex)
            {

                ModelState.AddModelError("", ex.Message);
                return View(ImageSettingEdit);
            }
            catch (UserNotFoundException)
            {
                return RedirectToAction("index", "notfound");
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", ex.Message);
                return View(ImageSettingEdit);
            }
            TempData["Success"] = ("Proses uğurlu oldu!");
            return RedirectToAction("index", "ImageSetting");
        }
    }
}
