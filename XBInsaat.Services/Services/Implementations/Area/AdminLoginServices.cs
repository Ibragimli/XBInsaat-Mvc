using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBInsaat.Core.Entites;
using XBInsaat.Core.IUnitOfWork;
using XBInsaat.Data.Datacontext;
using XBInsaat.Service.CustomExceptions;
using XBInsaat.Services.CustomExceptions;
using XBInsaat.Services.Dtos.Area;
using XBInsaat.Services.Services.Interfaces.Area;

namespace XBInsaat.Services.Services.Implementations.Area
{
    public class AdminLoginServices : IAdminLoginServices
    {
        private readonly DataContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AdminLoginServices(DataContext context, IUnitOfWork unitOfWork, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _context = context;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<bool> Login(AdminLoginPostDto adminLoginPostDto)
        {
            AppUser adminExist = await _unitOfWork.AppUserRepository.GetAsync(x => x.UserName == adminLoginPostDto.Username);
            var logger = await _context.Loggers.Where(x => x.Name == adminExist.UserName).OrderByDescending(x => x.Id).FirstOrDefaultAsync();

            if (adminExist != null && adminExist.IsAdmin == true && adminExist.LoginAttemptCount != 0)
            {
                var result = await _signInManager.PasswordSignInAsync(adminExist, adminLoginPostDto.Password, false, false);

                if (!result.Succeeded)
                {
                    adminExist.LoginAttemptCount -= 1;
                    await _unitOfWork.CommitAsync();
                    if (adminExist.LoginAttemptCount == 0)
                        throw new UserLoginAttempCountException("Hesab bloklandı!");
                    throw new UserLoginAttempCountException("Mümkün təkrar cəhd sayı! - " + adminExist.LoginAttemptCount);
                }
                if (logger != null && adminExist.LoginAttemptCount < 5 && (DateTime.UtcNow.AddHours(4) - logger.CreatedDate).Days > 0)
                {
                    adminExist.LoginAttemptCount = 5;
                }
                return true;
            }
            if (adminExist?.LoginAttemptCount == 0)
                throw new UserLoginAttempCountException("Hesab bloklandı!");

            throw new UserNotFoundException("Username və ya Password yanlışdır!");
        }

        public async void Logout()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
