using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBInsaat.Core.Entites;
using XBInsaat.Core.Repositories;
using XBInsaat.Data.Datacontext;

namespace XBInsaat.Data.Repositories
{

    public class LowProjectRepository : Repository<LowProject>, ILowProjectRepository
    {
        private readonly DataContext _context;

        public LowProjectRepository(DataContext context) : base(context)
        {
            _context = context;
        }

    }
}
