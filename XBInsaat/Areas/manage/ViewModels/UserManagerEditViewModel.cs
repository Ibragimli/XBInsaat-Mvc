using XBInsaat.Core.Entites;
using XBInsaat.Services.Dtos.Area;
using XBInsaat.Service.Helper;
using Microsoft.AspNetCore.Identity;
using System.Reflection.Metadata;

namespace XBInsaat.Mvc.Areas.manage.ViewModels
{
    public class UserManagerEditViewModel
    {
        public List<IdentityRole> Roles { get; set; }
        public UserManagerEditDto UserManagerEditDto { get; set; }
        public string RoleName { get; set; }
    }
}
