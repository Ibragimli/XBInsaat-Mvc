using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBInsaat.Services.Dtos.Area
{
    public class HighProjectCreateDto
    {
        public string Name { get; set; }
        public string DescribeAz { get; set; }
        public string DescribeEn { get; set; }
        public string DescribeRu { get; set; }
        public List<IFormFile> ImageFiles { get; set; }

    }
    public class HighProjectCreateDtoValidator : AbstractValidator<HighProjectCreateDto>
    {
        public HighProjectCreateDtoValidator()
        {
            RuleFor(x => x.Name).MinimumLength(3).WithMessage("Layihənin adının uzunluğu 3-dən az ola bilməz!").MaximumLength(100).WithMessage("Layihənin adının uzunluğu 100-dən böyük ola bilməz!");
            RuleFor(x => x.DescribeAz).NotEmpty().WithMessage("Layihənin ad hissəsi boş olmamalıdır.").MinimumLength(3).WithMessage("Layihənin adının uzunluğu 3-dən az ola bilməz!").MaximumLength(3000).WithMessage("Layihənin adının uzunluğu 3000-dən böyük ola bilməz!");
            RuleFor(x => x.DescribeEn).NotEmpty().WithMessage("Layihənin ad hissəsi boş olmamalıdır.").MinimumLength(3).WithMessage("Layihənin adının uzunluğu 3-dən az ola bilməz!").MaximumLength(3000).WithMessage("Layihənin adının uzunluğu 3000-dən böyük ola bilməz!");
            RuleFor(x => x.DescribeRu).NotEmpty().WithMessage("Layihənin ad hissəsi boş olmamalıdır.").MinimumLength(3).WithMessage("Layihənin adının uzunluğu 3-dən az ola bilməz!").MaximumLength(3000).WithMessage("Layihənin adının uzunluğu 3000-dən böyük ola bilməz!");
        }
    }
}
