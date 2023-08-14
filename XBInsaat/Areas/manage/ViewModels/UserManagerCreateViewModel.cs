using XBInsaat.Core.Entites;
using XBInsaat.Services.Dtos.Area;
using XBInsaat.Service.Helper;
using Microsoft.AspNetCore.Identity;
using System.Reflection.Metadata;

namespace XBInsaat.Mvc.Areas.manage.ViewModels
{
    public class UserManagerCreateViewModel
    {
        public List<IdentityRole> Roles { get; set; }
        public UserManagerCreateDto UserManagerCreateDto { get; set; }
    }
}
