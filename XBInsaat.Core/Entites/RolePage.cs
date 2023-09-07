using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBInsaat.Core.Entites
{
    public class RolePage : BaseEntity
    {
        public string Key { get; set; }
        public ICollection<RolePageIdentityRoleId> RolePageIdentityRoles { get; set; }
    }
}
