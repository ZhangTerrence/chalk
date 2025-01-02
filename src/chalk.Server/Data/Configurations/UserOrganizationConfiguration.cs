using chalk.Server.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace chalk.Server.Data.Configurations;

public class UserOrganizationConfiguration : IEntityTypeConfiguration<UserOrganization>
{
    public void Configure(EntityTypeBuilder<UserOrganization> builder)
    {
        builder.ToTable("user_organizations");

        builder.HasKey(e => new { e.UserId, e.OrganizationId });

        builder.Property(e => e.Status).IsRequired();
        builder.Property(e => e.JoinedDate);

        builder
            .HasOne(e => e.User)
            .WithMany(e => e.Organizations)
            .HasForeignKey(e => e.UserId)
            .IsRequired();
        builder
            .HasOne(e => e.Organization)
            .WithMany(e => e.Users)
            .HasForeignKey(e => e.OrganizationId)
            .IsRequired();
        builder
            .HasOne(e => e.Role)
            .WithMany(e => e.Users)
            .HasForeignKey(e => e.OrganizationRoleId)
            .IsRequired();
    }
}