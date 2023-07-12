using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBInsaat.Core.Entites;
using XBInsaat.Core.IUnitOfWork;
using XBInsaat.Service.CustomExceptions;
using XBInsaat.Service.HelperService.Interfaces;
using XBInsaat.Services.Services.Interfaces.Area;

namespace XBInsaat.Services.Services.Implementations.Area
{
    public class AdminNewsEditServices : IAdminNewsEditServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IManageImageHelper _manageImageHelper;
        private readonly IMapper _mapper;

        public AdminNewsEditServices(IUnitOfWork unitOfWork, IManageImageHelper manageImageHelper, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _manageImageHelper = manageImageHelper;
            _mapper = mapper;
        }

        public async Task EditNews(News News)
        {
            bool checkBool = false;

            var oldNews = await GetNews(News.Id);
            if (oldNews == null)
                throw new ItemNullException("Xəbər tapılmadı!");


            Check(News);
            if (News.ProjectImageFile != null)
                _manageImageHelper.PosterCheck(News.ProjectImageFile);

            if (News.ImageFiles != null)
                _manageImageHelper.ImagesCheck(News.ImageFiles);

            int deleteCount = DeleteImages(News, oldNews);
            if (deleteCount > 0)
                checkBool = true;

            if (PosterImageChange(News, oldNews) == 1)
                checkBool = true;

            if (await CreateImageFormFile(News.ImageFiles, News.Id, deleteCount) == 1)
                checkBool = true;

            if (oldNews.Title != News.Title)
            {
                oldNews.Title = News.Title;
                checkBool = true;
            }

            if (oldNews?.InstagramUrl != News?.InstagramUrl)
            {
                oldNews.InstagramUrl = News.InstagramUrl;
                checkBool = true;
            }
            if (oldNews.TextAz != News.TextAz)
            {

                oldNews.TextAz = News.TextAz;
                checkBool = true;

            }
            if (oldNews.TextEn != News.TextEn)
            {

                oldNews.TextEn = News.TextEn;
                checkBool = true;

            }
            if (oldNews.TextRu != News.TextRu)
            {
                oldNews.TextRu = News.TextRu;
                checkBool = true;
            }
            oldNews.ModifiedDate = DateTime.UtcNow.AddHours(4);
            if (checkBool)
                await _unitOfWork.CommitAsync();
        }

        public async Task<News> GetNews(int id)
        {
            var News = await _unitOfWork.NewsRepository.GetAsync(x => x.Id == id, "NewsImages");
            return News;
        }

        public async Task<IEnumerable<NewsImage>> GetImages(int id)
        {
            var images = await _unitOfWork.NewsImageRepository.GetAllAsync(x => x.NewsId == id);
            return images;
        }
        private int PosterImageChange(News News, News projectExist)
        {
            if (News.ProjectImageFile != null)
            {
                var posterImageFile = News.ProjectImageFile;

                NewsImage posterImage = projectExist.NewsImages.FirstOrDefault(x => x.IsPoster);

                if (posterImage == null) throw new ImageNullException("Şəkil tapılmadı!");

                string filename = _manageImageHelper.FileSave(posterImageFile, "News");
                _manageImageHelper.DeleteFile(posterImage.Image, "News");
                posterImage.Image = filename;
                posterImage.IsPoster = true;
                return 1;
            }
            return 0;

        }
        private async Task<int> CreateImageFormFile(List<IFormFile> imageFiles, int posterId, int deleteCount)
        {
            int countImage = await _unitOfWork.NewsImageRepository.GetTotalCountAsync(x => x.NewsId == posterId && !x.IsPoster);
            int i = 0;

            if (countImage < 9)
            {
                if (imageFiles != null)
                {
                    i = 8 - countImage - deleteCount;
                    if (i == 0)
                        throw new ImageCountException("Maksimum 8 şəkil əlavə edə bilərsiniz!");
                    foreach (var image in imageFiles)
                    {
                        if (i != 0)
                        {
                            NewsImage Posterimage = new NewsImage
                            {
                                IsPoster = false,
                                NewsId = posterId,
                                Image = _manageImageHelper.FileSave(image, "News"),
                            };
                            await _unitOfWork.NewsImageRepository.InsertAsync(Posterimage);
                            i--;
                        }
                    }
                    return 1;
                }
            }
            return 0;
        }

        private int DeleteImages(News poster, News posterExist)
        {
            int i = 0;
            ICollection<NewsImage> posterImages = posterExist.NewsImages;
            if (poster.ProjectImagesIds != null)
            {
                foreach (var image in posterImages.ToList().Where(x => x.IsDelete == false && !x.IsPoster && !poster.ProjectImagesIds.Contains(x.Id)))
                {
                    _manageImageHelper.DeleteFile(image.Image, "News");
                    posterExist.NewsImages.Remove(image);
                    i++;
                }
                posterImages.ToList().RemoveAll(x => !poster.ProjectImagesIds.Contains(x.Id));
                return i;
            }
            else
            {
                //if (posterExist.PosterImages?.Count() > 1)
                //{
                if (poster.ImageFiles?.Count() > 0)
                {
                    foreach (var item in posterImages.ToList().Where(x => !x.IsDelete && !x.IsPoster))
                    {
                        _manageImageHelper.DeleteFile(item.Image, "News");
                        posterExist.NewsImages.Remove(item);
                        i++;
                    }
                    return i;
                }
                else if (posterImages.Any(x => !x.IsPoster))
                {
                    foreach (var item in posterImages.ToList().Where(x => !x.IsDelete && !x.IsPoster))
                    {
                        _manageImageHelper.DeleteFile(item.Image, "News");
                        posterExist.NewsImages.Remove(item);
                        i++;
                    }
                    return i;
                }
                else if (posterImages.Any(x => x.IsPoster))
                {
                    return i;
                }
                else throw new ImageCountException("Axırıncı şəkil silinə bilməz!");

            }
        }
        private void Check(News News)
        {
            if (News.Title.Length < 3)
            {
                throw new ValueFormatExpception("Xəbər adının uzunluğu minimum 3 ola bilər");
            }
            if (News.Title.Length > 100)
            {
                throw new ValueFormatExpception("Xəbər adının uzunluğu maksimum 100 ola bilər");
            }
            if (News.TextAz.Length > 5000 || News.TextRu.Length > 5000 || News.TextEn.Length > 5000)
            {
                throw new ValueFormatExpception("Xəbər mətninin uzunluğu maksimum 5000 ola bilər");

            }
            if (News.TextAz.Length < 3 || News.TextRu.Length < 3 || News.TextEn.Length < 3)
            {
                throw new ValueFormatExpception("Xəbər mətninin uzunluğu minimum 3 ola bilər");
            }
            if (News.InstagramUrl?.Length > 200)
            {
                throw new ValueFormatExpception("Xəbər mətninin uzunluğu maksimum 200 ola bilər");
            }
        }

    }
}
