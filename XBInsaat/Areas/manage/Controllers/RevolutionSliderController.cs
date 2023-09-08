using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using XBInsaat.Core.Entites;
using XBInsaat.Data.Datacontext;
using XBInsaat.Mvc.Areas.manage.ViewModels;
using XBInsaat.Service.CustomExceptions;
using XBInsaat.Service.Helper;
using XBInsaat.Services.Dtos.Area;
using XBInsaat.Services.HelperService.Interfaces;
using XBInsaat.Services.Services.Interfaces.Area;

namespace XBInsaat.Mvc.Areas.manage.Controllers
{
    [Area("manage")]
    //[Authorize(Roles = "SuperAdmin,Admin,Editor")]
    public class RevolutionSliderController : Controller
    {
        private readonly ILoggerServices _loggerServices;
        private readonly UserManager<AppUser> _userManager;
        private readonly DataContext _context;
        private readonly IRevolutionSliderEditServices _revolutionSliderEditServices;
        private readonly IRevolutionSliderIndexServices _revolutionSliderIndexServices;

        public RevolutionSliderController(ILoggerServices loggerServices, UserManager<AppUser> userManager, DataContext context, IRevolutionSliderEditServices revolutionSliderEditServices, IRevolutionSliderIndexServices revolutionSliderIndexServices)
        {
            _loggerServices = loggerServices;
            _userManager = userManager;
            _context = context;
            _revolutionSliderEditServices = revolutionSliderEditServices;
            _revolutionSliderIndexServices = revolutionSliderIndexServices;
        }
        public IActionResult Index(int page = 1)
        {
            ViewBag.Page = page;

            var RevolutionSliders = _revolutionSliderIndexServices.SearchCheck();

            RevolutionSliderIndexViewModel RevolutionSliderIndexVM = new RevolutionSliderIndexViewModel
            {
                PagenatedItems = PagenetedList<RevolutionSlider>.Create(RevolutionSliders, page, 6),
            };

            return View(RevolutionSliderIndexVM);
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                await _revolutionSliderEditServices.GetSearch(id);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "notfound");
            }

            return View(await _revolutionSliderEditServices.GetSearch(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(RevolutionSlider revolutionSlider)
        {
            try
            {
                await _revolutionSliderEditServices.RevolutionSliderEdit(revolutionSlider);

                //Logger
                var product = await _revolutionSliderEditServices.GetSearch(revolutionSlider.Id);
                AppUser user = User.Identity.IsAuthenticated ? _userManager.Users.FirstOrDefault(x => x.UserName == User.Identity.Name && x.IsAdmin) : null;
                if (user == null)
                    throw new UserNotFoundException("Error bas verdi!");
                await _loggerServices.LoggerCreate("RevolutionSlider", "Edit", user.FullName, user.UserName, product.Image);
            }
            catch (ValueFormatExpception ex)
            {

                ModelState.AddModelError("", ex.Message);
                return View(revolutionSlider);
            }
            catch (ItemNotFoundException ex)
            {

                ModelState.AddModelError("", ex.Message);
                return View(revolutionSlider);
            }
            catch (UserNotFoundException)
            {
                return RedirectToAction("index", "notfound");
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", ex.Message);
                return View(revolutionSlider);
            }
            TempData["Success"] = ("Proses uğurlu oldu!");
            return RedirectToAction("index", "RevolutionSlider");
        }

    }
}
