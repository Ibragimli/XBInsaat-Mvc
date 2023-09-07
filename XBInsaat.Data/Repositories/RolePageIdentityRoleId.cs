using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using XBInsaat.Core.Entites;
using XBInsaat.Core.Repositories;
using XBInsaat.Data.Datacontext;

namespace XBInsaat.Data.Repositories
{

    public class RolePageIdentityRoleIdRepository : Repository<RolePageIdentityRoleId>, IRolePageIdentityRoleIdRepository
    {
        private readonly DataContext _context;

        public RolePageIdentityRoleIdRepository(DataContext context) : base(context)
        {
            _context = context;
        }

       
    }
}
