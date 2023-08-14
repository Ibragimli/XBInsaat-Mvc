﻿using XBInsaat.Core.Entites;
using XBInsaat.Core.IUnitOfWork;
using XBInsaat.Service.CustomExceptions;
using XBInsaat.Services.HelperService.Interfaces;
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
    public class AdminRoleManagerDeleteServices : IAdminRoleManagerDeleteServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public AdminRoleManagerDeleteServices(IUnitOfWork unitOfWork, RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task DeleteRoleManager(string id)
        {

            var role = await _roleManager.Roles.FirstOrDefaultAsync(x => x.Id == id);

            if (role == null)
                throw new ItemNullException("Role tapılmadı!");

            var list = await _userManager.GetUsersInRoleAsync(role.Name);

            if (list.Count() > 0)
                throw new ItemUseException("Bu role User-larda istifadə olunur!");


            await _roleManager.DeleteAsync(role);
            await _unitOfWork.CommitAsync();

        }
    }
}
