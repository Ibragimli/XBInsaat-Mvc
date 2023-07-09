using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBInsaat.Core.Entites;
using XBInsaat.Services.Dtos.Area;

namespace XBInsaat.Services.Services.Interfaces.Area
{
    public interface IAdminXBServiceCreateServices
    {

        Task<XBService> CreateProject(XBServiceCreateDto XBServiceCreateDto);
        void DtoCheck(XBServiceCreateDto XBServiceCreateDto);
    }
}
