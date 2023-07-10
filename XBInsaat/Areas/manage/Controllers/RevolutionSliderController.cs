using Microsoft.AspNetCore.Mvc;
using XBInsaat.Core.Entites;
using XBInsaat.Data.Datacontext;
using XBInsaat.Mvc.Areas.manage.ViewModels;
using XBInsaat.Service.CustomExceptions;
using XBInsaat.Service.Helper;
using XBInsaat.Services.Dtos.Area;
using XBInsaat.Services.Services.Interfaces.Area;

namespace XBInsaat.Mvc.Areas.manage.Controllers
{
    [Area("manage")]
    //[Authorize(Roles = "SuperAdmin,Admin")]

    public class RevolutionSliderController : Controller
    {
        private readonly DataContext _context;
        private readonly IRevolutionSliderEditServices _revolutionSliderEditServices;
        private readonly IRevolutionSliderIndexServices _revolutionSliderIndexServices;

        public RevolutionSliderController(DataContext context, IRevolutionSliderEditServices revolutionSliderEditServices, IRevolutionSliderIndexServices revolutionSliderIndexServices)
        {
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
                return RedirectToAction("notfound", "error");
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
