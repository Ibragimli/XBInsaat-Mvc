using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBInsaat.Data.Datacontext;
using XBInsaat.Services.Services.Interfaces.Area;
using Microsoft.EntityFrameworkCore;
using XBInsaat.Service.CustomExceptions;
using System.Data;

namespace XBInsaat.Services.Helper
{
    public class DynamicRoleFilter : IAuthorizationFilter
    {
        private readonly DataContext _context;

        public DynamicRoleFilter(DataContext context)
        {
            _context = context;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var controllerName = context.RouteData.Values["controller"].ToString();
            List<string> list = new List<string>();
            bool check = false;
            var rolePage = _context.RolePageIdentityRoles.Where(x => x.RolePage.Key == controllerName).Include(x => x.IdentityRole).Include(x => x.RolePage);

            if (rolePage.Count() > 0)
            {
                foreach (var item in rolePage)
                {
                    list.Add(item.IdentityRole.Name);
                }
                if (list.Count() > 0)
                {
                    var user = context.HttpContext.User;
                    foreach (var role in list)
                    {
                        if (user.IsInRole(role))
                            check = true;
                    }
                    if (!check)
                        context.Result = new ForbidResult();
                }
            }
        }
    }
}
