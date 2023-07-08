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
    public class AdminDeleteMidProjectServices : IAdminDeleteMidProjectServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IManageImageHelper _manageImageHelper;

        public AdminDeleteMidProjectServices(IUnitOfWork unitOfWork, IManageImageHelper manageImageHelper)
        {
            _unitOfWork = unitOfWork;
            _manageImageHelper = manageImageHelper;
        }

        public async Task DeleteMidProject(int id)
        {
            bool check = false;
            var project = await _unitOfWork.MidProjectRepository.GetAsync(x => !x.IsDelete && x.Id == id);
            if (project == null)
                throw new ItemNotFoundException("404");
            var images = await _unitOfWork.MidProjectImageRepository.GetAllAsync(x => x.MidProjectId == project.Id && !x.IsDelete);

            if (images != null)
            {
                foreach (var image in images)
                {
                    _unitOfWork.MidProjectImageRepository.Remove(image);
                    _manageImageHelper.DeleteFile(image.Image, "Midprojects");
                }
                check = true;
            }
            if (check)
            {
                _unitOfWork.MidProjectRepository.Remove(project);
                await _unitOfWork.CommitAsync();
            }
        }
    }
}
