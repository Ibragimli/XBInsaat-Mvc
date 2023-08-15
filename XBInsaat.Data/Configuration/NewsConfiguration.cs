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
  public class NewsConfiguration : IEntityTypeConfiguration<News>
    {
        public void Configure(EntityTypeBuilder<News> builder)
        {
            builder.Property(x => x.TitleAz).HasMaxLength(100).IsRequired(true);
            builder.Property(x => x.TitleEn).HasMaxLength(100).IsRequired(true);
            builder.Property(x => x.TitleRu).HasMaxLength(100).IsRequired(true);
            builder.Property(x => x.TextAz).HasMaxLength(5000).IsRequired(true);
            builder.Property(x => x.TextEn).HasMaxLength(5000).IsRequired(true);
            builder.Property(x => x.TextRu).HasMaxLength(5000).IsRequired(true);
            builder.Property(x => x.WebsiteUrl).HasMaxLength(200).IsRequired(false);
            builder.Property(x => x.InstagramUrl).HasMaxLength(200).IsRequired(false);

        }
    }
}
