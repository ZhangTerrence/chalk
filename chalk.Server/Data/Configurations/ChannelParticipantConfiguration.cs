using chalk.Server.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace chalk.Server.Data.Configurations;

public class ChannelParticipantConfiguration : IEntityTypeConfiguration<ChannelParticipant>
{
    public void Configure(EntityTypeBuilder<ChannelParticipant> builder)
    {
        builder.ToTable("channel_participants");

        builder.HasKey(e => new { e.UserId, e.ChannelId });

        builder.Property(e => e.JoinedDate).IsRequired();

        builder
            .HasOne(e => e.User)
            .WithMany(e => e.ChannelParticipants)
            .HasForeignKey(e => e.UserId)
            .IsRequired();
        builder
            .HasOne(e => e.Channel)
            .WithMany(e => e.ChannelParticipants)
            .HasForeignKey(e => e.ChannelId)
            .IsRequired();
        builder
            .HasOne(e => e.CourseRole)
            .WithMany(e => e.ChannelParticipants)
            .HasForeignKey(e => e.CourseRoleId)
            .IsRequired();
        builder
            .HasOne(e => e.ChannelRolePermission)
            .WithMany(e => e.ChannelParticipants)
            .HasForeignKey(e => new { e.ChannelId, e.CourseRoleId });
        builder
            .HasMany(e => e.Messages)
            .WithOne(e => e.ChannelParticipant)
            .HasForeignKey(e => new { e.UserId, e.ChannelId })
            .IsRequired();
    }
}