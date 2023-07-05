using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBInsaat.Core.Entites;

namespace XBInsaat.Data.Configuration
{
    public class MidProjectImageConfiguration : IEntityTypeConfiguration<MidProjectImage>
    {
        public void Configure(EntityTypeBuilder<MidProjectImage> builder)
        {
            builder.Property(x => x.Image).HasMaxLength(120).IsRequired(true);
        }
    }
}
