using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBInsaat.Core.Entites
{
    public class RolePageIdentityRoleId : BaseEntity
    {
        public int RolePageId { get; set; }
        public RolePage RolePage { get; set; }
        public string IdentityRoleId { get; set; }
        public IdentityRole IdentityRole { get; set; }
        [NotMapped]
        public List<string> IdentityRolePagesIds { get; set; }
    }
}
