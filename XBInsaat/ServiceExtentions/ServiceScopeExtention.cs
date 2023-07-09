using AutoMapper;
using FluentValidation.AspNetCore;
using XBInsaat.Core.Entites;
using XBInsaat.Core.IUnitOfWork;
using XBInsaat.Core.Repositories;
using XBInsaat.Data.Repositories;
using XBInsaat.Data.UnitOfWork;
using XBInsaat.Service.HelperService.Implementations;
using XBInsaat.Service.HelperService.Interfaces;
using XBInsaat.Services.Dtos.Area;
using XBInsaat.Services.Profiles;
using XBInsaat.Services.Services.Implementations.Area;
using XBInsaat.Services.Services.Interfaces.Area;

namespace XBInsaat.Mvc.ServiceExtentions
{
    public static class ServiceScopeExtention
    {
        public static void AddServiceScopeExtention(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IHighProjectRepository, HighProjectRepository>();
            services.AddScoped<IImageSettingRepository, ImageSettingRepository>();
            services.AddScoped<IHighProjectImageRepository, HighProjectImageRepository>();
            services.AddScoped<ISettingRepository, SettingRepository>();
            services.AddScoped<IAdminHighProjectIndexServices, AdminHighProjectIndexServices>();
            //services.AddScoped<IEmailServices, EmailServices>();
            //services.AddScoped<IProjectCreateServices, ProjectCreateServices>();
            services.AddScoped<IManageImageHelper, ManageImageHelper>();
            services.AddScoped<IImageValue, ImageValue>();
            services.AddScoped<IAdminHighProjectCreateServices, AdminHighProjectCreateServices>();
            services.AddAutoMapper(opt => { opt.AddProfile(new AppProfile()); });

            services.AddScoped<IAdminHighProjectEditServices, AdminHighProjectEditServices>();
            services.AddScoped<IAdminDeleteHighProjectServices, AdminDeleteHighProjectServices>();

            services.AddScoped<IAdminMidProjectEditServices, AdminMidProjectEditServices>();
            services.AddScoped<IAdminDeleteMidProjectServices, AdminDeleteMidProjectServices>();
            services.AddScoped<IAdminMidProjectCreateServices, AdminMidProjectCreateServices>();
            services.AddScoped<IAdminMidProjectIndexServices, AdminMidProjectIndexServices>();
            services.AddScoped<ISettingIndexServices, SettingIndexServices>();
            services.AddScoped<ISettingEditServices, SettingEditServices>();

            services.AddScoped<IAdminNewsEditServices, AdminNewsEditServices>();
            services.AddScoped<IAdminDeleteNewsServices, AdminDeleteNewsServices>();
            services.AddScoped<IAdminNewsCreateServices, AdminNewsCreateServices>();
            services.AddScoped<IAdminNewsIndexServices, AdminNewsIndexServices>();

            services.AddScoped<IAdminXBServiceEditServices, AdminXBServiceEditServices>();
            services.AddScoped<IAdminDeleteXBServiceServices, AdminDeleteXBServiceServices>();
            services.AddScoped<IAdminXBServiceCreateServices, AdminXBServiceCreateServices>();
            services.AddScoped<IAdminXBServiceIndexServices, AdminXBServiceIndexServices>();

            //services.AddScoped<ILayoutServices, LayoutServices>();
            //services.AddScoped<IAnaSehifeIndexServices, AnaSehifeIndexServices>();
            //services.AddScoped<IProjectCreateIndexServices, ProjectCreateIndexServices>();
        }
    }
}
