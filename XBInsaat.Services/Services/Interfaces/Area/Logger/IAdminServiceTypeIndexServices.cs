using XBInsaat.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBInsaat.Services.Services.Interfaces.Area.Loggers
{
    public interface IAdminLoggerIndexServices
    {
        public IQueryable<Logger> GetLogger(string name);
    }
}
