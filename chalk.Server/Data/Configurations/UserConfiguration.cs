using chalk.Server.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace chalk.Server.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(e => e.FirstName)
            .HasMaxLength(31)
            .IsRequired();
        builder.Property(e => e.LastName)
            .HasMaxLength(31)
            .IsRequired();
        builder.Property(e => e.FullName)
            .HasMaxLength(63)
            .IsRequired();
        builder.Property(e => e.DisplayName)
            .HasMaxLength(31)
            .IsRequired();
        builder.Property(e => e.ProfilePicture)
            .HasDefaultValue(null);
        builder.Property(e => e.Description)
            .HasDefaultValue(null)
            .HasMaxLength(255);
        builder.Property(e => e.RefreshToken)
            .HasDefaultValue(null);
        builder.Property(e => e.RefreshTokenExpiryDate)
            .HasDefaultValue(null);
        builder.Property(e => e.CreatedDate)
            .HasDefaultValue(DateTime.UtcNow)
            .IsRequired();
        builder.Property(e => e.UpdatedDate)
            .HasDefaultValue(DateTime.UtcNow)
            .IsRequired();

        builder
            .HasMany(e => e.UserOrganizations)
            .WithOne(e => e.User)
            .HasForeignKey(e => e.UserId)
            .IsRequired();
        builder
            .HasMany(e => e.UserCourses)
            .WithOne(e => e.User)
            .HasForeignKey(e => e.UserId)
            .IsRequired();
        builder
            .HasMany(e => e.ChannelParticipants)
            .WithOne(e => e.User)
            .HasForeignKey(e => e.UserId)
            .IsRequired();
    }
}