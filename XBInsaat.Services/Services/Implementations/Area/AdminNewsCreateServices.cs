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

    public class AdminNewsCreateServices : IAdminNewsCreateServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IManageImageHelper _manageImageHelper;

        public AdminNewsCreateServices(IUnitOfWork unitOfWork, IMapper mapper, IManageImageHelper manageImageHelper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _manageImageHelper = manageImageHelper;
        }


        public async Task<News> CreateProject(NewsCreateDto NewsCreateDto)
        {
            var News = _mapper.Map<News>(NewsCreateDto);
            await _unitOfWork.NewsRepository.InsertAsync(News);
            await _unitOfWork.CommitAsync();
            return News;
        }

        public void DtoCheck(NewsCreateDto newsCreateDto)
        {
            if (newsCreateDto == null)
                throw new ItemNullException("Xəta baş verdi.");
            if (newsCreateDto.InstagramUrl != null)
            {
                if (!newsCreateDto.InstagramUrl.Contains("www.") && !newsCreateDto.InstagramUrl.Contains(".com"))
                {
                    throw new ItemFormatException("Zəhmət olmasa linki doğru daxil edin");
                }
            }
            if (newsCreateDto.ImageFiles == null)
            {
                throw new ImageNullException("Şəkil əlavə edin");
            }
            if (newsCreateDto.TitleAz == null || newsCreateDto.TitleEn == null || newsCreateDto.TitleRu == null)
            {
                throw new ItemFormatException("Xəbər adı əlavə edin!");
            }
            if (newsCreateDto.TextAz == null || newsCreateDto.TextRu == null || newsCreateDto.TextEn == null)
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
                NewsImage NewsImage = new NewsImage
                {
                    IsPoster = posterStatus,
                    NewsId = Id,
                    Image = _manageImageHelper.FileSave(image, "News"),
                };
                await _unitOfWork.NewsImageRepository.InsertAsync(NewsImage);
                i++;
            }
            await _unitOfWork.CommitAsync();
        }
    }
}
