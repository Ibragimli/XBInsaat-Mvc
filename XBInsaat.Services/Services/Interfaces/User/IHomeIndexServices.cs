using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBInsaat.Core.Entites;

namespace XBInsaat.Services.Services.Interfaces.User
{
    public interface IHomeIndexServices
    {
        public Task<IEnumerable<Setting>> GetSettings();
        public Task<IEnumerable<HighProject>> GetHighProjects();
        public Task<IEnumerable<MidProject>> GetMidProjects();
        public Task<IEnumerable<XBService>> GetXBServices();
        public Task<IEnumerable<News>> GetNews();
    }
}
