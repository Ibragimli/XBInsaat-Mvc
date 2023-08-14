using XBInsaat.Core.Entites;
using XBInsaat.Mvc.Areas.manage.ViewModels;
using XBInsaat.Services.CustomExceptions;
using XBInsaat.Services.Dtos.Area;
using XBInsaat.Services.HelperService.Implementations;
using XBInsaat.Services.HelperService.Interfaces;
using XBInsaat.Services.Services.Interfaces.Area.Loggers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using XBInsaat.Service.Helper;
using XBInsaat.Service.CustomExceptions;

namespace XBInsaat.Mvc.Areas.manage.Controllers
{

    [Area("manage")]
    [Authorize(Roles = "SuperAdmin")]

    public class LoggerController : Controller
    {
        private readonly ILoggerServices _loggerServices;
        private readonly UserManager<AppUser> _userManager;
        private readonly IAdminLoggerIndexServices _adminLoggerIndexServices;

        public LoggerController(ILoggerServices loggerServices, UserManager<AppUser> userManager, IAdminLoggerIndexServices adminLoggerIndexServices)
        {
            _loggerServices = loggerServices;
            _userManager = userManager;
            _adminLoggerIndexServices = adminLoggerIndexServices;
        }
        public async Task<IActionResult> Index(int page = 1, string name = null)
        {
            LoggerIndexViewModel LoggerIndexVM = new LoggerIndexViewModel();
            try
            {
                var Logger = _adminLoggerIndexServices.GetLogger(name);
                LoggerIndexVM = new LoggerIndexViewModel
                {
                    Loggers = PagenetedList<Logger>.Create(Logger, page, 5),
                };
                //Logger
                AppUser user = User.Identity.IsAuthenticated ? _userManager.Users.FirstOrDefault(x => x.UserName == User.Identity.Name && x.IsAdmin) : null;
                if (user == null)
                    throw new UserNotFoundException("Error bas verdi!");
                await _loggerServices.LoggerCreate("Logger", "Index", user.FullName, user.UserName);
            }
            catch (NotFoundException)
            {
                return RedirectToAction("index", "notfound");
            }
            catch (UserNotFoundException)
            {
                return RedirectToAction("index", "notfound");
            }
            catch (Exception)
            {
                return RedirectToAction("index", "notfound");
            }
            return View(LoggerIndexVM);
        }
    }
}
