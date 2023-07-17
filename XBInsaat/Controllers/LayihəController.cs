using Microsoft.AspNetCore.Mvc;
using XBInsaat.Core.Entites;
using XBInsaat.Mvc.ViewModels;
using XBInsaat.Service.CustomExceptions;
using XBInsaat.Services.Dtos.User;
using XBInsaat.Services.Services.Interfaces.User;

namespace XBInsaat.Mvc.Controllers
{
    public class LayihəController : Controller
    {
        private readonly IHomeIndexServices _homeIndexServices;

        public LayihəController(IHomeIndexServices homeIndexServices)
        {
            _homeIndexServices = homeIndexServices;
        }
        public async Task<IActionResult> Index(int id)
        {
            ProjectViewModel projectViewModel = new ProjectViewModel();
            ContactUsCreateDto contactUsCreateDto = new ContactUsCreateDto();
            HomeIndexContactUsViewModel homeIndexContactUsViewModel = new HomeIndexContactUsViewModel
            {
                ContactUsCreateDto = contactUsCreateDto,
                Settings = await _homeIndexServices.GetSettings(),

            };
            try
            {
                projectViewModel = new ProjectViewModel
                {
                    HighProject = await _homeIndexServices.GetHighProject(id),
                    MidProjects = await _homeIndexServices.GetMidProjects(),
                    Settings = await _homeIndexServices.GetSettings(),
                    HomeIndexContactUsViewModel = homeIndexContactUsViewModel,
                };
            }
            catch (ItemNotFoundException ex)
            {
                TempData["Error"] = (ex.Message);
                return RedirectToAction("index", "home", projectViewModel);
            }
            catch (Exception ex)
            {
                TempData["Error"] = (ex.Message);
                return RedirectToAction("index", "notfound");
            }
            return View(projectViewModel);
        }
    }
}
