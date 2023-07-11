using Microsoft.AspNetCore.Mvc;

namespace XBInsaat.Mvc.Controllers
{
    public class NotFoundController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
