using AutoMapper;
using XBInsaat.Core.Entites;
using XBInsaat.Core.IUnitOfWork;
using XBInsaat.Services.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBInsaat.Services.HelperService.Interfaces;

namespace XBInsaat.Services.HelperService.Implementations
{
    public class LoggerServices : ILoggerServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LoggerServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task LoggerCreate(string controller, string action,  string name, string role, string? product = "-", string? username = "-")
        {
            LoggerPostDto newDto = new LoggerPostDto()
            {
                Action = action,
                Controller = controller,
                Name = name,
                Role = role,
                Product = product,
                Username = username,
            };

            await LoggerCreater(newDto);
        }
        private async Task LoggerCreater(LoggerPostDto loggerPostDto)
        {
            var logger = _mapper.Map<Logger>(loggerPostDto);
            await _unitOfWork.LoggerRepository.InsertAsync(logger);
            await _unitOfWork.CommitAsync();
        }
    }
}
