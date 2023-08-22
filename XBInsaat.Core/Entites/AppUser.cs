using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBInsaat.Core.Entites
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
        public string RoleName { get; set; }
        public int? LoginAttemptCount { get; set; }
        public bool IsAdmin { get; set; }

    }
}
