using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyIoTService.Domain;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace MyIoTService.Infrastructure.EF.Configurations
{
    public class DeviceDataIncomingConfiguration : IEntityTypeConfiguration<DeviceDataIncoming>
    {
        public void Configure(EntityTypeBuilder<DeviceDataIncoming> builder)
        {
            builder
                .ToTable("device_data_incoming");

            builder
                .HasKey(x => new { x.DeviceId, x.TimeStamp, x.DataTypeId });

            builder
                .HasOne(x => x.Device)
                .WithMany()
                .HasForeignKey(x => x.DeviceId);

            builder
                .HasOne(x => x.DataType)
                .WithMany()
                .HasForeignKey(x => x.DataTypeId);

            builder
                .Property(x => x.TimeStamp)
                .IsRequired();
        }
    }
}
