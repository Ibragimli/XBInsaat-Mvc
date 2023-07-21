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
        public Task<HighProject> GetHighProject(int id);
        public Task<MidProject> GetMidProject(int id);
        public Task<IEnumerable<MidProject>> GetMidProjects();
        public Task<IEnumerable<XBService>> GetXBServices();
        public Task<IEnumerable<News>> GetNews();
        public Task<List<News>> GetNewsData(int page, int pageSize);
        public Task<News> GetNew(int id);
        public Task<IEnumerable<HighProjectImage>> GetHighProjectImages();
        public Task<IEnumerable<MidProjectImage>> GetMidProjectImages();
        public Task<IEnumerable<NewsImage>> GetNewsImages();
        public Task<IEnumerable<RevolutionSlider>> GetRevolutionSliders();
    }
}
