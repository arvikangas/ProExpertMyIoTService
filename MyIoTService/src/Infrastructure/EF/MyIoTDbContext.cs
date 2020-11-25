using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyIoTService.Domain;
using MyIoTService.Infrastructure.EF.Configurations;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace MyIoTService.Infrastructure.EF
{
    public class MyIoTDbContext : IdentityDbContext<Account, IdentityRole<Guid>, Guid>
    {
        public MyIoTDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<AccountDevice> AccountDevices { get; set; }
        public DbSet<DeviceDataIncoming> DeviceDataIncoming { get; set; }
        public DbSet<DeviceDataOutgoing> DeviceDataOutgoing { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new AccountConfiguration());
            modelBuilder.ApplyConfiguration(new AccountDeviceConfiguration());
            modelBuilder.ApplyConfiguration(new DeviceConfiguration());
            modelBuilder.ApplyConfiguration(new DeviceDataIncomingConfiguration());
            modelBuilder.ApplyConfiguration(new DeviceDataOutgoingConfiguration());
        }
    }
}
