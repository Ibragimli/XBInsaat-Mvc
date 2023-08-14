using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Resources;
using System.Security.Policy;
using XBInsaat.Core.Entites;
using XBInsaat.Data.Datacontext;
using XBInsaat.Mvc.ViewModels;
using XBInsaat.Service.CustomExceptions;
using XBInsaat.Services.Dtos.User;
using XBInsaat.Services.Services.Implementations.User;
using XBInsaat.Services.Services.Interfaces;
using XBInsaat.Services.Services.Interfaces.User;

namespace XBInsaat.Controllers
{
    public class HomeController : Controller
    {
        private LanguageService _localization;
        private readonly IContactUsCreateServices _contactUsCreateServices;
        private readonly IEmailServices _emailServices;
        private readonly IStringLocalizerFactory _stringLocalizerFactory;
        private readonly IHomeIndexServices _homeIndexServices;
        private readonly DataContext _dataContext;

        public HomeController(LanguageService localization, IContactUsCreateServices contactUsCreateServices, IEmailServices emailServices, IStringLocalizerFactory stringLocalizerFactory, IHomeIndexServices homeIndexServices, DataContext dataContext)
        {
            _localization = localization;
            _contactUsCreateServices = contactUsCreateServices;
            _emailServices = emailServices;
            _stringLocalizerFactory = stringLocalizerFactory;
            _homeIndexServices = homeIndexServices;
            _dataContext = dataContext;
        }

        public async Task<IActionResult> Index(int newItemId = 2)
        {
            HomeIndexProjectsViewModel homeIndexProjectsViewModel = new HomeIndexProjectsViewModel();
            HomeIndexProjectViewModel homeIndexProjectViewModel = new HomeIndexProjectViewModel();
            HomeIndexContactUsViewModel homeIndexContactUsViewModel = new HomeIndexContactUsViewModel();
            HomeIndexNewsViewModel homeIndexNewsViewModel = new HomeIndexNewsViewModel();
            HomeIndexNewViewModel homeIndexNewViewModel = new HomeIndexNewViewModel();
            HomeIndexMidProjectViewModel homeIndexMidProjectViewModel = new HomeIndexMidProjectViewModel();

            HomeViewModel homeViewModel = new HomeViewModel();
            LoginViewModel loginVM = new LoginViewModel();


            try
            {
                loginVM = new LoginViewModel
                {
                    LoginPostDto = new LoginPostDto(),
                    Settings = await _homeIndexServices.GetSettings(),
                };
                News newItem = new News();

                ViewBag.Welcome = _localization.Getkey("GeneralText").Value;
                var currentCulture = Thread.CurrentThread.CurrentCulture.Name;
                homeIndexProjectsViewModel = new HomeIndexProjectsViewModel
                {
                    HighProjects = await _homeIndexServices.GetHighProjects(),
                    Settings = await _homeIndexServices.GetSettings(),
                };
                homeIndexProjectViewModel = new HomeIndexProjectViewModel
                {
                    HighProjects = await _homeIndexServices.GetHighProjects(),
                    Settings = await _homeIndexServices.GetSettings(),
                    MidProjects = await _homeIndexServices.GetMidProjects(),
                    HighProjectImages = await _homeIndexServices.GetHighProjectImages(),
                    MidProjectImages = await _homeIndexServices.GetMidProjectImages(),

                };
                homeIndexContactUsViewModel = new HomeIndexContactUsViewModel
                {
                    ContactUsCreateDto = new ContactUsCreateDto(),
                    Settings = await _homeIndexServices.GetSettings(),
                };
                homeIndexMidProjectViewModel = new HomeIndexMidProjectViewModel
                {
                    Settings = await _homeIndexServices.GetSettings(),
                    MidProjects = await _homeIndexServices.GetMidProjects(),
                    MidProjectImages = await _homeIndexServices.GetMidProjectImages(),

                };

                homeIndexNewsViewModel = new HomeIndexNewsViewModel
                {
                    News = await _homeIndexServices.GetNews(),
                    Settings = await _homeIndexServices.GetSettings(),
                };
                homeIndexNewViewModel = new HomeIndexNewViewModel
                {
                    News = await _homeIndexServices.GetNews(),
                    Settings = await _homeIndexServices.GetSettings(),
                    NewsImages = await _homeIndexServices.GetNewsImages(),
                    //New = await _homeIndexServices.GetNew(newItemId),

                };

                homeViewModel = new HomeViewModel
                {
                    ContactUsCreateDto = new ContactUsCreateDto(),
                    HighProjects = await _homeIndexServices.GetHighProjects(),
                    MidProjects = await _homeIndexServices.GetMidProjects(),
                    News = await _homeIndexServices.GetNews(),
                    Settings = await _homeIndexServices.GetSettings(),
                    XBServices = await _homeIndexServices.GetXBServices(),
                    HomeIndexProjectsViewModel = homeIndexProjectsViewModel,
                    HomeIndexContactUsViewModel = homeIndexContactUsViewModel,
                    HomeIndexProjectViewModel = homeIndexProjectViewModel,
                    HomeIndexNewsViewModel = homeIndexNewsViewModel,
                    HomeIndexNewViewModel = homeIndexNewViewModel,
                    RevolutionSliders = await _homeIndexServices.GetRevolutionSliders(),
                    HomeIndexMidProjectViewModel = homeIndexMidProjectViewModel,
                    LoginViewModel = loginVM
                };
            }
            catch (ItemNotFoundException ex)
            {
                TempData["Error"] = (ex.Message);
                return RedirectToAction("index", "home", homeViewModel);
            }
            catch (Exception ex)
            {
                TempData["Error"] = (ex.Message);
                return RedirectToAction("index", "notfound");
            }
            return View(homeViewModel);
        }
        private ActionResult GetNewsJsonData(int newItemId = 2, string language = "Az")
        {
            News data = _dataContext.News.Include(x => x.NewsImages).FirstOrDefault(x => x.Id == newItemId);
            var lang = "";
            if (language == "Az")
                lang = data.TextAz;
            else if (language == "En")
                lang = data.TextEn;
            else lang = data.TextRu;

            var instagramUrl = "#!";
            if (data.InstagramUrl != null)
                instagramUrl = data.InstagramUrl;
            var jsonData = new
            {
                Id = data.Id,
                Title = data.Title,
                Text = lang,
                Language = language,
                InstagramUrl = instagramUrl,
                // Diğer News özelliklerini buraya ekleyin

                NewsImages = data.NewsImages.Select(image => new
                {
                    ImageUrl = image.Image,
                    IsPoster = image.IsPoster
                    // Diğer NewsImage özelliklerini buraya ekleyin
                }).ToList() // NewsImages verilerini liste olarak dönüştürün
            };
            // Veriyi JSON formatına dönüştürün
            //jsonData = JsonConvert.SerializeObject(data);
            var jsonString = JsonConvert.SerializeObject(jsonData);


            return Json(jsonString);
        }


        public ActionResult GetProjectsJsonData(int projectItemId = 2, string language = "Az")
        {
            HighProject data = _dataContext.HighProjects.Include(x => x.HighProjectImages).Include(x => x.MidProjects).ThenInclude(x => x.MidProjectImages).FirstOrDefault(x => x.Id == projectItemId);
            var lang = "";
            if (language == "Az")
                lang = data.DescribeAz;
            else if (language == "En")
                lang = data.DescribeEn;
            else lang = data.DescribeRu;
            //var instagramUrl = "#!";
            //if (data.InstagramUrl != null)
            //    instagramUrl = data.InstagramUrl;
            var jsonData = new
            {
                Id = data.Id,
                Name = data.Name,
                Describe = lang,
                Language = language,
                //InstagramUrl = instagramUrl,
                // Diğer News özelliklerini buraya ekleyin

                HighProjectImages = data.HighProjectImages.Select(image => new
                {

                    ImageUrl = image.Image,
                    IsPoster = image.IsPoster,
                    ImageTpye = image.Image.Substring(image.Image.LastIndexOf(".") + 1),
                    // Diğer NewsImage özelliklerini buraya ekleyin
                }).ToList(), // NewsImages verilerini liste olarak dönüştürün

                MidProjects = data.MidProjects.Select(midProject => new
                {
                    MidImageUrl = midProject.MidProjectImages.FirstOrDefault(x => x.IsPoster && x.MidProjectId == midProject.Id)?.Image,
                    MidProjectId = midProject.Id,
                    MidProjectName = midProject.Name,
                    // Diğer NewsImage özelliklerini buraya ekleyin
                }).ToList() // NewsImages verilerini liste olarak dönüştürün
            };
            var jsonString = JsonConvert.SerializeObject(jsonData);


            return Json(jsonString);
        }
        private ActionResult GetMidProjectsJsonData(int midProjectItemId = 2, string language = "Az")
        {
            MidProject data = _dataContext.MidProjects.Include(x => x.MidProjectImages).FirstOrDefault(x => x.Id == midProjectItemId);
            var lang = "";
            if (language == "Az")
                lang = data.DescribeAz;
            else if (language == "En")
                lang = data.DescribeEn;
            else lang = data.DescribeRu;

            //var instagramUrl = "#!";
            //if (data.InstagramUrl != null)
            //    instagramUrl = data.InstagramUrl;
            var jsonData = new
            {
                Id = data.Id,
                Name = data.Name,
                Describe = lang,
                Language = language,
                //InstagramUrl = instagramUrl,
                // Diğer News özelliklerini buraya ekleyin

                MidProjectImages = data.MidProjectImages.Select(image => new
                {
                    ImageUrl = image.Image,
                    IsPoster = image.IsPoster,
                    ImageTpye = image.Image.Substring(image.Image.LastIndexOf(".") + 1),

                    // Diğer NewsImage özelliklerini buraya ekleyin
                }).ToList(), // NewsImages verilerini liste olarak dönüştürün
            };
            var jsonString = JsonConvert.SerializeObject(jsonData);

            return Json(jsonString);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ContactUs(ContactUsCreateDto contactUsCreateDto, int newItemId = 2)
        {

            HomeIndexProjectsViewModel homeIndexProjectsViewModel = new HomeIndexProjectsViewModel();
            HomeIndexProjectViewModel homeIndexProjectViewModel = new HomeIndexProjectViewModel();
            HomeIndexContactUsViewModel homeIndexContactUsViewModel = new HomeIndexContactUsViewModel();
            HomeIndexNewsViewModel homeIndexNewsViewModel = new HomeIndexNewsViewModel();
            HomeIndexNewViewModel homeIndexNewViewModel = new HomeIndexNewViewModel();

            HomeIndexMidProjectViewModel homeIndexMidProjectViewModel = new HomeIndexMidProjectViewModel();
            HomeViewModel homeViewModel = new HomeViewModel();
            try
            {
                News newItem = new News();

                ViewBag.Welcome = _localization.Getkey("GeneralText").Value;
                var currentCulture = Thread.CurrentThread.CurrentCulture.Name;
                homeIndexProjectsViewModel = new HomeIndexProjectsViewModel
                {
                    HighProjects = await _homeIndexServices.GetHighProjects(),
                    Settings = await _homeIndexServices.GetSettings(),
                };
                homeIndexProjectViewModel = new HomeIndexProjectViewModel
                {
                    HighProjects = await _homeIndexServices.GetHighProjects(),
                    Settings = await _homeIndexServices.GetSettings(),
                    MidProjects = await _homeIndexServices.GetMidProjects(),
                    HighProjectImages = await _homeIndexServices.GetHighProjectImages(),
                    MidProjectImages = await _homeIndexServices.GetMidProjectImages(),

                };
                homeIndexContactUsViewModel = new HomeIndexContactUsViewModel
                {
                    ContactUsCreateDto = new ContactUsCreateDto(),
                    Settings = await _homeIndexServices.GetSettings(),
                };

                homeIndexNewsViewModel = new HomeIndexNewsViewModel
                {
                    News = await _homeIndexServices.GetNews(),
                    Settings = await _homeIndexServices.GetSettings(),
                };
                homeIndexNewViewModel = new HomeIndexNewViewModel
                {
                    News = await _homeIndexServices.GetNews(),
                    Settings = await _homeIndexServices.GetSettings(),
                    NewsImages = await _homeIndexServices.GetNewsImages(),
                    //New = await _homeIndexServices.GetNew(newItemId),

                };
                homeIndexMidProjectViewModel = new HomeIndexMidProjectViewModel
                {
                    Settings = await _homeIndexServices.GetSettings(),
                    MidProjects = await _homeIndexServices.GetMidProjects(),
                    MidProjectImages = await _homeIndexServices.GetMidProjectImages(),

                };
                homeViewModel = new HomeViewModel
                {
                    ContactUsCreateDto = new ContactUsCreateDto(),
                    HighProjects = await _homeIndexServices.GetHighProjects(),
                    MidProjects = await _homeIndexServices.GetMidProjects(),
                    News = await _homeIndexServices.GetNews(),
                    Settings = await _homeIndexServices.GetSettings(),
                    XBServices = await _homeIndexServices.GetXBServices(),
                    HomeIndexProjectsViewModel = homeIndexProjectsViewModel,
                    HomeIndexContactUsViewModel = homeIndexContactUsViewModel,
                    HomeIndexProjectViewModel = homeIndexProjectViewModel,
                    HomeIndexNewsViewModel = homeIndexNewsViewModel,
                    HomeIndexNewViewModel = homeIndexNewViewModel,
                    RevolutionSliders = await _homeIndexServices.GetRevolutionSliders(),
                    HomeIndexMidProjectViewModel = homeIndexMidProjectViewModel,

                };

                await _contactUsCreateServices.ValuesCheck(contactUsCreateDto);

                //Email
                string body = string.Empty;

                using (StreamReader reader = new StreamReader("wwwroot/templates/contactEmail.html"))
                {
                    body = reader.ReadToEnd();
                }

                await _contactUsCreateServices.PhoneNumberCheck(contactUsCreateDto.PhoneNumber);
                await _contactUsCreateServices.EmailCheck(contactUsCreateDto.Email);
                await _contactUsCreateServices.ContactUsCreate(contactUsCreateDto);

                body = body.Replace("{{phonenumber}}", contactUsCreateDto.PhoneNumber);
                body = body.Replace("{{fullname}}", contactUsCreateDto.Fullname);
                body = body.Replace("{{email}}", contactUsCreateDto.Email);
                body = body.Replace("{{message}}", contactUsCreateDto.Message);
                await _emailServices.Send("info@xbinsaat.az", "Xarıbulbul elaqe mesaji", body);
            }
            catch (ItemNotFoundException ex)
            {
                TempData["Error"] = (ex.Message);
                return RedirectToAction("index", "home", homeViewModel);
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