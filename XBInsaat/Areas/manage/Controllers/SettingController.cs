using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using XBInsaat.Core.Entites;
using XBInsaat.Data.Datacontext;
using XBInsaat.Mvc.Areas.manage.ViewModels;
using XBInsaat.Service.Helper;
using System.Data;
using XBInsaat.Services.Dtos.Area;
using XBInsaat.Services.Services.Interfaces.Area;

namespace XBInsaat.Mvc.Areas.manage.Controllers
{
    [Area("manage")]
    //[Authorize(Roles = "SuperAdmin,Admin")]

    public class SettingController : Controller
    {
        private readonly DataContext _context;
        private readonly ISettingEditServices _SettingEditServices;
        private readonly ISettingIndexServices _SettingIndexServices;

        public SettingController(DataContext context, ISettingEditServices SettingEditServices, ISettingIndexServices SettingIndexServices)
        {
            _context = context;
            _SettingEditServices = SettingEditServices;
            _SettingIndexServices = SettingIndexServices;
        }
        public IActionResult Index(int page = 1, string search = null)
        {
            ViewBag.Page = page;

            var Settings = _SettingIndexServices.SearchCheck(search);

            SettingIndexViewModel SettingIndexVM = new SettingIndexViewModel
            {
                PagenatedItems = PagenetedList<Setting>.Create(Settings, page, 6),
            };

            return View(SettingIndexVM);
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
                return RedirectToAction("notfound", "error");
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
