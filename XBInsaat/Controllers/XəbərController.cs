using Microsoft.AspNetCore.Mvc;
using XBInsaat.Core.Entites;
using XBInsaat.Mvc.ViewModels;
using XBInsaat.Service.CustomExceptions;
using XBInsaat.Services.Dtos.User;
using XBInsaat.Services.Services.Interfaces.User;

namespace XBInsaat.Mvc.Controllers
{
    public class XəbərController : Controller
    {
        private readonly IHomeIndexServices _homeIndexServices;

        public XəbərController(IHomeIndexServices homeIndexServices)
        {
            _homeIndexServices = homeIndexServices;
        }
        public async Task<IActionResult> Index(int id)
        {
            ContactUsCreateDto contactUsCreateDto = new ContactUsCreateDto();
            HomeIndexContactUsViewModel homeIndexContactUsViewModel = new HomeIndexContactUsViewModel
            {
                ContactUsCreateDto = contactUsCreateDto,
                Settings = await _homeIndexServices.GetSettings(),

            };
            NewsViewModel newsViewModel = new NewsViewModel();


            try
            {
                newsViewModel = new NewsViewModel()
                {
                    News = await _homeIndexServices.GetNew(id),
                    Settings = await _homeIndexServices.GetSettings(),
                    HomeIndexContactUsViewModel = homeIndexContactUsViewModel,
                };
            }
            catch (ItemNotFoundException ex)
            {
                TempData["Error"] = (ex.Message);
                return RedirectToAction("index", "home", newsViewModel);
            }
            catch (Exception ex)
            {
                TempData["Error"] = (ex.Message);
                return RedirectToAction("index", "notfound");
            }
            return View(newsViewModel);
        }
    }
}
