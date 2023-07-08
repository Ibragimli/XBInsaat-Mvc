using Microsoft.AspNetCore.Mvc;

namespace XBInsaat.Mvc.Areas.manage.Controllers
{
    public class NewsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
