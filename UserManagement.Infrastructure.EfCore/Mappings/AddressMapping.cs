using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserManagement.Domain.AddressAgg;
using UserManagement.Domain.UserAgg;

namespace UserManagement.Infrastructure.EfCore.Mappings
{
    public class AddressMapping:IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("Addresses");
            builder.HasKey(a => a.AddressId);
            builder.Property(a => a.UserId);
            builder.Property(a => a.ReceiverFirstName).HasMaxLength(200);
            builder.Property(a => a.ReceiverLastName).HasMaxLength(200);
            builder.Property(a => a.ReceiverPhoneNumber).HasMaxLength(50);
            builder.Property(a => a.State).HasMaxLength(200);
            builder.Property(a => a.City).HasMaxLength(200);
            builder.Property(a => a.NeighborHood).HasMaxLength(200);
            builder.Property(a => a.Number).HasMaxLength(200);
            builder.Property(a => a.PostCode).HasMaxLength(200);
            builder.Property(a => a.IsDefault);
            builder.Property(a => a.IsDeleted);

            builder.HasOne<User>(a => a.User).WithMany(u => u.Addresses)
                .HasForeignKey(a => a.UserId);

            builder.HasQueryFilter(a => !a.IsDeleted);
        }
    }
}
