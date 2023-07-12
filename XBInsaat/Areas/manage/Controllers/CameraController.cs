using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace XBInsaat.Mvc.Areas.manage.Controllers
{

    [Area("manage")]
    [Authorize(Roles = "SuperAdmin")]
    public class CameraController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
