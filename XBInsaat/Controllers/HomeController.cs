using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Diagnostics;
using System.Resources;
using XBInsaat.Data.Datacontext;
using XBInsaat.Services.Services.Implementations.User;

namespace XBInsaat.Controllers
{
    public class HomeController : Controller
    {
        private LanguageService _localization;
        private readonly IStringLocalizerFactory _stringLocalizerFactory;
        private readonly DataContext _dataContext;

        public HomeController(LanguageService localization, IStringLocalizerFactory stringLocalizerFactory, DataContext dataContext)
        {
            _localization = localization;
            _stringLocalizerFactory = stringLocalizerFactory;
            _dataContext = dataContext;
        }

        public IActionResult Index()
        {
            ViewBag.Welcome = _localization.Getkey("GeneralText").Value;
            var currentCulture = Thread.CurrentThread.CurrentCulture.Name;

            //var project =  _dataContext.Projects.Where(x => x.IsDelete == false).ToList();
            //return View(project);
            return View();
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