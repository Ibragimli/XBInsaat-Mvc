using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBInsaat.Core.Entites;
using XBInsaat.Core.IUnitOfWork;
using XBInsaat.Services.Services.Interfaces.Area;

namespace XBInsaat.Services.Services.Implementations.Area
{
    public class RevolutionSliderIndexServices : IRevolutionSliderIndexServices
    {
        private readonly IUnitOfWork _unitOfWork;
        public RevolutionSliderIndexServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IQueryable<RevolutionSlider> SearchCheck()
        {
            var RevolutionSliderLast = _unitOfWork.RevolutionSliderRepository.asQueryable();
            //if (search != null)
            //{
            //    search = search.ToLower();
            //    if (search != null)
            //        RevolutionSliderLast = RevolutionSliderLast.Where(i => EF.Functions.Like(i.Image, $"%{search}%"));
            //}
            return RevolutionSliderLast;
        }

    }
}
