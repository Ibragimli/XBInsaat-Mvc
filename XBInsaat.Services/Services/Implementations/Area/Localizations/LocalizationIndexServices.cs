using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBInsaat.Core.Entites;
using XBInsaat.Core.IUnitOfWork;
using XBInsaat.Services.Services.Interfaces.Area.Localizations;

namespace Aztamlider.Services.Services.Implementations.Area.Localizations
{
    public class LocalizationIndexServices : ILocalizationIndexServices
    {
        private readonly IUnitOfWork _unitOfWork;
        public LocalizationIndexServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IQueryable<Localization> SearchCheck(string search)
        {
            var LocalizationLast = _unitOfWork.LocalizationRepository.asQueryable();
            if (search != null)
            {
                search = search.ToLower();
                if (search != null)
                    LocalizationLast = LocalizationLast.Where(i => EF.Functions.Like(i.Key, $"%{search}%"));
            }
            return LocalizationLast;
        }

    }
}
