using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Channel = chalk.Server.Entities.Channel;

namespace chalk.Server.Data.Configurations;

public class ChannelConfiguration : IEntityTypeConfiguration<Channel>
{
    public void Configure(EntityTypeBuilder<Channel> builder)
    {
        builder.ToTable("channels");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id).ValueGeneratedOnAdd();
        builder.Property(e => e.Name).HasMaxLength(31);
        builder.Property(e => e.Description).HasMaxLength(255);
        builder.Property(e => e.CreatedDate).IsRequired();
        builder.Property(e => e.UpdatedDate).IsRequired();

        builder
            .HasOne(e => e.Course)
            .WithMany(e => e.Channels)
            .HasForeignKey(e => e.CourseId);
        builder
            .HasMany(e => e.ChannelParticipants)
            .WithOne(e => e.Channel)
            .HasForeignKey(e => e.ChannelId)
            .IsRequired();
        builder
            .HasMany(e => e.ChannelRolePermissions)
            .WithOne(e => e.Channel)
            .HasForeignKey(e => e.ChannelId)
            .IsRequired();
        builder
            .HasMany(e => e.Messages)
            .WithOne(e => e.Channel)
            .HasForeignKey(e => e.ChannelId)
            .IsRequired();
    }
}