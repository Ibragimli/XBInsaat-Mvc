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
    public class RolePageIndexServices : IRolePageIndexServices
    {
        private readonly IUnitOfWork _unitOfWork;
        public RolePageIndexServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IQueryable<RolePage> SearchCheck(string search)
        {
            var RolePageLast = _unitOfWork.RolePageRepository.asQueryable();
            if (search != null)
            {
                search = search.ToLower();
                if (search != null)
                    RolePageLast = RolePageLast.Where(i => EF.Functions.Like(i.Key, $"%{search}%"));
            }
            return RolePageLast;
        }

    }
}
