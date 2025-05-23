﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using XBInsaat.Core.Entites;
using XBInsaat.Mvc.Areas.manage.ViewModels;
using XBInsaat.Service.CustomExceptions;
using XBInsaat.Service.Helper;
using XBInsaat.Service.HelperService.Interfaces;
using XBInsaat.Services.Dtos.Area;
using XBInsaat.Services.HelperService.Interfaces;
using XBInsaat.Services.Services.Interfaces.Area;

namespace XBInsaat.Mvc.Areas.manage.Controllers
{

    [Area("manage")]
    //[Authorize(Roles = "SuperAdmin,Admin,Editor,Viewer")]
    public class NewsController : Controller
    {
        private readonly ILoggerServices _loggerServices;
        private readonly UserManager<AppUser> _userManager;
        private readonly IAdminNewsIndexServices _adminNewsIndexServices;
        private readonly IManageImageHelper _manageImageHelper;
        private readonly IAdminDeleteNewsServices _adminDeleteNewsServices;
        private readonly IAdminNewsEditServices _adminNewsEditServices;
        private readonly IAdminNewsCreateServices _adminNewsCreateServices;

        public NewsController(ILoggerServices loggerServices, UserManager<AppUser> userManager, IAdminNewsIndexServices adminNewsIndexServices, IManageImageHelper manageImageHelper, IAdminDeleteNewsServices adminDeleteNewsServices, IAdminNewsEditServices adminNewsEditServices, IAdminNewsCreateServices adminNewsCreateServices)
        {
            _loggerServices = loggerServices;
            _userManager = userManager;
            _adminNewsIndexServices = adminNewsIndexServices;
            _manageImageHelper = manageImageHelper;
            _adminDeleteNewsServices = adminDeleteNewsServices;
            _adminNewsEditServices = adminNewsEditServices;
            _adminNewsCreateServices = adminNewsCreateServices;
        }
        public IActionResult Index(int page = 1, string name = null)
        {
            NewsIndexViewModel NewsIndexVM = new NewsIndexViewModel();
            try
            {
                var News = _adminNewsIndexServices.GetPoster(name);
                NewsIndexVM = new NewsIndexViewModel
                {
                    News = PagenetedList<News>.Create(News, page, 5),
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
            return View(NewsIndexVM);
        }
        public IActionResult Create()
        {

            NewsCreateDto NewsCreateDto = new NewsCreateDto();
            return View(NewsCreateDto);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Create(NewsCreateDto NewsCreateDto)
        {
            try
            {
                _adminNewsCreateServices.DtoCheck(NewsCreateDto);
                // CheckImage
                _manageImageHelper.ImagesCheck(NewsCreateDto.ImageFiles);
                var News = await _adminNewsCreateServices.CreateProject(NewsCreateDto);
                await _adminNewsCreateServices.CreateImageFormFile(NewsCreateDto.ImageFiles, News.Id);

                //Logger
                AppUser user = User.Identity.IsAuthenticated ? _userManager.Users.FirstOrDefault(x => x.UserName == User.Identity.Name && x.IsAdmin) : null;
                if (user == null)
                    throw new UserNotFoundException("Error bas verdi!");
                await _loggerServices.LoggerCreate("News", "Create", user.FullName, user.UserName, NewsCreateDto.TitleAz);
            }
            catch (ItemNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
            catch (UserNotFoundException)
            {
                return RedirectToAction("index", "notfound");
            }

            catch (ValueFormatExpception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
            catch (ImageFormatException ex)
            {
                ModelState.AddModelError("NewsCreateDto.ImageFiles", ex.Message);
                return View();
            }
            catch (ImageNullException ex)
            {
                ModelState.AddModelError("NewsCreateDto.ImageFiles", ex.Message);
                return View();
            }

            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                //TempData["Error"] = ("Proses uğursuz oldu!");
                return View();
            }

            return RedirectToAction("index", "News");

        }


        public async Task<IActionResult> Edit(int id)
        {
            NewsEditViewModel NewsEditVM = new NewsEditViewModel();

            try
            {
                NewsEditVM = new NewsEditViewModel()
                {
                    News = await _adminNewsEditServices.GetNews(id),
                    NewsImages = await _adminNewsEditServices.GetImages(id),
                };

            }
            catch (NotFoundException)
            {

                return RedirectToAction("Index", "notfound");
            }
            catch (ItemNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Index", NewsEditVM);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "notfound");
            }
            return View(NewsEditVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(News News)
        {
            NewsEditViewModel NewsEditVM = new NewsEditViewModel();

            try
            {
                NewsEditVM = new NewsEditViewModel()
                {
                    News = await _adminNewsEditServices.GetNews(News.Id),
                    NewsImages = await _adminNewsEditServices.GetImages(News.Id),
                };


                await _adminNewsEditServices.EditNews(News);

                //Logger
                var product = await _adminNewsEditServices.GetNews(News.Id);
                AppUser user = User.Identity.IsAuthenticated ? _userManager.Users.FirstOrDefault(x => x.UserName == User.Identity.Name && x.IsAdmin) : null;
                if (user == null)
                    throw new UserNotFoundException("Error bas verdi!");
                await _loggerServices.LoggerCreate("News", "Edit", user.FullName, user.UserName, product.TitleAz);
            }

            catch (NotFoundException)
            {

                return RedirectToAction("Index", "notfound");
            }
            catch (UserNotFoundException)
            {
                return RedirectToAction("index", "notfound");
            }
            catch (ValueFormatExpception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", NewsEditVM);
            }
            catch (ItemNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", NewsEditVM);

            }
            catch (ImageNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", NewsEditVM);

            }
            catch (ImageFormatException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", NewsEditVM);

            }
            catch (ImageCountException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", NewsEditVM);

            }
            catch (ValueAlreadyExpception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", NewsEditVM);
            }
            catch (ItemFormatException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Edit", NewsEditVM);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "notfound");
            }
            TempData["Success"] = ("Proses uğurlu oldu");
            return RedirectToAction("Index", "News");


        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                //Logger
                var product = await _adminNewsEditServices.GetNews(id);
                AppUser user = User.Identity.IsAuthenticated ? _userManager.Users.FirstOrDefault(x => x.UserName == User.Identity.Name && x.IsAdmin) : null;
                if (user == null)
                    throw new UserNotFoundException("Error bas verdi!");
                await _loggerServices.LoggerCreate("News", "Delete", user.FullName, user.UserName, product.TitleAz);

                await _adminDeleteNewsServices.DeleteNews(id);
            }
            catch (ItemNotFoundException ex)
            {
                TempData["Error"] = (ex.Message);
                return Ok();
            }
            catch (UserNotFoundException)
            {
                return RedirectToAction("index", "notfound");
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
