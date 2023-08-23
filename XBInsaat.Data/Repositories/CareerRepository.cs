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
    public class CareerRepository : Repository<Career>, ICareerRepository
    {
        private readonly DataContext _context;

        public CareerRepository(DataContext context) : base(context)
        {
            _context = context;
        }

    }
}
