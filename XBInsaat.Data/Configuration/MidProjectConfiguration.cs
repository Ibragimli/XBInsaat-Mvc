﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBInsaat.Core.Entites;

namespace XBInsaat.Data.Configuration
{
  public class MidProjectConfiguration : IEntityTypeConfiguration<MidProject>
    {
        public void Configure(EntityTypeBuilder<MidProject> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(100).IsRequired(true);
            builder.Property(x => x.InstagramUrl).HasMaxLength(200).IsRequired(false);
            builder.Property(x => x.ContactInfo).HasMaxLength(200).IsRequired(false);
            builder.Property(x => x.DescribeAz).HasMaxLength(5000).IsRequired(true);
            builder.Property(x => x.DescribeEn).HasMaxLength(5000).IsRequired(true);
            builder.Property(x => x.DescribeRu).HasMaxLength(5000).IsRequired(true);

        }
    }
}
