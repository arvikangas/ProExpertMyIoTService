using Microsoft.EntityFrameworkCore;
using MyIoTService.Domain;
using MyIoTService.Infrastructure.EF.Configurations;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace MyIoTService.Infrastructure.EF
{
    public class MyIoTDbContext : DbContext
    {
        public MyIoTDbContext(DbContextOptions<MyIoTDbContext> options) : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<AccountDevice> AccountDevices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AccountConfiguration());
            modelBuilder.ApplyConfiguration(new AccountDeviceConfiguration());
            modelBuilder.ApplyConfiguration(new DeviceConfiguration());
        }
    }
}
