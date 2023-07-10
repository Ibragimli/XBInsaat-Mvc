using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBInsaat.Core.Entites;
using XBInsaat.Core.IUnitOfWork;
using XBInsaat.Data.Datacontext;
using XBInsaat.Data.UnitOfWork;
using XBInsaat.Services.Services.Interfaces.User;

namespace XBInsaat.Services.Services.Implementations.User
{
    public class LayoutServices : ILayoutServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public LayoutServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Setting>> GetSettingsAsync()
        {
            return await _unitOfWork.SettingRepository.GetAllAsync(x => !x.IsDelete);
        }

    }
}
