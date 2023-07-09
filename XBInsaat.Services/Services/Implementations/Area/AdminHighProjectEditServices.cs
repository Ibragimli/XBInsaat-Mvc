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
    public class AdminHighProjectEditServices : IAdminHighProjectEditServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IManageImageHelper _manageImageHelper;

        public AdminHighProjectEditServices(IUnitOfWork unitOfWork, IManageImageHelper manageImageHelper)
        {
            _unitOfWork = unitOfWork;
            _manageImageHelper = manageImageHelper;
        }

        public async Task EditHighProject(HighProject highProject)
        {
            bool checkBool = false;

            var oldHighProject = await GetHighProject(highProject.Id);
            if (oldHighProject == null)
                throw new ItemNullException("Layihə tapılmadı!");


            Check(highProject);
            if (highProject.ProjectImageFile != null)
                _manageImageHelper.PosterCheck(highProject.ProjectImageFile);

            if (highProject.ImageFiles != null)
                _manageImageHelper.ImagesCheck(highProject.ImageFiles);

            int deleteCount = DeleteImages(highProject, oldHighProject);
            if (deleteCount > 0)
                checkBool = true;

            if (PosterImageChange(highProject, oldHighProject) == 1)
                checkBool = true;

            if (await CreateImageFormFile(highProject.ImageFiles, highProject.Id, deleteCount) == 1)
                checkBool = true;

            if (oldHighProject.Name != highProject.Name)
            {
                oldHighProject.Name = highProject.Name;
                checkBool = true;

            }
            if (oldHighProject.DescribeAz != highProject.DescribeAz)
            {

                oldHighProject.DescribeAz = highProject.DescribeAz;
                checkBool = true;

            }
            if (oldHighProject.DescribeEn != highProject.DescribeEn)
            {

                oldHighProject.DescribeEn = highProject.DescribeEn;
                checkBool = true;

            }
            if (oldHighProject.DescribeRu != highProject.DescribeRu)
            {
                oldHighProject.DescribeRu = highProject.DescribeRu;
                checkBool = true;
            }
            oldHighProject.ModifiedDate = DateTime.UtcNow.AddHours(4);
            if (checkBool)
                await _unitOfWork.CommitAsync();
        }

        public async Task<HighProject> GetHighProject(int id)
        {
            var highProject = await _unitOfWork.HighProjectRepository.GetAsync(x => x.Id == id, "HighProjectImages");
            return highProject;
        }

        public async Task<IEnumerable<HighProjectImage>> GetImages(int id)
        {
            var images = await _unitOfWork.HighProjectImageRepository.GetAllAsync(x => x.HighProjectId == id);
            return images;
        }
        private int PosterImageChange(HighProject highProject, HighProject projectExist)
        {
            if (highProject.ProjectImageFile != null)
            {
                var posterImageFile = highProject.ProjectImageFile;

                HighProjectImage posterImage = projectExist.HighProjectImages.FirstOrDefault(x => x.IsPoster);

                if (posterImage == null) throw new ImageNullException("Şəkil tapılmadı!");

                string filename = _manageImageHelper.FileSave(posterImageFile, "highprojects");
                _manageImageHelper.DeleteFile(posterImage.Image, "highprojects");
                posterImage.Image = filename;
                posterImage.IsPoster = true;
                return 1;
            }
            return 0;

        }
        private async Task<int> CreateImageFormFile(List<IFormFile> imageFiles, int posterId, int deleteCount)
        {
            int countImage = await _unitOfWork.HighProjectImageRepository.GetTotalCountAsync(x => x.HighProjectId == posterId && !x.IsPoster);
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
                            HighProjectImage Posterimage = new HighProjectImage
                            {
                                IsPoster = false,
                                HighProjectId = posterId,
                                Image = _manageImageHelper.FileSave(image, "highprojects"),
                            };
                            await _unitOfWork.HighProjectImageRepository.InsertAsync(Posterimage);
                            i--;
                        }
                    }
                    return 1;
                }
            }
            return 0;
        }

        private int DeleteImages(HighProject poster, HighProject posterExist)
        {
            int i = 0;
            ICollection<HighProjectImage> posterImages = posterExist.HighProjectImages;
            if (poster.ProjectImagesIds != null)
            {
                foreach (var image in posterImages.ToList().Where(x => x.IsDelete == false && !x.IsPoster && !poster.ProjectImagesIds.Contains(x.Id)))
                {
                    _manageImageHelper.DeleteFile(image.Image, "highprojects");
                    posterExist.HighProjectImages.Remove(image);
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
                        _manageImageHelper.DeleteFile(item.Image, "highprojects");
                        posterExist.HighProjectImages.Remove(item);
                        i++;
                    }
                    return i;
                }
                else throw new ImageCountException("Axırıncı şəkil silinə bilməz!");
                //}
                //return i;
            }
        }
        private void Check(HighProject highProject)
        {
            if (highProject.Name.Length < 3)
            {
                throw new ValueFormatExpception("Layihə adının uzunluğu minimum 3 ola bilər");
            }
            if (highProject.Name.Length > 100)
            {
                throw new ValueFormatExpception("Layihə adının uzunluğu maksimum 100 ola bilər");
            }
            if (highProject.DescribeAz.Length > 5000 || highProject.DescribeRu.Length > 5000 || highProject.DescribeEn.Length > 5000)
            {
                throw new ValueFormatExpception("Layihə təsvirinin uzunluğu maksimum 5000 ola bilər");

            }
            if (highProject.DescribeAz.Length < 3 || highProject.DescribeRu.Length < 3 || highProject.DescribeEn.Length < 3)
            {
                throw new ValueFormatExpception("Layihə təsvirinin uzunluğu minimum 3 ola bilər");
            }
        }

    }
}
