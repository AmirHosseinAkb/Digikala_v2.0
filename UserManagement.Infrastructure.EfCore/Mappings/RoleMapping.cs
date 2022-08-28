using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserManagement.Domain.RoleAgg;

namespace UserManagement.Infrastructure.EfCore.Mappings
{
    public class RoleMapping:IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles");
            builder.HasKey(r => r.RoleId);
            builder.Property(r => r.RoleTitle).HasMaxLength(200);
            builder.Property(r=>r.IsDeleted);

            builder.HasMany(r => r.Users)
                .WithOne(u => u.Role)
                .HasForeignKey(u => u.RoleId);

            builder.HasMany(r => r.Permissions)
                .WithOne(p => p.Role).HasForeignKey(p => p.RoleId);

            builder.HasData(
                new Role(1,"مدیر سایت"),
                new Role(2,"دستیار مدیر"),
                new Role(3,"کاربر سایت")
                );

            builder.HasQueryFilter(r => !r.IsDeleted);
        }
    }
}
