using XBInsaat.Core.Entites;
using XBInsaat.Service.Helper;
using Microsoft.AspNetCore.Identity;
using System.Reflection.Metadata;

namespace XBInsaat.Mvc.Areas.manage.ViewModels
{
    public class RoleManagerEditViewModel
    {
        public RoleManager<IdentityRole> RoleManager { get; set; }
    }
}
