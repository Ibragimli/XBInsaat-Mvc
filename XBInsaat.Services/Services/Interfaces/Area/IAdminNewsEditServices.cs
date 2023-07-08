using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBInsaat.Core.Entites;

namespace XBInsaat.Services.Services.Interfaces.Area
{
    public interface IAdminNewsEditServices
    {
        public Task<News> GetNews(int id);
        public Task EditNews(News News);
        public Task<IEnumerable<NewsImage>> GetImages(int id);

    }
}
