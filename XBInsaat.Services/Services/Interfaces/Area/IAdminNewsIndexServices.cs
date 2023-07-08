using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBInsaat.Core.Entites;

namespace XBInsaat.Services.Services.Interfaces.Area
{
    public interface IAdminNewsIndexServices
    {
        public IQueryable<News> GetPoster(string name);
    }
}
