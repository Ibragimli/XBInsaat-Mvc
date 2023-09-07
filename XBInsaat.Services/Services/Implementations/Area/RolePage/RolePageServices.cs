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
    public class RolePageServices : IRolePageServices
    {
        private readonly IUnitOfWork _unitOfWork;
        public RolePageServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<string>> GetRolePage(string key)
        {
            List<string> list = new List<string>();

            var rolePage = await _unitOfWork.RolePageIdentityRoleIdRepository.GetAllAsync(x => x.RolePage.Key == key, "RolePage", "IdentityRole");

            if (rolePage != null)
            {
                foreach (var item in rolePage)
                {
                    list.Add(item.IdentityRole.Name);
                }
                return list;
            }
            throw new NotFoundException("404");

        }
    }
}
