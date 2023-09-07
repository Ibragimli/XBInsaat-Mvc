using XBInsaat.Core.Entites;
using XBInsaat.Service.Helper;

namespace XBInsaat.Mvc.Areas.manage.ViewModels
{
    public class RolePageIndexViewModel
    {
        public PagenetedList<RolePage> PagenatedItems { get; set; }
        public IEnumerable<RolePageIdentityRoleId> RolePageIdentityRoles { get; set; }

    }
}
