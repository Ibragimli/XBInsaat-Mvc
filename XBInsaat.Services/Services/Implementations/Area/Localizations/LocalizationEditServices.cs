using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBInsaat.Core.IUnitOfWork;
using XBInsaat.Services.CustomExceptions;
using XBInsaat.Services.HelperService.Interfaces;
using XBInsaat.Services.Dtos.Area;
using XBInsaat.Services.Services.Interfaces.Area.Localizations;
using XBInsaat.Service.CustomExceptions;

namespace Aztamlider.Services.Services.Implementations.Area.Localizations
{
    public class LocalizationEditServices : ILocalizationEditServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public LocalizationEditServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<LocalizationEditDto> GetSearch(int Id)
        {
            var Localization = await _unitOfWork.LocalizationRepository.GetAsync(x => x.Id == Id);
            if (Localization == null)
                throw new ItemNotFoundException("Parametr tapılmadı");


            LocalizationEditDto LocalizationEditDto = new LocalizationEditDto
            {
                Id = Localization.Id,
                Key = Localization.Key,
                Value = Localization.Value,
            };
            return LocalizationEditDto;
        }

        public async Task LocalizationEdit(LocalizationEditDto LocalizationEdit)
        {
            if (LocalizationEdit.Value == null)
                throw new ItemNotFoundException("Dəyər adı boş ola bilməz!");

            var lastLocalization = await _unitOfWork.LocalizationRepository.GetAsync(x => x.Id == LocalizationEdit.Id);

            if (lastLocalization == null)
                throw new ItemNotFoundException("Parametr tapilmadı!");

            if (LocalizationEdit.Value != null)
                lastLocalization.Value = LocalizationEdit.Value;


            lastLocalization.ModifiedDate = DateTime.UtcNow.AddHours(4);
            await _unitOfWork.CommitAsync();
        }

        public async Task<LocalizationEditDto> IsExists(int id)
        {
            var LocalizationExist = await _unitOfWork.LocalizationRepository.GetAsync(x => x.Id == id);
            if (LocalizationExist == null)
                throw new ItemNotFoundException("ERROR");
            LocalizationEditDto editDto = new LocalizationEditDto
            {
                Value = LocalizationExist.Value,
                Id = LocalizationExist.Id,
            };
            return editDto;
        }
    }
}
