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
    public interface IAdminHighProjectCreateServices
    {

        Task<HighProject> CreateProject(HighProjectCreateDto highProjectCreateDto);
        void DtoCheck(HighProjectCreateDto highProjectCreateDto);
        public Task CreateImageFormFile(List<IFormFile> imageFiles, int Id);

    }
}
