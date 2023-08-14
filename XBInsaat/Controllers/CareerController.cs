using Microsoft.AspNetCore.Mvc;
using XBInsaat.Mvc.ViewModels;
using XBInsaat.Service.CustomExceptions;
using XBInsaat.Services.Dtos.User;
using XBInsaat.Services.Services.Implementations.User;
using XBInsaat.Services.Services.Interfaces.User;

namespace XBInsaat.Mvc.Controllers
{
    public class CareerController : Controller
    {
        private readonly IHomeIndexServices _homeIndexServices;
        private readonly ICareerServices _careerServices;

        public CareerController(IHomeIndexServices homeIndexServices,ICareerServices careerServices)
        {
            _homeIndexServices = homeIndexServices;
            _careerServices = careerServices;
        }
        public async Task<IActionResult> Index()
        {
            CareerViewModel careerVM = new CareerViewModel();
            try
            {
                HomeIndexContactUsViewModel homeIndexContactUsViewModel = new HomeIndexContactUsViewModel
                {
                    ContactUsCreateDto = new ContactUsCreateDto(),
                    Settings = await _homeIndexServices.GetSettings(),

                };
                LoginViewModel loginVM = new LoginViewModel
                {
                    LoginPostDto = new LoginPostDto(),
                    Settings = await _homeIndexServices.GetSettings(),

                };
                careerVM = new CareerViewModel()
                {
                    CareerPostDto = new CareerPostDto(),
                    Settings = await _homeIndexServices.GetSettings(),
                    HomeIndexContactUsViewModel = homeIndexContactUsViewModel,
                    LoginViewModel = loginVM,
                };

            }
            catch (ItemNotFoundException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return RedirectToAction("index", "career", careerVM);
            }
            catch (ItemFormatException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return RedirectToAction("index", "career", careerVM);
            }
            catch (ItemNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return RedirectToAction("index", "career", careerVM);
            }
            catch (Exception ex)
            {
                TempData["Error"] = (ex.Message);
                return RedirectToAction("index", "notfound");
            }
            return View(careerVM);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> CareerSend(CareerPostDto CareerPostDto)
        {
            CareerViewModel careerVM = new CareerViewModel();
            try
            {
                HomeIndexContactUsViewModel homeIndexContactUsViewModel = new HomeIndexContactUsViewModel
                {
                    ContactUsCreateDto = new ContactUsCreateDto(),
                    Settings = await _homeIndexServices.GetSettings(),

                };
                LoginViewModel loginVM = new LoginViewModel
                {
                    LoginPostDto = new LoginPostDto(),
                    Settings = await _homeIndexServices.GetSettings(),

                };
                careerVM = new CareerViewModel()
                {
                    CareerPostDto = new CareerPostDto(),
                    Settings = await _homeIndexServices.GetSettings(),
                    HomeIndexContactUsViewModel = homeIndexContactUsViewModel,
                    LoginViewModel = loginVM,
                };
                 _careerServices.CheckValue(CareerPostDto);
                await _careerServices.SendCV(CareerPostDto);

            }
            catch (ItemNotFoundException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("index", careerVM);
            }
            catch (ItemFormatException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("index",  careerVM);
            }
            catch (ItemNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("index", careerVM);
            }
            catch (Exception )
            {
                //TempData["Error"] = (ex.Message);
                return RedirectToAction("index", "notfound");
            }
            TempData["Success"] = "Sorğunuz göndərildi";
            return RedirectToAction("index", "career");

        }


    }
}
