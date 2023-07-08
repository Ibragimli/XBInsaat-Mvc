using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBInsaat.Core.Entites;
using XBInsaat.Core.IUnitOfWork;
using XBInsaat.Service.CustomExceptions;
using XBInsaat.Services.Services.Interfaces.Area;

namespace XBInsaat.Services.Services.Implementations.Area
{
    public class AdminMidProjectIndexServices : IAdminMidProjectIndexServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public AdminMidProjectIndexServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IQueryable<MidProject> GetPoster(string name)
        {
            var poster = _unitOfWork.MidProjectRepository.asQueryable("MidProjectImages","HighProject");
            poster = poster.Where(x => !x.IsDelete);

            if (name != null)
                poster = poster.Where(i => EF.Functions.Like(i.Name, $"%{name}%"));

            return poster;
        }
    }
}
