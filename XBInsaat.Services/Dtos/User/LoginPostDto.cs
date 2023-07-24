using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBInsaat.Services.Dtos.User
{
    public class LoginPostDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    public class LoginPostDtoValidator : AbstractValidator<LoginPostDto>
    {
        public LoginPostDtoValidator()
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage("İstifadəçi adınızı qeyd edin").MaximumLength(50);
            RuleFor(x => x.Password).NotEmpty().WithMessage("Şifrənizi qeyd edin.");
        }
    }
}
