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
                .HasKey(x => new { x.DeviceId, x.TimeStamp, x.DataType });

            builder
                .HasOne(x => x.Device)
                .WithMany()
                .HasForeignKey(x => x.DeviceId);

            builder
                .Property(x => x.TimeStamp)
                .IsRequired();

            builder
                .Ignore(x => x.Id);
        }
    }
}
