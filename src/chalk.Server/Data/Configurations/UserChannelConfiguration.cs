using chalk.Server.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace chalk.Server.Data.Configurations;

public class UserChannelConfiguration : IEntityTypeConfiguration<UserChannel>
{
    public void Configure(EntityTypeBuilder<UserChannel> builder)
    {
        builder.ToTable("user_channels");

        builder.HasKey(e => new { e.UserId, e.ChannelId });

        builder.Property(e => e.JoinedDate).IsRequired();

        builder
            .HasOne(e => e.User)
            .WithMany(e => e.DirectMessages)
            .HasForeignKey(e => e.UserId)
            .IsRequired();
        builder
            .HasOne(e => e.Channel)
            .WithMany(e => e.Users)
            .HasForeignKey(e => e.ChannelId)
            .IsRequired();
        builder
            .HasOne(e => e.CourseRole)
            .WithMany(e => e.Channels)
            .HasForeignKey(e => e.CourseRoleId);
        builder
            .HasOne(e => e.OrganizationRole)
            .WithMany(e => e.Channels)
            .HasForeignKey(e => e.OrganizationRoleId);
        builder
            .HasOne(e => e.RolePermission)
            .WithMany(e => e.Users)
            .HasForeignKey(e => new { e.ChannelId, e.CourseRoleId });
        builder
            .HasMany(e => e.Messages)
            .WithOne(e => e.User)
            .HasForeignKey(e => new { e.UserId, e.ChannelId })
            .IsRequired();
    }
}