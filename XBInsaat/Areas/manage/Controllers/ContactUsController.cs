using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using XBInsaat.Core.Entites;
using XBInsaat.Mvc.Areas.manage.ViewModels;
using XBInsaat.Service.CustomExceptions;
using XBInsaat.Service.Helper;
using XBInsaat.Services.Services.Interfaces.Area;

namespace XBInsaat.Mvc.Areas.manage.Controllers
{
    [Area("manage")]
    //[Authorize(Roles = "SuperAdmin,Admin,Editor,Viewer")]

    public class ContactUsController : Controller
    {
        private readonly IAdminContactUsIndexServices _adminContactUsIndexServices;

        public ContactUsController(IAdminContactUsIndexServices adminContactUsIndexServices)
        {
            _adminContactUsIndexServices = adminContactUsIndexServices;
        }
        public IActionResult Index(int page = 1, string name = null)
        {
            ContactUsIndexViewModel ContactUsIndexVM = new ContactUsIndexViewModel();
            try
            {
                var contactUs = _adminContactUsIndexServices.GetPoster(name);
                ContactUsIndexVM = new ContactUsIndexViewModel
                {
                    ContactUs = PagenetedList<ContactUs>.Create(contactUs, page, 5),
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
            return View(ContactUsIndexVM);
        }

    }
}
