using XBInsaat.Core.IUnitOfWork;
using XBInsaat.Core.Repositories;
using XBInsaat.Data.Repositories;
using XBInsaat.Data.UnitOfWork;

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
            //services.AddScoped<IEmailServices, EmailServices>();
            //services.AddScoped<IProjectCreateServices, ProjectCreateServices>();
            //services.AddScoped<IManageImageHelper, ManageImageHelper>();
            //services.AddScoped<IImageValue, ImageValue>();
            //services.AddScoped<ILayoutServices, LayoutServices>();
            //services.AddScoped<IAnaSehifeIndexServices, AnaSehifeIndexServices>();
            //services.AddScoped<IProjectCreateIndexServices, ProjectCreateIndexServices>();
        }
    }
}
