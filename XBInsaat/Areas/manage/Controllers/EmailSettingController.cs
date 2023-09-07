using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using XBInsaat.Core.Entites;
using XBInsaat.Data.Datacontext;
using XBInsaat.Mvc.Areas.manage.ViewModels;
using XBInsaat.Service.CustomExceptions;
using XBInsaat.Service.Helper;
using XBInsaat.Services.Dtos.Area;
using XBInsaat.Services.HelperService.Interfaces;
using XBInsaat.Services.Services.Interfaces.Area;

namespace XBInsaat.Mvc.Areas.manage.Controllers
{
    [Area("manage")]
    [Authorize(Roles = "SuperAdmin,Admin")]

    public class EmailSettingController : Controller
    {
        private readonly ILoggerServices _loggerServices;
        private readonly UserManager<AppUser> _userManager;
        private readonly DataContext _context;
        private readonly IEmailSettingEditServices _emailSettingEditServices;
        private readonly IEmailSettingIndexServices _emailSettingIndexServices;

        public EmailSettingController(ILoggerServices loggerServices, UserManager<AppUser> userManager, DataContext context, IEmailSettingEditServices emailSettingEditServices, IEmailSettingIndexServices emailSettingIndexServices)
        {
            _loggerServices = loggerServices;
            _userManager = userManager;
            _context = context;
            _emailSettingEditServices = emailSettingEditServices;
            _emailSettingIndexServices = emailSettingIndexServices;
        }
        public IActionResult Index(int page = 1, string search = null)
        {
            ViewBag.Page = page;

            var EmailSettings = _emailSettingIndexServices.SearchCheck(search);

            EmailSettingIndexViewModel EmailSettingIndexVM = new EmailSettingIndexViewModel
            {
                PagenatedItems = PagenetedList<EmailSetting>.Create(EmailSettings, page, 6),
            };

            return View(EmailSettingIndexVM);
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                await _emailSettingEditServices.IsExists(id);
                await _emailSettingEditServices.GetSearch(id);
            }
            catch (Exception)
            {
                return RedirectToAction("index", "notfound");

            }

            return View(await _emailSettingEditServices.GetSearch(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EmailSettingEditDto EmailSettingEdit)
        {
            try
            {
                await _emailSettingEditServices.EmailSettingEdit(EmailSettingEdit);
              
                //Logger
                var product = await _emailSettingEditServices.GetSearch(EmailSettingEdit.Id);
                AppUser user = User.Identity.IsAuthenticated ? _userManager.Users.FirstOrDefault(x => x.UserName == User.Identity.Name && x.IsAdmin) : null;
                if (user == null)
                    throw new UserNotFoundException("Error bas verdi!");
                await _loggerServices.LoggerCreate("EmailSetting", "Edit", user.FullName, user.UserName, product.Key);
            }
            catch (ValueFormatExpception ex)
            {

                ModelState.AddModelError("", ex.Message);
                return View(EmailSettingEdit);
            }
          
            catch (ItemNotFoundException ex)
            {

                ModelState.AddModelError("", ex.Message);
                return View(EmailSettingEdit);
            }
            catch (UserNotFoundException)
            {
                return RedirectToAction("index", "notfound");
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", ex.Message);
                return View(EmailSettingEdit);
            }
            TempData["Success"] = ("Proses uğurlu oldu!");
            return RedirectToAction("index", "EmailSetting");
        }
    }
}
