using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBInsaat.Core.Entites;
using XBInsaat.Core.IUnitOfWork;
using XBInsaat.Service.CustomExceptions;
using XBInsaat.Service.HelperService.Implementations;
using XBInsaat.Service.HelperService.Interfaces;
using XBInsaat.Services.Dtos.Area;
using XBInsaat.Services.Services.Interfaces.Area;

namespace XBInsaat.Services.Services.Implementations.Area
{
    public class RevolutionSliderEditServices : IRevolutionSliderEditServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IManageImageHelper _manageImageHelper;

        public RevolutionSliderEditServices(IUnitOfWork unitOfWork, IManageImageHelper manageImageHelper)
        {
            _unitOfWork = unitOfWork;
            _manageImageHelper = manageImageHelper;
        }
        public async Task<RevolutionSlider> GetSearch(int Id)
        {
            var RevolutionSlider = await _unitOfWork.RevolutionSliderRepository.GetAsync(x => x.Id == Id);
            if (RevolutionSlider == null)
                throw new ItemNotFoundException("Slider tapılmadı");
            return RevolutionSlider;
        }

        public async Task RevolutionSliderEdit(RevolutionSlider revolutionSliderEdit)
        {

            var lastRevolutionSlider = await _unitOfWork.RevolutionSliderRepository.GetAsync(x => x.Id == revolutionSliderEdit.Id && !x.IsDelete);

            if (lastRevolutionSlider == null)
                throw new ItemNotFoundException("Slider tapılmadı!");

            PosterImageChange(revolutionSliderEdit, lastRevolutionSlider);
            lastRevolutionSlider.ModifiedDate = DateTime.UtcNow.AddHours(4);
            await _unitOfWork.CommitAsync();
        }

        private void PosterImageChange(RevolutionSlider revolutionSlider, RevolutionSlider revolutionSliderExist)
        {
            var posterImageFile = revolutionSlider.ImageFile;
            string filename = _manageImageHelper.FileSave(posterImageFile, "revolutionSlider");
            _manageImageHelper.DeleteFile(revolutionSliderExist.Image, "revolutionSlider");
            revolutionSliderExist.Image = filename;
        }
    }
}
