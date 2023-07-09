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
    public class AdminXBServiceIndexServices : IAdminXBServiceIndexServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public AdminXBServiceIndexServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IQueryable<XBService> GetPoster(string name)
        {
            var poster = _unitOfWork.XBServiceRepository.asQueryable();
            poster = poster.Where(x => !x.IsDelete);

            if (name != null)
                poster = poster.Where(i => EF.Functions.Like(i.NameAz, $"%{name}%"));
            if (name != null)
                poster = poster.Where(i => EF.Functions.Like(i.NameEn, $"%{name}%"));
            if (name != null)
                poster = poster.Where(i => EF.Functions.Like(i.NameRu, $"%{name}%"));

            return poster;
        }
    }
}
