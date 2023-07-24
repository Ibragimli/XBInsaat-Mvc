using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBInsaat.Core.Entites;

namespace XBInsaat.Services.Services.Interfaces.Area
{
    public interface IAdminMidProjectEditServices
    {
        public Task<MidProject> GetMidProject(int id);
        public Task EditMidProject(MidProject MidProject);
        public int GetMaxRow();
        public Task<IEnumerable<MidProjectImage>> GetImages(int id);

    }
}
