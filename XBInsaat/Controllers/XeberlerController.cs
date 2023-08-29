using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Drawing.Printing;
using XBInsaat.Core.Entites;
using XBInsaat.Data.Datacontext;
using XBInsaat.Mvc.ViewModels;
using XBInsaat.Service.CustomExceptions;
using XBInsaat.Services.Dtos.User;
using XBInsaat.Services.Services.Interfaces.User;

namespace XBInsaat.Mvc.Controllers
{
    public class XeberlerController : Controller
    {
        private readonly IHomeIndexServices _homeIndexServices;
        private readonly DataContext _context;

        public XeberlerController(IHomeIndexServices homeIndexServices, DataContext context)
        {
            _homeIndexServices = homeIndexServices;
            _context = context;

        }
        public async Task<IActionResult> Index()
        {
            ContactUsCreateDto contactUsCreateDto = new ContactUsCreateDto();

            NewsViewModel newsViewModel = new NewsViewModel();


            try
            {
                HomeIndexContactUsViewModel homeIndexContactUsViewModel = new HomeIndexContactUsViewModel
                {
                    ContactUsCreateDto = contactUsCreateDto,
                    Settings = await _homeIndexServices.GetSettings(),
                    Localizations = await _homeIndexServices.GetLocalizations(),
                };
                LoginViewModel loginVM = new LoginViewModel
                {
                    LoginPostDto = new LoginPostDto(),
                    Settings = await _homeIndexServices.GetSettings(),
                    Localizations = await _homeIndexServices.GetLocalizations(),

                };
                newsViewModel = new NewsViewModel()
                {
                    News = await _homeIndexServices.GetNews(),
                    Settings = await _homeIndexServices.GetSettings(),
                    HomeIndexContactUsViewModel = homeIndexContactUsViewModel,
                    LoginViewModel = loginVM,
                    Localizations = await _homeIndexServices.GetLocalizations(),
                };

            }
            catch (ItemNotFoundException ex)
            {
                TempData["Error"] = (ex.Message);
                return RedirectToAction("index", "home", newsViewModel);
            }
            catch (Exception ex)
            {
                TempData["Error"] = (ex.Message);
                return RedirectToAction("index", "notfound");
            }
            return View(newsViewModel);
        }
        public async Task<IActionResult> GetDataNews(int page = 1, int pageSize = 5, string language = "Az")
        {


            //var news = _context.News.Include(x => x.NewsImages).Where(x => !x.IsDelete).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            var news = await _homeIndexServices.GetNewsData(page, pageSize);
            var lang = "";
            var title = "";
            List<object> list = new List<object>();
            foreach (var item in news)
            {
                if (language == "Az")
                    lang = item.TextAz;
                else if (language == "En")
                    lang = item.TextEn;
                else lang = item.TextRu;

                if (language == "Az")
                    title = item.TitleAz;
                else if (language == "En")
                    title = item.TitleEn;
                else title = item.TitleRu;


                var instagramUrl = "#!";
                if (item.InstagramUrl != null)
                    instagramUrl = item.InstagramUrl;
                var jsonData = new
                {
                    Id = item.Id,
                    Title = title,
                    Text = lang,
                    IsLoad = false,
                    Language = language,
                    InstagramUrl = instagramUrl,
                    CreatedTime = item.CreatedDate.ToString("MM/dd/yy HH:mm"),
                    NewsImages = item.NewsImages.Select(image => new
                    {
                        ImageUrl = image.Image,
                        IsPoster = image.IsPoster
                    }).ToList()
                };
                list.Add(jsonData);
            }

            var jsonString = JsonConvert.SerializeObject(list);
            return Json(jsonString);
        }
    }
}
