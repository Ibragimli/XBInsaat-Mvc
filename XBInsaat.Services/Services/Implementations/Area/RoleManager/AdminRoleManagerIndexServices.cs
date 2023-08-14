﻿using XBInsaat.Core.Entites;
using XBInsaat.Core.IUnitOfWork;
using XBInsaat.Services.Services.Interfaces.Area.RoleManagers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBInsaat.Services.Services.Implementations.Area.RoleManagers
{
    public class AdminRoleManagerIndexServices : IAdminRoleManagerIndexServices
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminRoleManagerIndexServices(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public IQueryable<IdentityRole> GetRoleManager(string name)
        {
            var roles = _roleManager.Roles.Where(x => x.Name != null).AsQueryable();


            if (roles != null)
                roles = roles.Where(i => EF.Functions.Like(i.Name, $"%{name}%"));

            return roles;
        }
    }
}
