using Microsoft.AspNetCore.Mvc;

namespace XBInsaat.Mvc.Areas.manage.Controllers
{
    public class ContactUsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
