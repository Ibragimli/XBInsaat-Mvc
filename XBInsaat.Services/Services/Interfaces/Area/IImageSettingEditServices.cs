using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBInsaat.Services.Dtos.Area;

namespace XBInsaat.Services.Services.Interfaces.Area
{
    public interface IImageSettingEditServices
    {
        Task ImageSettingEdit(ImageSettingEditDto ImageSettingEdit);
        Task<ImageSettingEditDto> IsExists(int id);
        Task<ImageSettingEditDto> GetSearch(int Id);
    }

}
