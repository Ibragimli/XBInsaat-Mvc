using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBInsaat.Core.Entites;
using XBInsaat.Core.IUnitOfWork;
using XBInsaat.Services.Services.Interfaces.Area;

namespace XBInsaat.Services.Services.Implementations.Area
{
    public class EmailSettingIndexServices : IEmailSettingIndexServices
    {
        private readonly IUnitOfWork _unitOfWork;
        public EmailSettingIndexServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IQueryable<EmailSetting> SearchCheck(string search)
        {
            var emailSettingLast = _unitOfWork.EmailSettingRepository.asQueryable();
            if (search != null)
            {
                search = search.ToLower();
                if (search != null)
                    emailSettingLast = emailSettingLast.Where(i => EF.Functions.Like(i.Key, $"%{search}%"));
            }
            return emailSettingLast;
        }

    }
}
