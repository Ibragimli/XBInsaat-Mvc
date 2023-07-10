using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBInsaat.Services.Dtos.Area
{
    public class RevolutionSliderEditDto
    {
        public IFormFile KeyImageFile { get; set; }
        public int Id { get; set; }

        public class CreatePostDtoValidator : AbstractValidator<RevolutionSliderEditDto>
        {
            public CreatePostDtoValidator()
            {
                RuleFor(x => x.KeyImageFile).NotEmpty().WithMessage("boş olmamalıdır.");
            }
        }
    }
}
