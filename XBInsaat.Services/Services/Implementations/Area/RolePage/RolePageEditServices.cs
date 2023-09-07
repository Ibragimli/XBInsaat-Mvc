using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Data;
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
    public class RolePageEditServices : IRolePageEditServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public RolePageEditServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<RolePageEditDto> GetSearch(int Id)
        {
            var RolePage = await _unitOfWork.RolePageRepository.GetAsync(x => x.Id == Id);
            if (RolePage == null)
                throw new ItemNotFoundException("Parametr tapılmadı");


            RolePageEditDto RolePageEditDto = new RolePageEditDto
            {
                Id = RolePage.Id,
                Key = RolePage.Key,
            };
            return RolePageEditDto;
        }
        public async Task RolePageEdit(RolePageEditDto RolePageEdit)
        {
            bool check = false;

            var rolePageExist = await _unitOfWork.RolePageRepository.GetAsync(x => x.Id == RolePageEdit.Id);
            var existingRolePageIdentityRoleIds = await _unitOfWork.RolePageIdentityRoleIdRepository
                .GetAllAsync(x => x.RolePageId == RolePageEdit.Id);

            if (RolePageEdit.IdentityRoleIds != null)
            {
                var rolePageIdentityRolesDelete = existingRolePageIdentityRoleIds
                    .Where(x => !RolePageEdit.IdentityRoleIds.Contains(x.IdentityRoleId))
                    .ToList();

                if (rolePageIdentityRolesDelete.Any())
                {
                    foreach (var roleId in rolePageIdentityRolesDelete)
                    {
                        _unitOfWork.RolePageIdentityRoleIdRepository.Remove(roleId);
                        check = true;
                    }
                }

                foreach (var roleId in RolePageEdit.IdentityRoleIds)
                {
                    if (!existingRolePageIdentityRoleIds.Any(x => x.RolePageId == rolePageExist.Id && x.IdentityRoleId == roleId))
                    {
                        RolePageIdentityRoleId rolePageIdentityRoleId = new RolePageIdentityRoleId()
                        {
                            IdentityRoleId = roleId,
                            RolePageId = rolePageExist.Id,
                        };
                        await _unitOfWork.RolePageIdentityRoleIdRepository.InsertAsync(rolePageIdentityRoleId);
                        check = true;
                    }
                }
            }

            if (check)
                await _unitOfWork.CommitAsync();

        }

        //public async Task RolePageEdit(RolePageEditDto RolePageEdit)
        //{
        //    bool check = false;

        //    var rolePageExist = await _unitOfWork.RolePageRepository.GetAsync(x => x.Id == RolePageEdit.Id);
        //    var rolePageIdentityRoles = await _unitOfWork.RolePageIdentityRoleIdRepository.GetAllAsync(x => x.RolePageId == RolePageEdit.Id);
        //    var rolePageIdentityRolesDelete = await _unitOfWork.RolePageIdentityRoleIdRepository.GetAllAsync(x => x.RolePageId == RolePageEdit.Id);

        //    if (RolePageEdit.IdentityRoleIds != null)
        //        rolePageIdentityRolesDelete = rolePageIdentityRolesDelete.Where(x => !RolePageEdit.IdentityRoleIds.Contains(x.IdentityRoleId));
        //    if (rolePageIdentityRolesDelete.Count() > 0)
        //        foreach (var roleId in rolePageIdentityRolesDelete)
        //        {
        //            _unitOfWork.RolePageIdentityRoleIdRepository.Remove(roleId);
        //            check = true;
        //        }


        //    if (RolePageEdit.IdentityRoleIds != null)
        //        foreach (var roleId in RolePageEdit.IdentityRoleIds)
        //        {
        //            if (!rolePageIdentityRoles.Any(x => x.RolePageId == rolePageExist.Id && x.IdentityRoleId == roleId))
        //            {
        //                RolePageIdentityRoleId rolePageIdentityRoleId = new RolePageIdentityRoleId()
        //                {
        //                    IdentityRoleId = roleId,
        //                    RolePageId = rolePageExist.Id,
        //                };
        //                await _unitOfWork.RolePageIdentityRoleIdRepository.InsertAsync(rolePageIdentityRoleId);
        //                check = true;
        //            }
        //        }
        //    if (check)
        //        await _unitOfWork.CommitAsync();          
        //}

        public async Task<RolePageEditDto> IsExists(int id)
        {
            var RolePageExist = await _unitOfWork.RolePageRepository.GetAsync(x => x.Id == id);
            if (RolePageExist == null)
                throw new ItemNotFoundException("ERROR");
            RolePageEditDto editDto = new RolePageEditDto
            {
                Id = RolePageExist.Id,
            };
            return editDto;
        }

        public async Task<IEnumerable<IdentityRole>> GetAllRoles()
        {

            var roles = await _unitOfWork.IdentityRoleRepository.GetAllAsync(x => x.Name != "yoxlama");
            if (roles == null)
                throw new ItemNotFoundException("ERROR");

            return roles;
        }

        public async Task<RolePageIdentityRoleId> GetRolePageIdentityRoleId(int Id)
        {
            var rolePageIdentityRoleId = await _unitOfWork.RolePageIdentityRoleIdRepository.GetAsync(x => x.Id == Id);
            //if (rolePageIdentityRoleId == null)
            //    throw new ItemNotFoundException("ERROR");

            return rolePageIdentityRoleId;
        }

        public async Task<IEnumerable<RolePageIdentityRoleId>> GetAllRolePageIdentityRoleIds()
        {
            var rolePageIdentityRoleId = await _unitOfWork.RolePageIdentityRoleIdRepository.GetAllAsync(x => !x.IsDelete, "IdentityRole","RolePage");

            return rolePageIdentityRoleId;
        }
    }
}
