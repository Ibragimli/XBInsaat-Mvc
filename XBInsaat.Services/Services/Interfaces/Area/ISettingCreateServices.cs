using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBInsaat.Services.Dtos.Area;

namespace XBInsaat.Services.Services.Interfaces.Area
{
    public interface ISettingCreateServices
    {
        public Task SettingCreate(SettingCreateDto settingCreateDto);
    }
}
