using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using XBInsaat.Core.Entites;
using XBInsaat.Data.Configuration;

namespace XBInsaat.Data.Datacontext
{
    public class DataContext : IdentityDbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<ImageSetting> ImageSettings { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<EmailSetting> EmailSettings { get; set; }
        public DbSet<ContactUs> ContactUss { get; set; }
        public DbSet<XBService> XBServices { get; set; }
        public DbSet<HighProjectImage> HighProjectImages { get; set; }
        public DbSet<HighProject> HighProjects { get; set; }
        public DbSet<Camera> Cameras { get; set; }
        public DbSet<MidProject> MidProjects { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<MidProjectImage> MidProjectImages { get; set; }
        public DbSet<NewsImage> NewsImages { get; set; }
        public DbSet<Logger> Loggers { get; set; }
        public DbSet<Career> Careers { get; set; }
        public DbSet<RevolutionSlider> RevolutionSliders { get; set; }
        public DbSet<Localization> Localizations { get; set; }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(LowProjectConfiguration).Assembly);
            base.OnModelCreating(builder);
        }
    }
}
