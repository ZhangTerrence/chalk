using chalk.Server.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace chalk.Server.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(e => e.DisplayName)
            .HasMaxLength(31)
            .IsRequired();
        builder.Property(e => e.RefreshToken)
            .HasDefaultValue(null);
        builder.Property(e => e.RefreshTokenExpiryDate)
            .HasDefaultValue(null);
    }
}