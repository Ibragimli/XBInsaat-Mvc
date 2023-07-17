using Microsoft.AspNetCore.Mvc;
using XBInsaat.Core.Entites;
using XBInsaat.Mvc.ViewModels;
using XBInsaat.Service.CustomExceptions;
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
            var newItem = new News();

            try
            {
                newItem = await _homeIndexServices.GetNew(id);
            }
            catch (ItemNotFoundException ex)
            {
                TempData["Error"] = (ex.Message);
                return RedirectToAction("index", "home", newItem);
            }
            catch (Exception ex)
            {
                TempData["Error"] = (ex.Message);
                return RedirectToAction("index", "notfound");
            }
            return View(newItem);
        }
    }
}
