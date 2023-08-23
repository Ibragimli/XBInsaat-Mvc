using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBInsaat.Core.Entites;
using XBInsaat.Services.Dtos.Area;
using XBInsaat.Services.Dtos.User;

namespace XBInsaat.Services.Profiles
{
    public class AppProfile : Profile
    {
        public AppProfile()
        {
            CreateMap<HighProjectCreateDto, HighProject>();
            CreateMap<MidProjectCreateDto, MidProject>();
            CreateMap<NewsCreateDto, News>();
            CreateMap<XBServiceCreateDto, XBService>();
            CreateMap<ContactUsCreateDto, ContactUs>();
            CreateMap<LoggerPostDto, Logger>();
            CreateMap<CareerPostDto, Career>();

        }
    }
}
