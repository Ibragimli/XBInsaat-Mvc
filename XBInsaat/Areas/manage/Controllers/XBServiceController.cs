using Microsoft.AspNetCore.Mvc;

namespace XBInsaat.Mvc.Areas.manage.Controllers
{
    public class XBServiceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
