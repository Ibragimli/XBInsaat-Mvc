using Microsoft.AspNetCore.Identity;
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
    public class HighProjectController : Controller
    {
        private readonly IAdminHighProjectIndexServices _adminHighProjectIndexServices;
        private readonly IManageImageHelper _manageImageHelper;
        private readonly IAdminDeleteHighProjectServices _adminDeleteHighProjectServices;
        private readonly IAdminHighProjectEditServices _adminHighProjectEditServices;
        private readonly IAdminHighProjectCreateServices _adminHighProjectCreateServices;

        public HighProjectController(IAdminHighProjectIndexServices adminHighProjectIndexServices, IManageImageHelper manageImageHelper, IAdminDeleteHighProjectServices adminDeleteHighProjectServices, IAdminHighProjectEditServices adminHighProjectEditServices, IAdminHighProjectCreateServices adminHighProjectCreateServices)
        {
            _adminHighProjectIndexServices = adminHighProjectIndexServices;
            _manageImageHelper = manageImageHelper;
            _adminDeleteHighProjectServices = adminDeleteHighProjectServices;
            _adminHighProjectEditServices = adminHighProjectEditServices;
            _adminHighProjectCreateServices = adminHighProjectCreateServices;
        }
        public async Task<IActionResult> Index(int page = 1, string name = null)
        {
            HighProjectIndexViewModel highProjectIndexVM = new HighProjectIndexViewModel();
            try
            {
                var highProject = _adminHighProjectIndexServices.GetPoster(name);
                highProjectIndexVM = new HighProjectIndexViewModel
                {
                    HighProjects = PagenetedList<HighProject>.Create(highProject, page, 5),
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
            return View(highProjectIndexVM);
        }
        public IActionResult Create()
        {

            HighProjectCreateDto highProjectCreateDto = new HighProjectCreateDto();
            return View(highProjectCreateDto);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Create(HighProjectCreateDto highProjectCreateDto)
        {
            try
            {
                _adminHighProjectCreateServices.DtoCheck(highProjectCreateDto);
                // CheckImage
                _manageImageHelper.ImagesCheck(highProjectCreateDto.ImageFiles);
                var highProject = await _adminHighProjectCreateServices.CreateProject(highProjectCreateDto);
                await _adminHighProjectCreateServices.CreateImageFormFile(highProjectCreateDto.ImageFiles, highProject.Id);
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
            catch (ImageFormatException ex)
            {
                ModelState.AddModelError("HighProjectCreateDto.ImageFiles", ex.Message);
                return View();
            }
            catch (ImageNullException ex)
            {
                ModelState.AddModelError("HighProjectCreateDto.ImageFiles", ex.Message);
                return View();
            }

            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                //TempData["Error"] = ("Proses uğursuz oldu!");
                return View();
            }

            return RedirectToAction("index", "highproject");

        }


        public async Task<IActionResult> Edit(int id)
        {
            HighProjectEditViewModel highProjectEditVM = new HighProjectEditViewModel();

            try
            {
                highProjectEditVM = new HighProjectEditViewModel()
                {
                    HighProject = await _adminHighProjectEditServices.GetHighProject(id),
                    HighProjectImages = await _adminHighProjectEditServices.GetImages(id),
                };

            }
            catch (NotFoundException)
            {

                return RedirectToAction("Index", "notfound");
            }
            catch (ItemNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Index", highProjectEditVM);

            }
            catch (Exception)
            {
                return RedirectToAction("Index", "notfound");
            }
            return View(highProjectEditVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(HighProject highProject)
        {
            HighProjectEditViewModel highProjectEditVM = new HighProjectEditViewModel();

            try
            {
                highProjectEditVM = new HighProjectEditViewModel()
                {
                    HighProject = await _adminHighProjectEditServices.GetHighProject(highProject.Id),
                    HighProjectImages = await _adminHighProjectEditServices.GetImages(highProject.Id),
                };


                await _adminHighProjectEditServices.EditHighProject(highProject);
            }

            catch (NotFoundException)
            {

                return RedirectToAction("Index", "notfound");
            }
            catch (ItemNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", highProjectEditVM);

            }
            catch (ImageNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", highProjectEditVM);

            }
            catch (ImageFormatException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", highProjectEditVM);

            }
            catch (ImageCountException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", highProjectEditVM);

            }
            catch (ValueAlreadyExpception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", highProjectEditVM);

            }
            catch (Exception)
            {
                return RedirectToAction("Index", "notfound");
            }
            TempData["Success"] = ("Proses uğurlu oldu");
            return RedirectToAction("Index", "highproject");


        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _adminDeleteHighProjectServices.DeleteHighProject(id);
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
