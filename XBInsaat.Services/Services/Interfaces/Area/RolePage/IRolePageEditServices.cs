using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBInsaat.Core.Entites;
using XBInsaat.Services.Dtos.Area;

namespace XBInsaat.Services.Services.Interfaces.Area
{
    public interface IRolePageEditServices
    {
        Task RolePageEdit(RolePageEditDto RolePageEdit);
        Task<RolePageEditDto> IsExists(int id);
        Task<RolePageEditDto> GetSearch(int Id);
        Task<IEnumerable<IdentityRole>> GetAllRoles();
        Task<IEnumerable<RolePageIdentityRoleId>> GetAllRolePageIdentityRoleIds();
        Task<RolePageIdentityRoleId> GetRolePageIdentityRoleId(int Id);

    }

}
