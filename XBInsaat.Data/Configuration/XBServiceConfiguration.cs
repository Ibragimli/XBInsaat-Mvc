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
    public class XBServiceConfiguration : IEntityTypeConfiguration<XBService>
    {
        public void Configure(EntityTypeBuilder<XBService> builder)
        {
            builder.Property(x => x.NameAz).HasMaxLength(100).IsRequired(true);
            builder.Property(x => x.NameEn).HasMaxLength(100).IsRequired(true);
            builder.Property(x => x.NameRu).HasMaxLength(100).IsRequired(true);
            builder.Property(x => x.DescribeAz).HasMaxLength(5000).IsRequired(true);
            builder.Property(x => x.DescribeEn).HasMaxLength(5000).IsRequired(true);
            builder.Property(x => x.DescribeRu).HasMaxLength(5000).IsRequired(true);
        }
    }
}
