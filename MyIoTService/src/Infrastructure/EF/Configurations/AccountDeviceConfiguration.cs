using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyIoTService.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyIoTService.Infrastructure.EF.Configurations
{
    public class AccountDeviceConfiguration : IEntityTypeConfiguration<AccountDevice>
    {
        public void Configure(EntityTypeBuilder<AccountDevice> builder)
        {
            builder
                .ToTable("accountdevices");

            builder
                .HasKey(x => new { x.AccountId, x.DeviceId });

            builder
                .HasOne(x => x.Account)
                .WithMany()
                .HasForeignKey(x => x.AccountId);

            builder
                .HasOne(x => x.Device)
                .WithMany()
                .HasForeignKey(x => x.DeviceId);

            builder
                .Ignore(x => x.Id);
        }
    }
}
