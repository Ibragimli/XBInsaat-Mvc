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
    public class LowProjectConfiguration : IEntityTypeConfiguration<LowProject>
    {
        public void Configure(EntityTypeBuilder<LowProject> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(100).IsRequired(true);
            builder.Property(x => x.DescribeAz).HasMaxLength(3000).IsRequired(true);
            builder.Property(x => x.DescribeEn).HasMaxLength(3000).IsRequired(true);
            builder.Property(x => x.DescribeRu).HasMaxLength(3000).IsRequired(true);

        }
    }
}
