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
    public class ImageSettingConfiguration : IEntityTypeConfiguration<ImageSetting>
    {
        public void Configure(EntityTypeBuilder<ImageSetting> builder)
        {
            builder.Property(x => x.Key).HasMaxLength(100).IsRequired(true);
            builder.Property(x => x.Value).HasMaxLength(500).IsRequired(true);
        }
    }
}
