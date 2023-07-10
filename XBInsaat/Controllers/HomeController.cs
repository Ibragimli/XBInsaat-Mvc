using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Diagnostics;
using System.Resources;
using XBInsaat.Data.Datacontext;
using XBInsaat.Mvc.ViewModels;
using XBInsaat.Service.CustomExceptions;
using XBInsaat.Services.Dtos.User;
using XBInsaat.Services.Services.Implementations.User;
using XBInsaat.Services.Services.Interfaces.User;

namespace XBInsaat.Controllers
{
    public class HomeController : Controller
    {
        private LanguageService _localization;
        private readonly IContactUsCreateServices _contactUsCreateServices;
        private readonly IStringLocalizerFactory _stringLocalizerFactory;
        private readonly IHomeIndexServices _homeIndexServices;

        public HomeController(LanguageService localization, IContactUsCreateServices contactUsCreateServices, IStringLocalizerFactory stringLocalizerFactory, IHomeIndexServices homeIndexServices)
        {
            _localization = localization;
            _contactUsCreateServices = contactUsCreateServices;
            _stringLocalizerFactory = stringLocalizerFactory;
            _homeIndexServices = homeIndexServices;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Welcome = _localization.Getkey("GeneralText").Value;
            var currentCulture = Thread.CurrentThread.CurrentCulture.Name;
            HomeIndexProjectsViewModel homeIndexProjectsViewModel = new HomeIndexProjectsViewModel
            {
                HighProjects = await _homeIndexServices.GetHighProjects(),
                Settings = await _homeIndexServices.GetSettings(),
            };
            HomeIndexContactUsViewModel homeIndexContactUsViewModel = new HomeIndexContactUsViewModel
            {
                ContactUsCreateDto = new ContactUsCreateDto(),
                Settings = await _homeIndexServices.GetSettings(),
            };
            HomeViewModel homeViewModel = new HomeViewModel
            {
                ContactUsCreateDto = new ContactUsCreateDto(),
                HighProjects = await _homeIndexServices.GetHighProjects(),
                MidProjects = await _homeIndexServices.GetMidProjects(),
                News = await _homeIndexServices.GetNews(),
                Settings = await _homeIndexServices.GetSettings(),
                XBServices = await _homeIndexServices.GetXBServices(),
                HomeIndexProjectsViewModel = homeIndexProjectsViewModel,
                HomeIndexContactUsViewModel = homeIndexContactUsViewModel,
            };
            //var project =  _dataContext.Projects.Where(x => x.IsDelete == false).ToList();
            //return View(project);
            return View(homeViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ContactUs(ContactUsCreateDto contactUsCreateDto)
        {

            HomeIndexProjectsViewModel homeIndexProjectsViewModel = new HomeIndexProjectsViewModel
            {
                HighProjects = await _homeIndexServices.GetHighProjects(),
                Settings = await _homeIndexServices.GetSettings(),
            };
            HomeIndexContactUsViewModel homeIndexContactUsViewModel = new HomeIndexContactUsViewModel
            {
                ContactUsCreateDto = new ContactUsCreateDto(),
                Settings = await _homeIndexServices.GetSettings(),
            };
            HomeViewModel homeViewModel = new HomeViewModel
            {
                ContactUsCreateDto = new ContactUsCreateDto(),
                HighProjects = await _homeIndexServices.GetHighProjects(),
                MidProjects = await _homeIndexServices.GetMidProjects(),
                News = await _homeIndexServices.GetNews(),
                Settings = await _homeIndexServices.GetSettings(),
                XBServices = await _homeIndexServices.GetXBServices(),
                HomeIndexProjectsViewModel = homeIndexProjectsViewModel,


            };
            try
            {
                await _contactUsCreateServices.PhoneNumberCheck(contactUsCreateDto.PhoneNumber);
                await _contactUsCreateServices.EmailCheck(contactUsCreateDto.Email);
                await _contactUsCreateServices.ContactUsCreate(contactUsCreateDto);

            }
            catch (ItemFormatException ex)
            {
                TempData["Error"] = (ex.Message);
                return RedirectToAction("index", "home", homeViewModel);
            }
            catch (Exception)
            {
                return RedirectToAction("index", "notfound");
            }
            TempData["Success"] = ("Məktub göndərildi");
            return View("index", homeViewModel);
        }


        public IActionResult ChangeLanguage(string culture)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)), new CookieOptions()
                {
                    Expires = DateTimeOffset.UtcNow.AddYears(1)
                });
            return Redirect(Request.Headers["Referer"].ToString());
        }


    }
}