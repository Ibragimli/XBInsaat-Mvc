using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBInsaat.Core.Entites;

namespace XBInsaat.Services.Dtos.Area
{
    public class RolePageEditDto
    {
        public int Id { get; set; }
        public List<string>? IdentityRoleIds { get; set; }
        public string Key { get; set; }
        public int RoleId { get; set; }
        public IEnumerable<RolePageIdentityRoleId> AllPagesRoles { get; set; }



        public class EditPostDtoValidator : AbstractValidator<RolePageEditDto>
        {
            public EditPostDtoValidator()
            {
                //RuleFor(x => x.Value).NotEmpty().WithMessage("boş olmamalıdır.").MaximumLength(750);
            }
        }
    }
}
