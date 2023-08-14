﻿using XBInsaat.Core.Entites;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBInsaat.Services.Services.Interfaces.Area.UserManagers
{
    public interface IAdminUserManagerIndexServices
    {
        public IQueryable<AppUser> GetUserManager(string name);
    }
}
