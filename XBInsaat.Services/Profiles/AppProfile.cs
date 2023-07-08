using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBInsaat.Core.Entites;
using XBInsaat.Services.Dtos.Area;

namespace XBInsaat.Services.Profiles
{
    public class AppProfile : Profile
    {
        public AppProfile()
        {
            CreateMap<HighProjectCreateDto, HighProject>();
            CreateMap<MidProjectCreateDto, MidProject>();

        }
    }
}
