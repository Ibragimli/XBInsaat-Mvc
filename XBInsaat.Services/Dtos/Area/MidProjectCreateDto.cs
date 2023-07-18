using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBInsaat.Services.Dtos.Area
{
    public class MidProjectCreateDto
    {
        public string Name { get; set; }
        public string DescribeAz { get; set; }
        public string DescribeEn { get; set; }
        public string DescribeRu { get; set; }
        public int HighProjectId { get; set; }
        public string InstagramUrl { get; set; }
        public string ContactInfo { get; set; }
        public List<string> ImageFilesStr { get; set; }
        public List<IFormFile> ImageFiles { get; set; }

    }
    public class MidProjectCreateDtoValidator : AbstractValidator<MidProjectCreateDto>
    {
        public MidProjectCreateDtoValidator()
        {
            RuleFor(x => x.ContactInfo).Empty().MinimumLength(3).WithMessage("Əlaqə məlumatlarının uzunluğu 3-dən az ola bilməz!").MaximumLength(200).WithMessage("Əlaqə məlumatlarının uzunluğu 200-dən böyük ola bilməz!");
            RuleFor(x => x.InstagramUrl).Empty().MinimumLength(3).WithMessage("Instagram url-nin uzunluğu 3-dən az ola bilməz!").MaximumLength(200).WithMessage("Instagram url-nin uzunluğu 200-dən böyük ola bilməz!");
            RuleFor(x => x.Name).MinimumLength(3).WithMessage("Layihənin adının uzunluğu 3-dən az ola bilməz!").MaximumLength(100).WithMessage("Layihənin adının uzunluğu 100-dən böyük ola bilməz!");
            RuleFor(x => x.DescribeAz).NotEmpty().WithMessage("Layihənin ad hissəsi boş olmamalıdır.").MinimumLength(3).WithMessage("Layihənin adının uzunluğu 3-dən az ola bilməz!").MaximumLength(3000).WithMessage("Layihənin adının uzunluğu 3000-dən böyük ola bilməz!");
            RuleFor(x => x.DescribeEn).NotEmpty().WithMessage("Layihənin ad hissəsi boş olmamalıdır.").MinimumLength(3).WithMessage("Layihənin adının uzunluğu 3-dən az ola bilməz!").MaximumLength(3000).WithMessage("Layihənin adının uzunluğu 3000-dən böyük ola bilməz!");
            RuleFor(x => x.DescribeRu).NotEmpty().WithMessage("Layihənin ad hissəsi boş olmamalıdır.").MinimumLength(3).WithMessage("Layihənin adının uzunluğu 3-dən az ola bilməz!").MaximumLength(3000).WithMessage("Layihənin adının uzunluğu 3000-dən böyük ola bilməz!");
        }
    }
}
