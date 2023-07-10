using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBInsaat.Core.Entites;

namespace XBInsaat.Services.Services.Interfaces.User
{
    public interface ILayoutServices
    {
        public Task<IEnumerable<Setting>> GetSettingsAsync();

    }
}
