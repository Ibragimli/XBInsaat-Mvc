using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBInsaat.Services.Dtos.Area
{
    public class NewsCreateDto
    {
        public string TitleAz { get; set; }
        public string TitleEn { get; set; }
        public string TitleRu { get; set; }
        public string TextRu { get; set; }
        public string TextAz { get; set; }
        public string TextEn { get; set; }
        public string? InstagramUrl { get; set; }
        public List<IFormFile> ImageFiles { get; set; }

    }
    public class NewsCreateDtoValidator : AbstractValidator<NewsCreateDto>
    {
        public NewsCreateDtoValidator()
        {
            RuleFor(x => x.InstagramUrl).Empty().MinimumLength(3).WithMessage("Layihənin adının uzunluğu 3-dən az ola bilməz!").MaximumLength(200).WithMessage("Layihənin adının uzunluğu 3000-dən böyük ola bilməz!");
            RuleFor(x => x.TitleAz).NotEmpty().WithMessage("Layihənin ad hissəsi boş olmamalıdır.").MinimumLength(3).WithMessage("Layihənin adının uzunluğu 3-dən az ola bilməz!").MaximumLength(3000).WithMessage("Layihənin adının uzunluğu 3000-dən böyük ola bilməz!");
            RuleFor(x => x.TitleEn).NotEmpty().WithMessage("Layihənin ad hissəsi boş olmamalıdır.").MinimumLength(3).WithMessage("Layihənin adının uzunluğu 3-dən az ola bilməz!").MaximumLength(3000).WithMessage("Layihənin adının uzunluğu 3000-dən böyük ola bilməz!");
            RuleFor(x => x.TitleRu).NotEmpty().WithMessage("Layihənin ad hissəsi boş olmamalıdır.").MinimumLength(3).WithMessage("Layihənin adının uzunluğu 3-dən az ola bilməz!").MaximumLength(3000).WithMessage("Layihənin adının uzunluğu 3000-dən böyük ola bilməz!");
            RuleFor(x => x.TextAz).MinimumLength(3).WithMessage("Layihənin adının uzunluğu 3-dən az ola bilməz!").MaximumLength(100).WithMessage("Layihənin adının uzunluğu 100-dən böyük ola bilməz!");
            RuleFor(x => x.TextEn).NotEmpty().WithMessage("Layihənin ad hissəsi boş olmamalıdır.").MinimumLength(3).WithMessage("Layihənin adının uzunluğu 3-dən az ola bilməz!").MaximumLength(3000).WithMessage("Layihənin adının uzunluğu 3000-dən böyük ola bilməz!");
            RuleFor(x => x.TextRu).NotEmpty().WithMessage("Layihənin ad hissəsi boş olmamalıdır.").MinimumLength(3).WithMessage("Layihənin adının uzunluğu 3-dən az ola bilməz!").MaximumLength(3000).WithMessage("Layihənin adının uzunluğu 3000-dən böyük ola bilməz!");
        }
    }
}
