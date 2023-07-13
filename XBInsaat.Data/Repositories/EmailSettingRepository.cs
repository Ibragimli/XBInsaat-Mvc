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
    public class EmailSettingRepository : Repository<EmailSetting>, IEmailSettingRepository
    {
        private readonly DataContext _context;

        public EmailSettingRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
