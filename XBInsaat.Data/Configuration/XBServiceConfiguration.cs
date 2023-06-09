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
            builder.Property(x => x.Name).HasMaxLength(100).IsRequired(true);
            builder.Property(x => x.Describe).HasMaxLength(3000).IsRequired(true);

        }
    }
}
