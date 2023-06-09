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
        public DbSet<XBService> XBServices { get; set; }
        public DbSet<ProjectImage> ProjectImages { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Camera> Cameras { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.ApplyConfigurationsFromAssembly(typeof(ProjectConfiguration).Assembly);
            base.OnModelCreating(builder);
        }
    }
}
