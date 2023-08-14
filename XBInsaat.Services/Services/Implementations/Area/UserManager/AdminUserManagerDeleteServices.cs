using XBInsaat.Core.Entites;
using XBInsaat.Core.IUnitOfWork;
using XBInsaat.Data.Datacontext;
using XBInsaat.Services.HelperService.Interfaces;
using XBInsaat.Services.Services.Interfaces.Area.UserManagers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBInsaat.Service.CustomExceptions;

namespace XBInsaat.Services.Services.Implementations.Area.UserManagers
{
    public class AdminUserManagerDeleteServices : IAdminUserManagerDeleteServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;

        public AdminUserManagerDeleteServices(IUnitOfWork unitOfWork, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task DeleteUserManager(string id)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
                throw new ItemNullException("User tapılmadı!");

            await _userManager.DeleteAsync(user);
            await _unitOfWork.CommitAsync();

        }
    }
}
