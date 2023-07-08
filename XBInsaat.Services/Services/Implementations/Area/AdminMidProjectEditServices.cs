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
    public class AdminMidProjectEditServices : IAdminMidProjectEditServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IManageImageHelper _manageImageHelper;
        private readonly IMapper _mapper;

        public AdminMidProjectEditServices(IUnitOfWork unitOfWork, IManageImageHelper manageImageHelper, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _manageImageHelper = manageImageHelper;
            _mapper = mapper;
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
                                Image = _manageImageHelper.FileSave(image, "Midprojects"),
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



        //private int DeleteImages(MidProject poster, MidProject posterExist)
        //{
        //    int i = 0;
        //    ICollection<MidProjectImage> posterImages = posterExist.MidProjectImages;

        //    if (poster.ProjectImagesIds != null)
        //    {
        //        foreach (var image in posterImages.ToList().Where(x => x.IsDelete == false && !x.IsPoster && !poster.ProjectImagesIds.Contains(x.Id)))
        //        {
        //            _manageImageHelper.DeleteFile(image.Image, "Midprojects");
        //            posterExist.MidProjectImages.Remove(image);
        //            i++;
        //        }
        //        posterImages.ToList().RemoveAll(x => !poster.ProjectImagesIds.Contains(x.Id));
        //        return i;
        //    }
        //    else if (poster.ImageFiles != null && poster.ImageFiles.Count() > 0)
        //    {
        //        foreach (var item in posterImages.ToList().Where(x => !x.IsDelete && !x.IsPoster))
        //        {
        //            _manageImageHelper.DeleteFile(item.Image, "Midprojects");
        //            posterExist.MidProjectImages.Remove(item);
        //            i++;
        //        }
        //        return i;
        //    }
        //    else
        //    {
        //        throw new ImageCountException("Axırıncı şəkil silinə bilməz!");
        //    }
        //}

        private int DeleteImages(MidProject poster, MidProject posterExist)
        {
            int i = 0;
            ICollection<MidProjectImage> posterImages = posterExist.MidProjectImages;
            if (poster.ProjectImagesIds != null)
            {
                foreach (var image in posterImages.ToList().Where(x => x.IsDelete == false && !x.IsPoster && !poster.ProjectImagesIds.Contains(x.Id)))
                {
                    _manageImageHelper.DeleteFile(image.Image, "Midprojects");
                    posterExist.MidProjectImages.Remove(image);
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
                        _manageImageHelper.DeleteFile(item.Image, "Midprojects");
                        posterExist.MidProjectImages.Remove(item);
                        i++;
                    }
                    return i;
                }
                else throw new ImageCountException("Axırıncı şəkil silinə bilməz!");
                //}
                //return i;
            }
        }
        private void Check(MidProject MidProject)
        {
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
            if (MidProject.Name.Length < 3 || MidProject.DescribeAz.Length < 3 || MidProject.DescribeRu.Length < 3 || MidProject.DescribeEn.Length < 3)
            {
                throw new ValueFormatExpception("Layihə təsvirinin uzunluğu minimum 3 ola bilər");
            }
        }

    }
}
