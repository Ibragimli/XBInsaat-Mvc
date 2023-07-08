using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBInsaat.Services.Dtos.Area;

namespace XBInsaat.Services.Services.Interfaces.Area
{
    public interface ISettingEditServices
    {
        Task SettingEdit(SettingEditDto SettingEdit);
        Task<SettingEditDto> IsExists(int id);
        Task<SettingEditDto> GetSearch(int Id);
    }

}
