using XBInsaat.Core.Entites;
using XBInsaat.Services.Dtos.Area;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBInsaat.Services.Services.Interfaces.Area.RoleManagers
{
    public interface IAdminRoleManagerCreateServices
    {
        Task CreateRoleManager(RoleManagerCreateDto RoleManagerCreateDto);

    }
}
