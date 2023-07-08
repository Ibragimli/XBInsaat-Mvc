using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBInsaat.Core.Entites;

namespace XBInsaat.Services.Services.Interfaces.Area
{
    public interface IAdminHighProjectEditServices
    {
        public Task<HighProject> GetHighProject(int id);
        public Task EditHighProject(HighProject highProject);
        public Task<IEnumerable<HighProjectImage>> GetImages(int id);

    }
}
