using XBInsaat.Core.Entites;
using XBInsaat.Services.Dtos.Area;
using XBInsaat.Service.Helper;
using Microsoft.AspNetCore.Identity;
using System.Reflection.Metadata;

namespace XBInsaat.Mvc.Areas.manage.ViewModels
{
    public class RolePageEditViewModel
    {
        public IEnumerable<IdentityRole> Roles { get; set; }
        public IEnumerable<RolePageIdentityRoleId> RolePageIdentityRoles { get; set; }
        public RolePageIdentityRoleId RolePageIdentityRole { get; set; }
        public RolePageEditDto RolePageEditDto { get; set; }

    }
}
