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
using XBInsaat.Services.Dtos.Area;
using XBInsaat.Services.Services.Interfaces.Area;

namespace XBInsaat.Services.Services.Implementations.Area
{
    public class AdminMidProjectEditServices : IAdminMidProjectEditServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IManageImageHelper _manageImageHelper;

        public AdminMidProjectEditServices(IUnitOfWork unitOfWork, IManageImageHelper manageImageHelper)
        {
            _unitOfWork = unitOfWork;
            _manageImageHelper = manageImageHelper;
        }

        public async Task EditMidProject(MidProject MidProject)
        {
            bool checkBool = false;

            var oldMidProject = await GetMidProject(MidProject.Id);
            if (oldMidProject == null)
                throw new ItemNullException("Layihə tapılmadı!");


            Check(MidProject);
            if (MidProject.ProjectImageFile != null)
                _manageImageHelper.PosterCheck(MidProject.ProjectImageFile);

            if (MidProject.ImageFiles != null)
                _manageImageHelper.ImagesCheck(MidProject.ImageFiles);

            int deleteCount = DeleteImages(MidProject, oldMidProject);
            if (deleteCount > 0)
                checkBool = true;

            if (PosterImageChange(MidProject, oldMidProject) == 1)
                checkBool = true;

            if (await CreateImageFormFile(MidProject.ImageFiles, MidProject.Id, deleteCount) == 1)
                checkBool = true;

            if (oldMidProject.Name != MidProject.Name)
            {
                oldMidProject.Name = MidProject.Name;
                checkBool = true;

            }

            if (oldMidProject?.Row != MidProject?.Row)
            {
                var oldRowProject = await _unitOfWork.MidProjectRepository.GetAsync(x => x.Row == MidProject.Row);
                if (oldRowProject != null && oldRowProject.Row > 0)
                {
                    oldRowProject.Row = oldMidProject.Row;
                    oldMidProject.Row = MidProject.Row;
                    checkBool = true;
                }
            }
            if (oldMidProject.HighProjectId != MidProject.HighProjectId)
            {
                oldMidProject.HighProjectId = MidProject.HighProjectId;
                checkBool = true;

            }
            if (oldMidProject?.InstagramUrl != MidProject?.InstagramUrl)
            {
                oldMidProject.InstagramUrl = MidProject.InstagramUrl;
                checkBool = true;
            }
            if (oldMidProject?.ContactInfo != MidProject?.ContactInfo)
            {
                oldMidProject.ContactInfo = MidProject.ContactInfo;
                checkBool = true;
            }
            if (oldMidProject.DescribeAz != MidProject.DescribeAz)
            {

                oldMidProject.DescribeAz = MidProject.DescribeAz;
                checkBool = true;

            }
            if (oldMidProject.DescribeEn != MidProject.DescribeEn)
            {

                oldMidProject.DescribeEn = MidProject.DescribeEn;
                checkBool = true;

            }
            if (oldMidProject.DescribeRu != MidProject.DescribeRu)
            {
                oldMidProject.DescribeRu = MidProject.DescribeRu;
                checkBool = true;
            }
            oldMidProject.ModifiedDate = DateTime.UtcNow.AddHours(4);
            if (checkBool)
                await _unitOfWork.CommitAsync();
        }


        public async Task<MidProject> GetMidProject(int id)
        {
            var MidProject = await _unitOfWork.MidProjectRepository.GetAsync(x => x.Id == id, "MidProjectImages");
            return MidProject;
        }

        public async Task<IEnumerable<MidProjectImage>> GetImages(int id)
        {
            var images = await _unitOfWork.MidProjectImageRepository.GetAllAsync(x => x.MidProjectId == id);
            return images;
        }
        private int PosterImageChange(MidProject MidProject, MidProject projectExist)
        {
            if (MidProject.ProjectImageFile != null)
            {
                var posterImageFile = MidProject.ProjectImageFile;

                MidProjectImage posterImage = projectExist.MidProjectImages.FirstOrDefault(x => x.IsPoster);

                if (posterImage == null) throw new ImageNullException("Şəkil tapılmadı!");

                string filename = _manageImageHelper.FileSave(posterImageFile, "Midprojects");
                _manageImageHelper.DeleteFile(posterImage.Image, "Midprojects");
                posterImage.Image = filename;
                posterImage.IsPoster = true;
                return 1;
            }
            return 0;

        }
        private async Task<int> CreateImageFormFile(List<IFormFile> imageFiles, int posterId, int deleteCount)
        {
            int countImage = await _unitOfWork.MidProjectImageRepository.GetTotalCountAsync(x => x.MidProjectId == posterId && !x.IsPoster);
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
                            MidProjectImage Posterimage = new MidProjectImage
                            {
                                IsPoster = false,
                                MidProjectId = posterId,
                                Image = _manageImageHelper.FileSave(image, "midprojects"),
                            };
                            await _unitOfWork.MidProjectImageRepository.InsertAsync(Posterimage);
                            i--;
                        }
                    }
                    return 1;
                }
            }
            return 0;
        }
        private int DeleteImages(MidProject poster, MidProject posterExist)
        {
            int i = 0;
            ICollection<MidProjectImage> posterImages = posterExist.MidProjectImages;
            if (poster.ProjectImagesIds != null)
            {
                foreach (var image in posterImages.ToList().Where(x => x.IsDelete == false && !x.IsPoster && !poster.ProjectImagesIds.Contains(x.Id)))
                {
                    _manageImageHelper.DeleteFile(image.Image, "midprojects");
                    posterExist.MidProjectImages.Remove(image);
                    i++;
                }
                posterImages.ToList().RemoveAll(x => !poster.ProjectImagesIds.Contains(x.Id));
                return i;
            }
            else
            {

                if (poster.ImageFiles?.Count() > 0)
                {
                    foreach (var item in posterImages.ToList().Where(x => !x.IsDelete && !x.IsPoster))
                    {
                        _manageImageHelper.DeleteFile(item.Image, "midprojects");
                        posterExist.MidProjectImages.Remove(item);
                        i++;
                    }
                    return i;
                }
                else if (posterImages.Any(x => !x.IsPoster))
                {
                    foreach (var item in posterImages.ToList().Where(x => !x.IsDelete && !x.IsPoster))
                    {
                        _manageImageHelper.DeleteFile(item.Image, "midprojects");
                        posterExist.MidProjectImages.Remove(item);
                        i++;
                    }
                    return i;
                }
                else if (posterImages.Any(x => x.IsPoster))
                {
                    return i;
                }
                else throw new ImageCountException("Axırıncı şəkil silinə bilməz!");
                ;
            }
        }
        private void Check(MidProject MidProject)
        {

            var maxRow = _unitOfWork.MidProjectRepository.MaxRow();
            maxRow.Row = maxRow.Row + 1;
            if (MidProject?.Row < 1)
            {
                throw new ValueFormatExpception("Sıra nömrəsi 0-ola bilməz!!");
            }
            if (MidProject?.Row > maxRow.Row)
            {
                throw new ValueFormatExpception("Sonuncu sıra nömrəsi maksimum " + maxRow.Row + " ola bilər");
            }


            if (MidProject.Name.Length < 3)
            {
                throw new ValueFormatExpception("Layihə adının uzunluğu minimum 3 ola bilər");
            }
            if (MidProject.Name.Length > 100)
            {
                throw new ValueFormatExpception("Layihə adının uzunluğu maksimum 100 ola bilər");
            }
            if (MidProject.DescribeAz.Length > 5000 || MidProject.DescribeRu.Length > 5000 || MidProject.DescribeEn.Length > 5000)
            {
                throw new ValueFormatExpception("Layihə təsvirinin uzunluğu maksimum 5000 ola bilər");

            }
            if (MidProject.DescribeAz.Length < 3 || MidProject.DescribeRu.Length < 3 || MidProject.DescribeEn.Length < 3)
            {
                throw new ValueFormatExpception("Layihə təsvirinin uzunluğu minimum 3 ola bilər");
            }
            if (MidProject.InstagramUrl?.Length > 200)
            {
                throw new ValueFormatExpception("Xəbər mətninin uzunluğu maksimum 200 ola bilər");
            }
            if (MidProject.ContactInfo?.Length > 200)
            {
                throw new ValueFormatExpception("Əlaqə məlumatlarının uzunluğu maksimum 200 ola bilər");
            }
            if (MidProject.InstagramUrl != null)
            {
                if (!MidProject.InstagramUrl.Contains("www.") && !MidProject.InstagramUrl.Contains(".com"))
                {
                    throw new ItemFormatException("Zəhmət olmasa linki doğru daxil edin");
                }
            }
        }

        public int GetMaxRow()
        {
            var project = _unitOfWork.MidProjectRepository.MaxRow();
            return project.Row;
        }
    }
}
