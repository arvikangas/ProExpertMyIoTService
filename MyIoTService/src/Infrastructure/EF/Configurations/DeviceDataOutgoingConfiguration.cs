using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyIoTService.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyIoTService.Infrastructure.EF.Configurations
{
    public class DeviceDataOutgoingConfiguration : IEntityTypeConfiguration<DeviceDataOutgoing>
    {
        public void Configure(EntityTypeBuilder<DeviceDataOutgoing> builder)
        {
            builder
                .ToTable("device_data_outgoing");

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
