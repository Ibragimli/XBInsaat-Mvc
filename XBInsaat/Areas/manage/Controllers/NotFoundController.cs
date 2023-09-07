using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Runtime.Serialization;
using XBInsaat.Core.IUnitOfWork;
using XBInsaat.Data.Datacontext;
using XBInsaat.Services.Services.Interfaces.Area;

namespace XBInsaat.Mvc.Areas.manage.Controllers
{
    [Area("manage")]
    public class NotFoundController : Controller
    {
        private readonly IRolePageServices _rolePageServices;
        public NotFoundController(IRolePageServices rolePageServices)
        {
            _rolePageServices = rolePageServices;
        }
        public async Task<IActionResult> Index()
        {
            var roles = await _rolePageServices.GetRolePage("NotFound");

            // roles listesini virgülle birleştirip bir string olarak elde edin
            string rolesString = string.Join(",", roles);

            // [Authorize] attribute'unu dinamik olarak ayarlayın
            AuthorizeAttribute authorizeAttribute = new AuthorizeAttribute();
            authorizeAttribute.Roles = rolesString;

            // Controller sınıfına [Authorize] attribute'ünü uygulayın
            typeof(NotFoundController).GetCustomAttributes(true)
                .OfType<AuthorizeAttribute>()
                .ToList()
                .ForEach(attr => attr.Roles = rolesString);
            return View();
        }
    }
}
