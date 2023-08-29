using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBInsaat.Services.Dtos.Area;

namespace XBInsaat.Services.Services.Interfaces.Area.Localizations
{
    public interface ILocalizationEditServices
    {
        Task LocalizationEdit(LocalizationEditDto LocalizationEdit);
        Task<LocalizationEditDto> IsExists(int id);
        Task<LocalizationEditDto> GetSearch(int Id);
    }

}
