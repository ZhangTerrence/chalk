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
            .HasMany(e => e.Users)
            .WithOne(e => e.Channel)
            .HasForeignKey(e => e.ChannelId)
            .IsRequired();
        builder
            .HasMany(e => e.Messages)
            .WithOne()
            .HasForeignKey(e => e.ChannelId)
            .IsRequired();
        builder
            .HasMany(e => e.RolePermissions)
            .WithOne()
            .HasForeignKey(e => e.ChannelId)
            .IsRequired();
    }
}