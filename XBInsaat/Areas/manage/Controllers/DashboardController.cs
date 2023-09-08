using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using XBInsaat.Mvc.Areas.manage.ViewModels;
using XBInsaat.Services.Services.Interfaces.Area.Dashboard;

namespace XBInsaat.Mvc.Areas.manage.Controllers
{
    [Area("manage")]
    //[Authorize(Roles = "SuperAdmin,Admin,Editor,Viewer")]

    public class DashboardController : Controller
    {
        private readonly IDashboardServices _dashboardServices;

        public DashboardController(IDashboardServices dashboardServices)
        {
            _dashboardServices = dashboardServices;
        }
        public async Task<IActionResult> Index()
        {
            DashboardViewModel dashboardVM = new DashboardViewModel();
            try
            {
                dashboardVM = new DashboardViewModel
                {
                    ContactUsCount = await _dashboardServices.GetContactCount(),
                    CareerCount = await _dashboardServices.GetCareerCount(),
                    CareerMonthCount = await _dashboardServices.GetMonthCareerCount(),
                    ContactMonthCount = await _dashboardServices.GetMonthContactCount(),
                };
            }
            catch (Exception)
            {
                return RedirectToAction("index", "notfound");
            }
            return View(dashboardVM);
        }
    }
}
