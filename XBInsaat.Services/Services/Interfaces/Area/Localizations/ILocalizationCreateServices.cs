using XBInsaat.Core.Entites;
using XBInsaat.Services.Dtos.Area;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBInsaat.Services.Services.Interfaces.Area.Localizations
{
    public interface ILocalizationCreateServices
    {
        Task<Localization> CreateLocalization(LocalizationCreateDto LocalizationCreateDto);

    }
}
