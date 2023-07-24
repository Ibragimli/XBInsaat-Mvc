using AutoMapper;
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
    public class AdminMidProjectCreateServices : IAdminMidProjectCreateServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IManageImageHelper _manageImageHelper;

        public AdminMidProjectCreateServices(IUnitOfWork unitOfWork, IMapper mapper, IManageImageHelper manageImageHelper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _manageImageHelper = manageImageHelper;
        }


        public async Task<MidProject> CreateProject(MidProjectCreateDto MidProjectCreateDto)
        {
            var midExist =  _unitOfWork.MidProjectRepository.MaxRow();
            var rowMax = midExist.Row;

            var midProject = _mapper.Map<MidProject>(MidProjectCreateDto);
            midProject.Row = rowMax + 1;
            await _unitOfWork.MidProjectRepository.InsertAsync(midProject);
            await _unitOfWork.CommitAsync();
            return midProject;
        }

        public async Task DtoCheck(MidProjectCreateDto MidProjectCreateDto)
        {
            if (MidProjectCreateDto == null)
                throw new ItemNullException("Xəta baş verdi.");
            if (!await _unitOfWork.HighProjectRepository.IsExistAsync(x => x.Id == MidProjectCreateDto.HighProjectId))
                throw new ItemNotFoundException("Layihə tapılmadı");
            if (MidProjectCreateDto.ImageFiles == null)
            {
                throw new ImageNullException("Şəkil əlavə edin");
            }
            if (MidProjectCreateDto.Name == null)
            {
                throw new ItemFormatException("Layihə adı əlavə edin!");
            }
            if (MidProjectCreateDto.DescribeAz == null)
            {
                throw new ItemFormatException("Təsvir əlavə edin!");
            }
            if (MidProjectCreateDto.DescribeEn == null)
            {
                throw new ItemFormatException("Təsvir əlavə edin!");
            }
            if (MidProjectCreateDto.DescribeRu == null)
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
                MidProjectImage MidProjectImage = new MidProjectImage
                {
                    IsPoster = posterStatus,
                    MidProjectId = Id,
                    Image = _manageImageHelper.FileSave(image, "midprojects"),
                };
                await _unitOfWork.MidProjectImageRepository.InsertAsync(MidProjectImage);
                i++;
            }
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<HighProject>> GetAllHighProjects()
        {
            var highProjects = await _unitOfWork.HighProjectRepository.GetAllAsync(x => !x.IsDelete);
            return highProjects;
        }


    }
}
