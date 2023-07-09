using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBInsaat.Services.Dtos.Area
{
    public class XBServiceCreateDto
    {
        public string NameAz { get; set; }
        public string NameEn { get; set; }
        public string NameRu { get; set; }
        public string DescribeAz { get; set; }
        public string DescribeEn { get; set; }
        public string DescribeRu { get; set; }

    }
    public class XBServiceCreateDtoValidator : AbstractValidator<XBServiceCreateDto>
    {
        public XBServiceCreateDtoValidator()
        {
            RuleFor(x => x.NameAz).MinimumLength(3).WithMessage("Servisin adının uzunluğu 3-dən az ola bilməz!").MaximumLength(100).WithMessage("Servisin adının uzunluğu 100-dən böyük ola bilməz!");
            RuleFor(x => x.NameRu).MinimumLength(3).WithMessage("Servisin adının uzunluğu 3-dən az ola bilməz!").MaximumLength(100).WithMessage("Servisin adının uzunluğu 100-dən böyük ola bilməz!");
            RuleFor(x => x.NameEn).MinimumLength(3).WithMessage("Servisin adının uzunluğu 3-dən az ola bilməz!").MaximumLength(100).WithMessage("Servisin adının uzunluğu 100-dən böyük ola bilməz!");
            RuleFor(x => x.DescribeAz).NotEmpty().WithMessage("Servisin ad hissəsi boş olmamalıdır.").MinimumLength(3).WithMessage("Servisin adının uzunluğu 3-dən az ola bilməz!").MaximumLength(3000).WithMessage("Servisin adının uzunluğu 3000-dən böyük ola bilməz!");
            RuleFor(x => x.DescribeEn).NotEmpty().WithMessage("Servisin ad hissəsi boş olmamalıdır.").MinimumLength(3).WithMessage("Servisin adının uzunluğu 3-dən az ola bilməz!").MaximumLength(3000).WithMessage("Servisin adının uzunluğu 3000-dən böyük ola bilməz!");
            RuleFor(x => x.DescribeRu).NotEmpty().WithMessage("Servisin ad hissəsi boş olmamalıdır.").MinimumLength(3).WithMessage("Servisin adının uzunluğu 3-dən az ola bilməz!").MaximumLength(3000).WithMessage("Servisin adının uzunluğu 3000-dən böyük ola bilməz!");
        }
    }
}
