using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBInsaat.Services.Dtos.Area
{
    public class SettingCreateDto
    {
        public string? Value { get; set; }
        public string Key { get; set; }
        public IFormFile? KeyImageFile { get; set; }
    }
    public class SettingCreateDtoValidator : AbstractValidator<SettingCreateDto>
    {
        public SettingCreateDtoValidator()
        {
            RuleFor(x => x.Value).Empty().MaximumLength(750);
            RuleFor(x => x.KeyImageFile).Empty();
        }
    }
}
