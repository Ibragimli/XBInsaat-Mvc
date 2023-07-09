using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBInsaat.Core.Entites;
using XBInsaat.Core.IUnitOfWork;
using XBInsaat.Service.CustomExceptions;
using XBInsaat.Service.HelperService.Interfaces;
using XBInsaat.Services.Dtos.Area;
using XBInsaat.Services.Services.Interfaces.Area;

namespace XBInsaat.Services.Services.Implementations.Area
{
    public class AdminXBServiceCreateServices : IAdminXBServiceCreateServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AdminXBServiceCreateServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<XBService> CreateProject(XBServiceCreateDto XBServiceCreateDto)
        {
            var XBService = _mapper.Map<XBService>(XBServiceCreateDto);
            await _unitOfWork.XBServiceRepository.InsertAsync(XBService);
            await _unitOfWork.CommitAsync();
            return XBService;
        }

        public void DtoCheck(XBServiceCreateDto XBServiceCreateDto)
        {
            if (XBServiceCreateDto == null)
                throw new ItemNullException("Xəta baş verdi.");
        }



    }
}
