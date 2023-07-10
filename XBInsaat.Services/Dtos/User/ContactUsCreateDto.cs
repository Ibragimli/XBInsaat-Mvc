using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBInsaat.Services.Dtos.Area;

namespace XBInsaat.Services.Dtos.User
{
    public class ContactUsCreateDto
    {
        public string Fullname { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
    }
    public class ContactUsCreateDtoValidator : AbstractValidator<ContactUsCreateDto>
    {
        public ContactUsCreateDtoValidator()
        {
            RuleFor(x => x.Fullname).NotEmpty().WithMessage("Ad və Soyadınızı qeyd edin.").MinimumLength(3).WithMessage("Ad və soyadınızın uzunluğu 3-dən az ola bilməz!").MaximumLength(50).WithMessage("Ad və Soyadınızın  uzunluğu 50-dən böyük ola bilməz!");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Telefon nömrənəzi qeyd edin.").MinimumLength(9).WithMessage("Telefon nömrəsini düzgün qeyd edin").MaximumLength(15).WithMessage("Telefon nömrəsini düzgün qeyd edin!");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Emailinizi qeyd edin.").MinimumLength(8).WithMessage("Emailinizin  uzunluğu 8-dən az ola bilməz!").MaximumLength(50).WithMessage("Emailinizin uzunluğu 50-dən böyük ola bilməz!");
            RuleFor(x => x.Message).NotEmpty().WithMessage("Messajınızı qeyd edin.").MinimumLength(3).WithMessage("Mesajınızın uzunluğu 3-dən az ola bilməz!").MaximumLength(5000).WithMessage("Mesajınızın uzunluğu 5000-dən böyük ola bilməz!");
        }
    }
}
