using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Runtime.Serialization;
using XBInsaat.Core.IUnitOfWork;
using XBInsaat.Data.Datacontext;
using XBInsaat.Services.Services.Implementations.Area;
using XBInsaat.Services.Services.Interfaces.Area;


namespace XBInsaat.Mvc.Areas.manage.Controllers
{
    [Area("manage")]
    public class NotFoundController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }

    }
}

