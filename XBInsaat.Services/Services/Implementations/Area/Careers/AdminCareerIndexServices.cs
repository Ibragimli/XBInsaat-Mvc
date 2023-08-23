using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBInsaat.Core.Entites;
using XBInsaat.Core.IUnitOfWork;
using XBInsaat.Services.CustomExceptions;
using XBInsaat.Services.Services.Interfaces.Area.Careers;

namespace XBInsaat.Services.Services.Implementations.Area.Careers
{
    public class AdminCareerIndexServices : IAdminCareerIndexServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public AdminCareerIndexServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IQueryable<Career> GetPoster(string name)
        {
            var poster = _unitOfWork.CareerRepository.asQueryable();
            poster = poster.Where(x => !x.IsDelete);

            if (name != null)
                poster = poster.Where(i => EF.Functions.Like(i.Fullname, $"%{name}%"));


            return poster;
        }
    }
}
