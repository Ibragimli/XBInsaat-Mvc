using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBInsaat.Core.Repositories;
using XBInsaat.Data.Datacontext;

namespace XBInsaat.Data.Repositories
{
    public class IdentityRoleRepository : Repository<IdentityRole>, IIdentityRoleRepository
    {
        private readonly DataContext _context;

        public IdentityRoleRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
