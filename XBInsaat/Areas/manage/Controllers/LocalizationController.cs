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
using XBInsaat.Services.Services.Interfaces.Area.Localizations;

namespace XBInsaat.Mvc.Areas.manage.Controllers
{

    [Area("manage")]
    //[Authorize(Roles = "SuperAdmin,Admin,Editor")]

    public class LocalizationController : Controller
    {
        private readonly ILoggerServices _loggerServices;
        private readonly UserManager<AppUser> _userManager;
        private readonly DataContext _context;
        private readonly ILocalizationCreateServices _LocalizationCreateServices;
        private readonly ILocalizationEditServices _LocalizationEditServices;
        private readonly ILocalizationIndexServices _LocalizationIndexServices;

        public LocalizationController(ILoggerServices loggerServices, UserManager<AppUser> userManager, DataContext context, ILocalizationCreateServices LocalizationCreateServices, ILocalizationEditServices LocalizationEditServices, ILocalizationIndexServices LocalizationIndexServices)
        {
            _loggerServices = loggerServices;
            _userManager = userManager;
            _context = context;
            _LocalizationCreateServices = LocalizationCreateServices;
            _LocalizationEditServices = LocalizationEditServices;
            _LocalizationIndexServices = LocalizationIndexServices;
        }
        public IActionResult Index(int page = 1, string search = null)
        {
            ViewBag.Page = page;
            ViewBag.Search = search;

            var Localizations = _LocalizationIndexServices.SearchCheck(search);

            LocalizationIndexViewModel LocalizationIndexVM = new LocalizationIndexViewModel
            {
                PagenatedItems = PagenetedList<Localization>.Create(Localizations, page, 6),
            };

            return View(LocalizationIndexVM);
        }

        public IActionResult Create()
        {
            LocalizationCreateDto LocalizationCreateDto = new LocalizationCreateDto();

            return View(LocalizationCreateDto);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Create(LocalizationCreateDto LocalizationCreateDto)
        {
            LocalizationCreateDto LocalizationDto = new LocalizationCreateDto();

            try
            {
                var Localization = await _LocalizationCreateServices.CreateLocalization(LocalizationCreateDto);


                //Logger
                AppUser user = User.Identity.IsAuthenticated ? _userManager.Users.FirstOrDefault(x => x.UserName == User.Identity.Name && x.IsAdmin) : null;
                if (user == null)
                    throw new UserNotFoundException("Error bas verdi!");
                await _loggerServices.LoggerCreate("Localization", "Create", user.FullName, user.UserName, LocalizationCreateDto.Key);
            }
            catch (ItemNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(LocalizationDto);
            }
            catch (ItemNotFoundException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(LocalizationDto);
            }
            catch (ValueFormatExpception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(LocalizationDto);
            }
            catch (UserNotFoundException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(LocalizationDto);
            }


            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                //TempData["Error"] = ("Proses uğursuz oldu!");
                return View(LocalizationDto);
            }

            return RedirectToAction("index", "Localization");

        }



        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                await _LocalizationEditServices.IsExists(id);
                await _LocalizationEditServices.GetSearch(id);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "notfound");
            }

            return View(await _LocalizationEditServices.GetSearch(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(LocalizationEditDto LocalizationEdit)
        {
            try
            {
                await _LocalizationEditServices.LocalizationEdit(LocalizationEdit);

                //Logger
                var product = await _LocalizationEditServices.GetSearch(LocalizationEdit.Id);
                AppUser user = User.Identity.IsAuthenticated ? _userManager.Users.FirstOrDefault(x => x.UserName == User.Identity.Name && x.IsAdmin) : null;
                if (user == null)
                    throw new UserNotFoundException("Error bas verdi!");
                await _loggerServices.LoggerCreate("Localization", "Edit", user.FullName, user.UserName, product.Key);
            }
            catch (ValueFormatExpception ex)
            {

                ModelState.AddModelError("", ex.Message);
                return View(LocalizationEdit);
            }
            catch (ItemNotFoundException ex)
            {

                ModelState.AddModelError("", ex.Message);
                return View(LocalizationEdit);
            }
            catch (UserNotFoundException ex)
            {

                ModelState.AddModelError("", ex.Message);
                return View(LocalizationEdit);
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", ex.Message);
                return View(LocalizationEdit);
            }
            TempData["Success"] = ("Proses uğurlu oldu!");
            return RedirectToAction("index", "Localization");
        }

    }
}
