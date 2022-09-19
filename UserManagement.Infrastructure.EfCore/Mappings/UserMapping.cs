using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserManagement.Domain.AddressAgg;
using UserManagement.Domain.TransactionAgg;
using UserManagement.Domain.UserAgg;

namespace UserManagement.Infrastructure.EfCore.Mappings
{
    public class UserMapping:IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(u => u.UserId);
            builder.Property(u => u.FirstName).IsRequired(false).HasMaxLength(200);
            builder.Property(u => u.LastName).IsRequired(false).HasMaxLength(200);
            builder.Property(u=>u.Email).IsRequired(false).HasMaxLength(200);
            builder.Property(u=>u.Password).IsRequired(false).HasMaxLength(200);
            builder.Property(u => u.NationalNumber).IsRequired(false).HasMaxLength(200);
            builder.Property(u => u.PhoneNumber).IsRequired(false).HasMaxLength(200);
            builder.Property(u=>u.AccountNumber).IsRequired(false).HasMaxLength(200);
            builder.Property(u => u.BirthDate).IsRequired(false);
            builder.Property(u => u.ActivationCode).HasMaxLength(500);
            builder.Property(x => x.AvatarName).HasMaxLength(500);
            builder.Property(u => u.VerificationCode).HasMaxLength(200);
            builder.Property(x => x.RegisterDate);
            builder.Property(u => u.IsActive);
            builder.Property(u => u.IsDeleted);
            builder.Property(u => u.RoleId);
            builder.Property(u => u.RefundType);

            builder.HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId);

            builder.HasMany<Transaction>(u => u.Transactions)
                .WithOne(t => t.User)
                .HasForeignKey(t => t.UserId);

            builder.HasMany<Address>(u => u.Addresses).WithOne(a => a.User)
                .HasForeignKey(a => a.UserId);

            builder.HasQueryFilter(u => !u.IsDeleted);
        }
    }
}
