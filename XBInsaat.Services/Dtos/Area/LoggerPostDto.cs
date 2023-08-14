using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBInsaat.Services.Dtos.User
{
    public class LoggerPostDto
    {
        public string Name { get; set; }
        public string Role { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string? Product { get; set; }
    }
    public class LoggerPostDtoValidator : AbstractValidator<LoggerPostDto>
    {
        public LoggerPostDtoValidator()
        {
            //RuleFor(x => x.Message).MaximumLength(5000).WithMessage("Müraciətinizin uzunluğu 5000-dən böyük ola bilməz!");
        }
    }
}
