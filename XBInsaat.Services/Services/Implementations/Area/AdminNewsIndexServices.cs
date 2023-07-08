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
    public class AdminNewsIndexServices : IAdminNewsIndexServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public AdminNewsIndexServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IQueryable<News> GetPoster(string name)
        {
            var poster = _unitOfWork.NewsRepository.asQueryable("NewsImages");
            poster = poster.Where(x => !x.IsDelete);

            if (name != null)
                poster = poster.Where(i => EF.Functions.Like(i.Title, $"%{name}%"));

            return poster;
        }
    }
}
