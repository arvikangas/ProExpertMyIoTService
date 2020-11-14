﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyIoTService.Infrastructure.EF;

namespace MyIoTService.Infrastructure.EF.Migrations
{
    [DbContext(typeof(MyIoTDbContext))]
    partial class MyIoTDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("MyIoTService.Domain.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("accounts");
                });

            modelBuilder.Entity("MyIoTService.Domain.AccountDevice", b =>
                {
                    b.Property<Guid>("AccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DeviceId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("AccountId", "DeviceId");

                    b.HasIndex("DeviceId");

                    b.ToTable("accountdevices");
                });

            modelBuilder.Entity("MyIoTService.Domain.Device", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.ToTable("devices");
                });

            modelBuilder.Entity("MyIoTService.Domain.AccountDevice", b =>
                {
                    b.HasOne("MyIoTService.Domain.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyIoTService.Domain.Device", "Device")
                        .WithMany()
                        .HasForeignKey("DeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Device");
                });
#pragma warning restore 612, 618
        }
    }
}
