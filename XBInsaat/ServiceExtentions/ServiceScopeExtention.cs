using AutoMapper;
using XBInsaat.Services.Services.Implementations.Area.UserManagers;
using FluentValidation.AspNetCore;
using XBInsaat.Core.Entites;
using XBInsaat.Core.IUnitOfWork;
using XBInsaat.Core.Repositories;
using XBInsaat.Data.Repositories;
using XBInsaat.Data.UnitOfWork;
using XBInsaat.Service.HelperService.Implementations;
using XBInsaat.Service.HelperService.Interfaces;
using XBInsaat.Services.Dtos.Area;
using XBInsaat.Services.HelperService.Implementations;
using XBInsaat.Services.HelperService.Interfaces;
using XBInsaat.Services.Profiles;
using XBInsaat.Services.Services.Implementations;
using XBInsaat.Services.Services.Implementations.Area;
using XBInsaat.Services.Services.Implementations.Area.Loggers;
using XBInsaat.Services.Services.Implementations.User;
using XBInsaat.Services.Services.Interfaces;
using XBInsaat.Services.Services.Interfaces.Area;
using XBInsaat.Services.Services.Interfaces.Area.Loggers;
using XBInsaat.Services.Services.Interfaces.Area.UserManagers;
using XBInsaat.Services.Services.Interfaces.User;
using XBInsaat.Services.Services.Implementations.Area.RoleManagers;
using XBInsaat.Services.Services.Interfaces.Area.RoleManagers;
using XBInsaat.Services.Services.Interfaces.Area.Dashboard;
using XBInsaat.Services.Services.Implementations.Area.Dashboard;
using XBInsaat.Services.Services.Interfaces.Area.Careers;
using XBInsaat.Services.Services.Implementations.Area.Careers;

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
            services.AddScoped<IEmailServices, EmailServices>();
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

            services.AddScoped<IEmailSettingEditServices, EmailSettingEditServices>();
            services.AddScoped<IEmailSettingIndexServices, EmailSettingIndexServices>();

            services.AddScoped<IImageSettingIndexServices, ImageSettingIndexServices>();
            services.AddScoped<IImageSettingEditServices, ImageSettingEditServices>();

            services.AddScoped<IAdminNewsEditServices, AdminNewsEditServices>();
            services.AddScoped<IAdminDeleteNewsServices, AdminDeleteNewsServices>();
            services.AddScoped<IAdminNewsCreateServices, AdminNewsCreateServices>();
            services.AddScoped<IAdminNewsIndexServices, AdminNewsIndexServices>();

            services.AddScoped<IAdminXBServiceEditServices, AdminXBServiceEditServices>();
            services.AddScoped<IAdminDeleteXBServiceServices, AdminDeleteXBServiceServices>();
            services.AddScoped<IAdminXBServiceCreateServices, AdminXBServiceCreateServices>();
            services.AddScoped<IAdminXBServiceIndexServices, AdminXBServiceIndexServices>();


            services.AddScoped<IRevolutionSliderIndexServices, RevolutionSliderIndexServices>();
            services.AddScoped<IRevolutionSliderEditServices, RevolutionSliderEditServices>();

            services.AddScoped<IContactUsRepository, ContactUsRepository>();
            services.AddScoped<IContactUsCreateServices, ContactUsCreateServices>();

            services.AddScoped<IHomeIndexServices, HomeIndexServices>();

            services.AddScoped<IAdminContactUsIndexServices, AdminContactUsIndexServices>();

            services.AddScoped<IAdminUserManagerCreateServices, AdminUserManagerCreateServices>();
            services.AddScoped<IAdminUserManagerDeleteServices, AdminUserManagerDeleteServices>();
            services.AddScoped<IAdminUserManagerEditServices, AdminUserManagerEditServices>();
            services.AddScoped<IAdminUserManagerIndexServices, AdminUserManagerIndexServices>();

            services.AddScoped<IAdminRoleManagerCreateServices, AdminRoleManagerCreateServices>();
            services.AddScoped<IAdminRoleManagerDeleteServices, AdminRoleManagerDeleteServices>();
            services.AddScoped<IAdminRoleManagerEditServices, AdminRoleManagerEditServices>();
            services.AddScoped<IAdminRoleManagerIndexServices, AdminRoleManagerIndexServices>();

            services.AddScoped<IDashboardServices, DashboardServices>();
            services.AddScoped<IAdminCareerIndexServices, AdminCareerIndexServices>();



            services.AddScoped<IAdminLoggerIndexServices, AdminLoggerIndexServices>();
            services.AddScoped<ILoggerServices, LoggerServices>();

            services.AddScoped<IAdminLoginServices, AdminLoginServices>();

            services.AddScoped<ILayoutServices, LayoutServices>();
            services.AddScoped<ICareerServices, CareerServices>();
        }
    }
}
