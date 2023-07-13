using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBInsaat.Core.IUnitOfWork;
using XBInsaat.Service.CustomExceptions;
using XBInsaat.Service.HelperService.Interfaces;
using XBInsaat.Services.Dtos.Area;
using XBInsaat.Services.Services.Interfaces.Area;

namespace XBInsaat.Services.Services.Implementations.Area
{
    public class ImageSettingEditServices : IImageSettingEditServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IManageImageHelper _ImageSettingImage;

        public ImageSettingEditServices(IUnitOfWork unitOfWork, IManageImageHelper ImageSettingImage)
        {
            _unitOfWork = unitOfWork;
            _ImageSettingImage = ImageSettingImage;
        }
        public async Task<ImageSettingEditDto> GetSearch(int Id)
        {
            var ImageSetting = await _unitOfWork.ImageSettingRepository.GetAsync(x => x.Id == Id);
            if (ImageSetting == null)
                throw new ItemNotFoundException("Parametr tapılmadı");


            ImageSettingEditDto ImageSettingEditDto = new ImageSettingEditDto
            {
                Id = ImageSetting.Id,
                Key = ImageSetting.Key,

                Value = ImageSetting.Value,
            };
            return ImageSettingEditDto;
        }

        public async Task ImageSettingEdit(ImageSettingEditDto ImageSettingEdit)
        {
            if (ImageSettingEdit.Value == null)
                throw new ItemNotFoundException("Dəyər adı boş ola bilməz!");

            var lastImageSetting = await _unitOfWork.ImageSettingRepository.GetAsync(x => x.Id == ImageSettingEdit.Id);

            if (lastImageSetting == null)
                throw new ItemNotFoundException("Parametr tapilmadı!");

            if (ImageSettingEdit.Value != null)
                lastImageSetting.Value = ImageSettingEdit.Value;

            lastImageSetting.ModifiedDate = DateTime.UtcNow.AddHours(4);
            await _unitOfWork.CommitAsync();
        }

        public async Task<ImageSettingEditDto> IsExists(int id)
        {
            var ImageSettingExist = await _unitOfWork.ImageSettingRepository.GetAsync(x => x.Id == id);
            if (ImageSettingExist == null)
                throw new ItemNotFoundException("ERROR");
            ImageSettingEditDto editDto = new ImageSettingEditDto
            {
                Value = ImageSettingExist.Value,
                Id = ImageSettingExist.Id,
            };
            return editDto;
        }
    }
}
