using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBInsaat.Services.Dtos.User
{
    public class CareerPostDto
    {
        public string Fullname { get; set; }
        public string PhoneNumber { get; set; }
        public IFormFile CV { get; set; }
        public string? Message { get; set; }
    }
    public class CareerPostDtoValidator : AbstractValidator<CareerPostDto>
    {
        public CareerPostDtoValidator()
        {
            RuleFor(x => x.Fullname).NotEmpty().WithMessage("Ad və Soyadınızı qeyd edin.").MinimumLength(3).WithMessage("Ad və soyadınızın uzunluğu 3-dən az ola bilməz!").MaximumLength(50).WithMessage("Ad və Soyadınızın  uzunluğu 50-dən böyük ola bilməz!");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Telefon nömrənəzi qeyd edin.").MinimumLength(9).WithMessage("Telefon nömrəsini düzgün qeyd edin").MaximumLength(15).WithMessage("Telefon nömrəsini düzgün qeyd edin!");
            RuleFor(x => x.Message).MaximumLength(5000).WithMessage("Müraciətinizin uzunluğu 5000-dən böyük ola bilməz!");
        }
    }
}
