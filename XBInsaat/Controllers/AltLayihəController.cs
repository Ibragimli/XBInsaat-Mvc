using Microsoft.AspNetCore.Mvc;
using XBInsaat.Core.Entites;
using XBInsaat.Mvc.ViewModels;
using XBInsaat.Service.CustomExceptions;
using XBInsaat.Services.Dtos.User;
using XBInsaat.Services.Services.Interfaces.User;

namespace XBInsaat.Mvc.Controllers
{
    public class AltLayihəController : Controller
    {
        private readonly IHomeIndexServices _homeIndexServices;

        public AltLayihəController(IHomeIndexServices homeIndexServices)
        {
            _homeIndexServices = homeIndexServices;
        }
        public async Task<IActionResult> Index(int id)
        {
            MidProjectViewModel midProjectViewModel = new MidProjectViewModel();
            ContactUsCreateDto contactUsCreateDto = new ContactUsCreateDto();
          

            try
            {
                HomeIndexContactUsViewModel homeIndexContactUsViewModel = new HomeIndexContactUsViewModel
                {
                    ContactUsCreateDto = contactUsCreateDto,
                    Settings = await _homeIndexServices.GetSettings(),

                };
                LoginViewModel loginVM = new LoginViewModel
                {
                    LoginPostDto = new LoginPostDto(),
                    Settings = await _homeIndexServices.GetSettings(),

                };
                midProjectViewModel = new MidProjectViewModel
                {
                    MidProject = await _homeIndexServices.GetMidProject(id),
                    Settings = await _homeIndexServices.GetSettings(),
                    HomeIndexContactUsViewModel = homeIndexContactUsViewModel,
                    LoginViewModel = loginVM,
                };
            }
            catch (ItemNotFoundException ex)
            {
                TempData["Error"] = (ex.Message);
                return RedirectToAction("index", "home", midProjectViewModel);
            }
            catch (Exception ex)
            {
                TempData["Error"] = (ex.Message);
                return RedirectToAction("index", "notfound");
            }
            return View(midProjectViewModel);
        }
    }
}
