﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
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
    public class AdminHighProjectCreateServices : IAdminHighProjectCreateServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IManageImageHelper _manageImageHelper;

        public AdminHighProjectCreateServices(IUnitOfWork unitOfWork, IMapper mapper,IManageImageHelper manageImageHelper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _manageImageHelper = manageImageHelper;
        }


        public async Task<HighProject> CreateProject(HighProjectCreateDto highProjectCreateDto)
        {
            var highProject = _mapper.Map<HighProject>(highProjectCreateDto);
            await _unitOfWork.HighProjectRepository.InsertAsync(highProject);
            await _unitOfWork.CommitAsync();
            return highProject;
        }

        public void DtoCheck(HighProjectCreateDto highProjectCreateDto)
        {
            if (highProjectCreateDto == null)
                throw new ItemNullException("Xəta baş verdi.");
            if (highProjectCreateDto.ContactInfo?.Length > 200)
            {
                throw new ValueFormatExpception("Əlaqə məlumatlarının uzunluğu maksimum 200 ola bilər");
            }
            if (highProjectCreateDto.InstagramUrl != null)
            {
                if (!highProjectCreateDto.InstagramUrl.Contains("www.") && !highProjectCreateDto.InstagramUrl.Contains(".com"))
                {
                    throw new ItemFormatException("Zəhmət olmasa linki doğru daxil edin");
                }
            }
            if (highProjectCreateDto.ImageFiles == null)
            {
                throw new ImageNullException("Şəkil əlavə edin");
            }
            if (highProjectCreateDto.Name == null)
            {
                throw new ItemFormatException("Layihə adı əlavə edin!");
            }
            if (highProjectCreateDto.DescribeAz == null)
            {
                throw new ItemFormatException("Təsvir əlavə edin!");
            }
            if (highProjectCreateDto.DescribeEn == null)
            {
                throw new ItemFormatException("Təsvir əlavə edin!");
            }
            if (highProjectCreateDto.DescribeRu == null)
            {
                throw new ItemFormatException("Təsvir əlavə edin!");
            }
        }
        public async Task CreateImageFormFile(List<IFormFile> imageFiles, int Id)
        {
            int i = 1;
            bool posterStatus;
            foreach (var image in imageFiles)
            {
                posterStatus = false;
                if (i == 1)
                    posterStatus = true;
                HighProjectImage highProjectImage = new HighProjectImage
                {
                    IsPoster = posterStatus,
                    HighProjectId = Id,
                    Image = _manageImageHelper.FileSave(image, "highprojects"),
                };
                await _unitOfWork.HighProjectImageRepository.InsertAsync(highProjectImage);
                i++;
            }
            await _unitOfWork.CommitAsync();
        }
    }
}
