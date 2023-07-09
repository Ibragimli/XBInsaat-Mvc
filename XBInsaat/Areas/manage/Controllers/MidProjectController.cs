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
    public class MidProjectController : Controller
    {
        private readonly IAdminMidProjectIndexServices _adminMidProjectIndexServices;
        private readonly IManageImageHelper _manageImageHelper;
        private readonly IAdminDeleteMidProjectServices _adminDeleteMidProjectServices;
        private readonly IAdminMidProjectEditServices _adminMidProjectEditServices;
        private readonly IAdminMidProjectCreateServices _adminMidProjectCreateServices;

        public MidProjectController(IAdminMidProjectIndexServices adminMidProjectIndexServices, IManageImageHelper manageImageHelper, IAdminDeleteMidProjectServices adminDeleteMidProjectServices, IAdminMidProjectEditServices adminMidProjectEditServices, IAdminMidProjectCreateServices adminMidProjectCreateServices)
        {
            _adminMidProjectIndexServices = adminMidProjectIndexServices;
            _manageImageHelper = manageImageHelper;
            _adminDeleteMidProjectServices = adminDeleteMidProjectServices;
            _adminMidProjectEditServices = adminMidProjectEditServices;
            _adminMidProjectCreateServices = adminMidProjectCreateServices;
        }
        public async Task<IActionResult> Index(int page = 1, string name = null)
        {
            MidProjectIndexViewModel MidProjectIndexVM = new MidProjectIndexViewModel();
            try
            {
                var MidProject = _adminMidProjectIndexServices.GetPoster(name);
                MidProjectIndexVM = new MidProjectIndexViewModel
                {
                    MidProjects = PagenetedList<MidProject>.Create(MidProject, page, 5),
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
            return View(MidProjectIndexVM);
        }
        public async Task<IActionResult> Create()
        {
            MidProjectCreateViewModel midProjectCreateVM = new MidProjectCreateViewModel();

            try
            {
                midProjectCreateVM = new MidProjectCreateViewModel()
                {

                    MidProjectCreateDto = new MidProjectCreateDto(),
                    HighProjects = await _adminMidProjectCreateServices.GetAllHighProjects()
                };
            }
            catch (Exception)
            {
                return RedirectToAction("index", "notfound");
            }
            return View(midProjectCreateVM);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Create(MidProjectCreateDto MidProjectCreateDto)
        {
            MidProjectCreateViewModel midProjectCreateVM = new MidProjectCreateViewModel();

            try
            {
                midProjectCreateVM = new MidProjectCreateViewModel()
                {
                    MidProjectCreateDto = new MidProjectCreateDto(),
                    HighProjects = await _adminMidProjectCreateServices.GetAllHighProjects()
                };
                await _adminMidProjectCreateServices.DtoCheck(MidProjectCreateDto);
                // CheckImage
                _manageImageHelper.ImagesCheck(MidProjectCreateDto.ImageFiles);
                var MidProject = await _adminMidProjectCreateServices.CreateProject(MidProjectCreateDto);
                await _adminMidProjectCreateServices.CreateImageFormFile(MidProjectCreateDto.ImageFiles, MidProject.Id);
            }
            catch (ItemNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(midProjectCreateVM);
            }
            catch (ItemNotFoundException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(midProjectCreateVM);
            }
            catch (ValueFormatExpception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(midProjectCreateVM);
            }
            catch (ImageFormatException ex)
            {
                ModelState.AddModelError("MidProjectCreateDto.ImageFiles", ex.Message);
                return View(midProjectCreateVM);
            }
            catch (ImageNullException ex)
            {
                ModelState.AddModelError("MidProjectCreateDto.ImageFiles", ex.Message);
                return View(midProjectCreateVM);
            }

            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                //TempData["Error"] = ("Proses uğursuz oldu!");
                return View(midProjectCreateVM);
            }

            return RedirectToAction("index", "MidProject");

        }


        public async Task<IActionResult> Edit(int id)
        {
            MidProjectEditViewModel MidProjectEditVM = new MidProjectEditViewModel();

            try
            {
                MidProjectEditVM = new MidProjectEditViewModel()
                {
                    MidProject = await _adminMidProjectEditServices.GetMidProject(id),
                    MidProjectImages = await _adminMidProjectEditServices.GetImages(id),
                    HighProjects = await _adminMidProjectCreateServices.GetAllHighProjects(),
                };

            }
            catch (NotFoundException)
            {
                return RedirectToAction("Index", "notfound");
            }
            catch (ItemNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Index", MidProjectEditVM);
            }
            catch (ItemNotFoundException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Index", MidProjectEditVM);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "notfound");
            }
            return View(MidProjectEditVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MidProject MidProject)
        {
            MidProjectEditViewModel MidProjectEditVM = new MidProjectEditViewModel();

            try
            {
                MidProjectEditVM = new MidProjectEditViewModel()
                {
                    MidProject = await _adminMidProjectEditServices.GetMidProject(MidProject.Id),
                    MidProjectImages = await _adminMidProjectEditServices.GetImages(MidProject.Id),
                };


                await _adminMidProjectEditServices.EditMidProject(MidProject);
            }

            catch (NotFoundException)
            {

                return RedirectToAction("Index", "notfound");
            }
            catch (ItemNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", MidProjectEditVM);
            }
            catch (ValueFormatExpception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", MidProjectEditVM);
            }
            catch (ItemNotFoundException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", MidProjectEditVM);

            }
            catch (ImageNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", MidProjectEditVM);

            }
            catch (ImageFormatException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", MidProjectEditVM);

            }
            catch (ImageCountException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", MidProjectEditVM);

            }
            catch (ValueAlreadyExpception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", MidProjectEditVM);

            }
            catch (Exception)
            {
                return RedirectToAction("Index", "notfound");
            }
            TempData["Success"] = ("Proses uğurlu oldu");
            return RedirectToAction("Index", "MidProject");
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _adminDeleteMidProjectServices.DeleteMidProject(id);
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
