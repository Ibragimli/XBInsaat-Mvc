using AutoMapper;
using XBInsaat.Core.Entites;
using XBInsaat.Core.IUnitOfWork;
using XBInsaat.Services.CustomExceptions;
using XBInsaat.Services.Dtos.Area;
using XBInsaat.Services.HelperService.Interfaces;
using XBInsaat.Services.Services.Interfaces.Area.Localizations;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBInsaat.Service.CustomExceptions;

namespace XBInsaat.Services.Services.Implementations.Area.Localizations
{
    public class LocalizationCreateServices : ILocalizationCreateServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LocalizationCreateServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<Localization> CreateLocalization(LocalizationCreateDto LocalizationCreateDto)
        {
            await DtoCheck(LocalizationCreateDto);
            var Localization = _mapper.Map<Localization>(LocalizationCreateDto);

            await _unitOfWork.LocalizationRepository.InsertAsync(Localization);
            await _unitOfWork.CommitAsync();

            return Localization;
        }

        private async Task DtoCheck(LocalizationCreateDto LocalizationCreateDto)
        {
            if (LocalizationCreateDto.Value == null)
            {
                throw new ItemNullException("Value qeyd edin!");
            }
            if (LocalizationCreateDto.Value?.Length < 2)
            {
                throw new ValueFormatExpception("Value uzunluğu minimum 2 ola bilər");
            }
            if (LocalizationCreateDto.Value?.Length > 5000)
            {
                throw new ValueFormatExpception("Value uzunluğu maksimum 5000 ola bilər");
            }
        }
    }
}
