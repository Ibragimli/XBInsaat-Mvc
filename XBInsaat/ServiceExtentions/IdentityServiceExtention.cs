using XBInsaat.Core.Entites;
using XBInsaat.Data.Datacontext;
using Microsoft.AspNetCore.Identity;

namespace XBInsaat.Mvc.ServiceExtentions
{
    public static class IdentityServiceExtention
    {
        public static void AddIdentityServiceExtention(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, IdentityRole>(opt =>
            {
                opt.Password.RequiredUniqueChars = 0;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.User.RequireUniqueEmail = false;
            }).AddDefaultTokenProviders().AddEntityFrameworkStores<DataContext>();

        }
    }
}
