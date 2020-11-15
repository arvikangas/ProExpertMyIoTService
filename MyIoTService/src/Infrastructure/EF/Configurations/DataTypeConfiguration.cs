using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyIoTService.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyIoTService.Infrastructure.EF.Configurations
{
    public class DataTypeConfiguration : IEntityTypeConfiguration<DataType>
    {
        public void Configure(EntityTypeBuilder<DataType> builder)
        {
            builder
                .ToTable("datatypes");

            builder
                .HasKey(x => x.Id);
        }
    }
}
