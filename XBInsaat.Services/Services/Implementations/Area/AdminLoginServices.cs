using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBInsaat.Core.Entites;
using XBInsaat.Core.IUnitOfWork;
using XBInsaat.Service.CustomExceptions;
using XBInsaat.Services.Dtos.Area;
using XBInsaat.Services.Services.Interfaces.Area;

namespace XBInsaat.Services.Services.Implementations.Area
{
    public class AdminLoginServices : IAdminLoginServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AdminLoginServices(IUnitOfWork unitOfWork, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<bool> Login(AdminLoginPostDto adminLoginPostDto)
        {
            AppUser adminExist = await _unitOfWork.AppUserRepository.GetAsync(x => x.UserName == adminLoginPostDto.Username);

            if (adminExist != null && adminExist.IsAdmin == true)
            {
                var result = await _signInManager.PasswordSignInAsync(adminExist, adminLoginPostDto.Password, false, false);
                if (!result.Succeeded) throw new UserNotFoundException("Username və ya Passoword yanlışdır!");

                return true;
            }
            throw new UserNotFoundException("Username və ya Passoword yanlışdır!");
        }

        public async void Logout()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
