using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBInsaat.Core.IUnitOfWork;
using XBInsaat.Service.CustomExceptions;
using XBInsaat.Service.HelperService.Interfaces;
using XBInsaat.Services.Services.Interfaces.Area;

namespace XBInsaat.Services.Services.Implementations.Area
{

    public class AdminDeleteXBServiceServices : IAdminDeleteXBServiceServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public AdminDeleteXBServiceServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task DeleteXBService(int id)
        {
            var project = await _unitOfWork.XBServiceRepository.GetAsync(x => !x.IsDelete && x.Id == id);
            if (project == null)
                throw new ItemNotFoundException("404");

            _unitOfWork.XBServiceRepository.Remove(project);
            await _unitOfWork.CommitAsync();

        }
    }
}
