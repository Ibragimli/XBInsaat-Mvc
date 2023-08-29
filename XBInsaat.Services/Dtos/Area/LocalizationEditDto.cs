using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBInsaat.Services.Dtos.Area
{

    public class LocalizationEditDto
    {
        public string Value { get; set; }
        public string Key { get; set; }
        public int Id { get; set; }

        public class LocalizationEditDtoValidator : AbstractValidator<LocalizationEditDto>
        {
            public LocalizationEditDtoValidator()
            {
                RuleFor(x => x.Value).NotEmpty().WithMessage("boş olmamalıdır.").MaximumLength(750);
            }
        }
    }
}
