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
    public class EmailSettingEditServices : IEmailSettingEditServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IManageImageHelper _emailSettingImage;

        public EmailSettingEditServices(IUnitOfWork unitOfWork, IManageImageHelper emailSettingImage)
        {
            _unitOfWork = unitOfWork;
            _emailSettingImage = emailSettingImage;
        }
        public async Task<EmailSettingEditDto> GetSearch(int Id)
        {
            var emailSetting = await _unitOfWork.EmailSettingRepository.GetAsync(x => x.Id == Id);
            if (emailSetting == null)
                throw new ItemNotFoundException("Parametr tapılmadı");


            EmailSettingEditDto EmailSettingEditDto = new EmailSettingEditDto
            {
                Id = emailSetting.Id,
                Key = emailSetting.Key,
                Value = emailSetting.Value,
            };
            return EmailSettingEditDto;
        }

        public async Task EmailSettingEdit(EmailSettingEditDto EmailSettingEdit)
        {
            if (EmailSettingEdit.Value == null)
                throw new ItemNotFoundException("Dəyər adı boş ola bilməz!");

            var lastEmailSetting = await _unitOfWork.EmailSettingRepository.GetAsync(x => x.Id == EmailSettingEdit.Id);

            if (lastEmailSetting == null)
                throw new ItemNotFoundException("Parametr tapilmadı!");

            if (EmailSettingEdit.Value != null)
            {
                lastEmailSetting.ModifiedDate = DateTime.UtcNow.AddHours(4);
                lastEmailSetting.Value = EmailSettingEdit.Value;
            }

            await _unitOfWork.CommitAsync();
        }

        public async Task<EmailSettingEditDto> IsExists(int id)
        {
            var EmailSettingExist = await _unitOfWork.EmailSettingRepository.GetAsync(x => x.Id == id);
            if (EmailSettingExist == null)
                throw new ItemNotFoundException("ERROR");
            EmailSettingEditDto editDto = new EmailSettingEditDto
            {
                Value = EmailSettingExist.Value,
                Id = EmailSettingExist.Id,
            };
            return editDto;
        }
    }
}
