using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBInsaat.Services.Dtos.Area
{
    public class EmailSettingEditDto
    {
        public string Value { get; set; }
        public string Key { get; set; }
        public int Id { get; set; }

        public class CreatePostDtoValidator : AbstractValidator<EmailSettingEditDto>
        {
            public CreatePostDtoValidator()
            {
                RuleFor(x => x.Value).NotEmpty().WithMessage("boş olmamalıdır.").MaximumLength(750);
            }
        }
    }
}
