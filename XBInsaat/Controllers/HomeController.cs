using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Resources;
using XBInsaat.Services.Services.Implementations.User;

namespace XBInsaat.Controllers
{
    public class HomeController : Controller
    {
        private LanguageService _localization;
        public HomeController(LanguageService localization)
        {
            _localization = localization;
        }
        public IActionResult Change(string text, string language)
        {
         


            return View();
        }
        public IActionResult Index()
        {
            ViewBag.Welcome = _localization.Getkey("GeneralText").Value;
            var currentCulture = Thread.CurrentThread.CurrentCulture.Name;
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