using XBInsaat.Core.Entites;
using XBInsaat.Service.Helper;
using Microsoft.AspNetCore.Identity;
using System.Reflection.Metadata;

namespace XBInsaat.Mvc.Areas.manage.ViewModels
{
    public class RoleManagerIndexViewModel
    {
        public PagenetedList<IdentityRole> RoleManagers { get; set; }
    }
}
