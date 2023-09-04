using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBInsaat.Core.Entites;
using XBInsaat.Core.IUnitOfWork;
using XBInsaat.Service.CustomExceptions;
using XBInsaat.Services.Services.Interfaces.User;

namespace XBInsaat.Services.Services.Implementations.User
{
    public class HomeIndexServices : IHomeIndexServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeIndexServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<HighProjectImage>> GetHighProjectImages()
        {
            return await _unitOfWork.HighProjectImageRepository.GetAllAsync(x => !x.IsDelete);
        }

        public async Task<IEnumerable<HighProject>> GetHighProjects()
        {
            return await _unitOfWork.HighProjectRepository.GetAllAsync(x => !x.IsDelete, "HighProjectImages", "MidProjects");
        }

        public async Task<IEnumerable<MidProjectImage>> GetMidProjectImages()
        {
            return await _unitOfWork.MidProjectImageRepository.GetAllAsync(x => !x.IsDelete);
        }

        public async Task<IEnumerable<MidProject>> GetMidProjects()
        {
            return await _unitOfWork.MidProjectRepository.GetAllAsync(x => !x.IsDelete, "MidProjectImages", "HighProject");
        }
        public async Task<IEnumerable<MidProject>> GetRowMidProjects(int highProjectId)
        {
            var midProjects = await _unitOfWork.MidProjectRepository.GetAllAsync(x => x.HighProjectId == highProjectId, "MidProjectImages", "HighProject");
            midProjects = midProjects.OrderBy(x => x.Row);

            return midProjects;
        }

        public async Task<News> GetNew(int id)
        {
            var item = await _unitOfWork.NewsRepository.GetAsync(x => !x.IsDelete && x.Id == id, "NewsImages");
            if (item == null)
                throw new ItemNotFoundException("Xəbər tapılmadı!");
            return item;

        }
        public async Task<HighProject> GetHighProject(int id)
        {
            var item = await _unitOfWork.HighProjectRepository.GetAsync(x => !x.IsDelete && x.Id == id, "HighProjectImages");

            if (item == null)
                throw new ItemNotFoundException("Layihə tapılmadı!");
            return item;

        }

        public async Task<IEnumerable<News>> GetNews()
        {
            var news = await _unitOfWork.NewsRepository.GetAllAsync(x => !x.IsDelete, "NewsImages");
            news = news.OrderByDescending(x => x.Id);
            return news;
        }

        public async Task<IEnumerable<NewsImage>> GetNewsImages()
        {
            return await _unitOfWork.NewsImageRepository.GetAllAsync(x => !x.IsDelete);

        }

        public async Task<IEnumerable<RevolutionSlider>> GetRevolutionSliders()
        {
            return await _unitOfWork.RevolutionSliderRepository.GetAllAsync(x => !x.IsDelete);

        }

        public async Task<IEnumerable<Setting>> GetSettings()
        {
            return await _unitOfWork.SettingRepository.GetAllAsync(x => !x.IsDelete);
        }

        public async Task<IEnumerable<XBService>> GetXBServices()
        {
            return await _unitOfWork.XBServiceRepository.GetAllAsync(x => !x.IsDelete);
        }

        public async Task<MidProject> GetMidProject(int id)
        {
            var item = await _unitOfWork.MidProjectRepository.GetAsync(x => !x.IsDelete && x.Id == id, "MidProjectImages", "HighProject");

            if (item == null)
                throw new ItemNotFoundException("Layihə tapılmadı!");
            return item;
        }

        public async Task<List<News>> GetNewsData(int page, int pageSize)
        {
            //var news = await _unitOfWork.NewsRepository.GetAllPagenatedAsync(x => !x.IsDelete, page, pageSize, "NewsImages").ToList();
            //return news;

            var newsList = await _unitOfWork.NewsRepository.GetAllPagenatedAsync(x => !x.IsDelete, page, pageSize, "NewsImages"); ;


            var newsListAsList = newsList.OrderByDescending(x => x.Id).ToList();


            return newsListAsList;
        }

        public async Task<IEnumerable<Localization>> GetLocalizations()
        {
            return await _unitOfWork.LocalizationRepository.GetAllAsync(x => !x.IsDelete);

        }
    }
}
