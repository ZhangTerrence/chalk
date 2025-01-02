using chalk.Server.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace chalk.Server.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(e => e.FirstName).HasMaxLength(31).IsRequired();
        builder.Property(e => e.LastName).HasMaxLength(31).IsRequired();
        builder.Property(e => e.DisplayName).HasMaxLength(31).IsRequired();
        builder.Property(e => e.ProfilePicture);
        builder.Property(e => e.Description).HasMaxLength(255);
        builder.Property(e => e.RefreshToken);
        builder.Property(e => e.RefreshTokenExpiryDate);
        builder.Property(e => e.CreatedDate).IsRequired();
        builder.Property(e => e.UpdatedDate).IsRequired();

        builder
            .HasMany(e => e.DirectMessages)
            .WithOne(e => e.User)
            .HasForeignKey(e => e.UserId)
            .IsRequired();
        builder
            .HasMany(e => e.Organizations)
            .WithOne(e => e.User)
            .HasForeignKey(e => e.UserId)
            .IsRequired();
        builder
            .HasMany(e => e.Courses)
            .WithOne(e => e.User)
            .HasForeignKey(e => e.UserId)
            .IsRequired();
    }
}