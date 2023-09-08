using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using XBInsaat.Mvc.Areas.manage.ViewModels;
using XBInsaat.Service.CustomExceptions;
using XBInsaat.Service.Helper;
using XBInsaat.Services.Services.Interfaces.Area.Careers;
using System.Data;

namespace XBInsaat.Mvc.Areas.manage.Controllers
{
    [Area("manage")]
    //[Authorize(Roles = "SuperAdmin,Admin,Editor,Viewer")]

    public class CareerController : Controller
    {
        private readonly IAdminCareerIndexServices _adminCareerIndexServices;

        public CareerController(IAdminCareerIndexServices adminCareerIndexServices)
        {
            _adminCareerIndexServices = adminCareerIndexServices;
        }
        public IActionResult Index(int page = 1, string name = null)
        {
            CareerIndexViewModel CareerIndexVM = new CareerIndexViewModel();
            try
            {
                var Career = _adminCareerIndexServices.GetPoster(name);
                CareerIndexVM = new CareerIndexViewModel
                {
                    Careers = PagenetedList<XBInsaat.Core.Entites.Career>.Create(Career, page, 10),
                };
            }
            catch (NotFoundException)
            {
                return RedirectToAction("index", "notfound");
            }

            catch (Exception)
            {
                return RedirectToAction("index", "notfound");
            }
            return View(CareerIndexVM);
        }

    }
}
