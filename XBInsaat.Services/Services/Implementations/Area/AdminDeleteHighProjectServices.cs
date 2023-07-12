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
    public class AdminDeleteHighProjectServices : IAdminDeleteHighProjectServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IManageImageHelper _manageImageHelper;

        public AdminDeleteHighProjectServices(IUnitOfWork unitOfWork, IManageImageHelper manageImageHelper)
        {
            _unitOfWork = unitOfWork;
            _manageImageHelper = manageImageHelper;
        }
        public async Task DeleteHighProject(int id)
        {
            bool check = false;
            var project = await _unitOfWork.HighProjectRepository.GetAsync(x => !x.IsDelete && x.Id == id);
            if (project == null)
                throw new ItemNotFoundException("404");
            var images = await _unitOfWork.HighProjectImageRepository.GetAllAsync(x => x.HighProjectId == project.Id && !x.IsDelete);
            var midProject = _unitOfWork.MidProjectRepository.GetAllAsync(x => x.HighProjectId == id);
            if (midProject == null)
                throw new ItemUseException("Layihə mağazalarda istifadə olunduğu üçün silinmədi.");
            if (images != null)
            {
                foreach (var image in images)
                {
                    _unitOfWork.HighProjectImageRepository.Remove(image);
                    _manageImageHelper.DeleteFile(image.Image, "highprojects");
                }
                check = true;
            }
            if (check)
            {
                _unitOfWork.HighProjectRepository.Remove(project);
                await _unitOfWork.CommitAsync();
            }

        }
    }
}
