using Microsoft.AspNetCore.Mvc;
using XBInsaat.Core.Entites;
using XBInsaat.Mvc.Areas.manage.ViewModels;
using XBInsaat.Service.CustomExceptions;
using XBInsaat.Service.Helper;
using XBInsaat.Service.HelperService.Interfaces;
using XBInsaat.Services.Dtos.Area;
using XBInsaat.Services.Services.Interfaces.Area;

namespace XBInsaat.Mvc.Areas.manage.Controllers
{
    [Area("manage")]
    //[Authorize(Roles = "SuperAdmin,Admin")]
    public class XBServiceController : Controller
    {
        private readonly IAdminXBServiceIndexServices _adminXBServiceIndexServices;
        private readonly IAdminDeleteXBServiceServices _adminDeleteXBServiceServices;
        private readonly IAdminXBServiceEditServices _adminXBServiceEditServices;
        private readonly IAdminXBServiceCreateServices _adminXBServiceCreateServices;

        public XBServiceController(IAdminXBServiceIndexServices adminXBServiceIndexServices, IAdminDeleteXBServiceServices adminDeleteXBServiceServices, IAdminXBServiceEditServices adminXBServiceEditServices, IAdminXBServiceCreateServices adminXBServiceCreateServices)
        {
            _adminXBServiceIndexServices = adminXBServiceIndexServices;
            _adminDeleteXBServiceServices = adminDeleteXBServiceServices;
            _adminXBServiceEditServices = adminXBServiceEditServices;
            _adminXBServiceCreateServices = adminXBServiceCreateServices;
        }
        public async Task<IActionResult> Index(int page = 1, string name = null)
        {
            XBServiceIndexViewModel XBServiceIndexVM = new XBServiceIndexViewModel();
            try
            {
                var XBService = _adminXBServiceIndexServices.GetPoster(name);
                XBServiceIndexVM = new XBServiceIndexViewModel
                {
                    XBServices = PagenetedList<XBService>.Create(XBService, page, 5),
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
            return View(XBServiceIndexVM);
        }
        public IActionResult Create()
        {

            XBServiceCreateDto XBServiceCreateDto = new XBServiceCreateDto();
            return View(XBServiceCreateDto);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Create(XBServiceCreateDto XBServiceCreateDto)
        {
            try
            {
                _adminXBServiceCreateServices.DtoCheck(XBServiceCreateDto);
                var XBService = await _adminXBServiceCreateServices.CreateProject(XBServiceCreateDto);
            }
            catch (ItemNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }

            catch (ValueFormatExpception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                //TempData["Error"] = ("Proses uğursuz oldu!");
                return View();
            }

            return RedirectToAction("index", "XBService");

        }


        public async Task<IActionResult> Edit(int id)
        {
            XBServiceEditViewModel XBServiceEditVM = new XBServiceEditViewModel();

            try
            {
                XBServiceEditVM = new XBServiceEditViewModel()
                {
                    XBService = await _adminXBServiceEditServices.GetXBService(id),
                };

            }
            catch (NotFoundException)
            {

                return RedirectToAction("Index", "notfound");
            }
            catch (ItemNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Index", XBServiceEditVM);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "notfound");
            }
            return View(XBServiceEditVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(XBService XBService)
        {
            XBServiceEditViewModel XBServiceEditVM = new XBServiceEditViewModel();

            try
            {
                XBServiceEditVM = new XBServiceEditViewModel()
                {
                    XBService = await _adminXBServiceEditServices.GetXBService(XBService.Id),
                };


                await _adminXBServiceEditServices.EditXBService(XBService);
            }

            catch (NotFoundException)
            {

                return RedirectToAction("Index", "notfound");
            }
            catch (ItemNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", XBServiceEditVM);

            }
            catch (ValueFormatExpception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", XBServiceEditVM);
            }
            catch (ValueAlreadyExpception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", XBServiceEditVM);

            }
            catch (Exception)
            {
                return RedirectToAction("Index", "notfound");
            }
            TempData["Success"] = ("Proses uğurlu oldu");
            return RedirectToAction("Index", "XBService");


        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _adminDeleteXBServiceServices.DeleteXBService(id);
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
            catch (Exception ex)
            {
                return Ok(ex.Message);
                //return RedirectToAction("index", "notfound");
            }
            TempData["Success"] = ("Elan silindi!");
            return Ok();
        }
    }
}
