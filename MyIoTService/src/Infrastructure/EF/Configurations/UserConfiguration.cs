using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyIoTService.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyIoTService.Infrastructure.EF.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .ToTable("users");

            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Name)
                .IsRequired();
        }
    }
}
