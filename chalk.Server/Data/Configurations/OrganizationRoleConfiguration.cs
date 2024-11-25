using chalk.Server.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace chalk.Server.Data.Configurations;

public class OrganizationRoleConfiguration : IEntityTypeConfiguration<OrganizationRole>
{
    public void Configure(EntityTypeBuilder<OrganizationRole> builder)
    {
        builder.ToTable("organization_roles");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name)
            .HasMaxLength(31)
            .IsRequired();
        builder.Property(e => e.Description)
            .HasDefaultValue(null)
            .HasMaxLength(255);
        builder.Property(e => e.Permissions)
            .HasDefaultValue(0L)
            .IsRequired();
        builder.Property(e => e.CreatedDate)
            .HasDefaultValue(DateTime.UtcNow)
            .IsRequired();
        builder.Property(e => e.UpdatedDate)
            .HasDefaultValue(DateTime.UtcNow)
            .IsRequired();

        builder
            .HasOne(e => e.Organization)
            .WithMany(e => e.OrganizationRoles)
            .HasForeignKey(e => e.OrganizationId)
            .IsRequired();
        builder
            .HasMany(e => e.UserOrganizations)
            .WithOne(e => e.OrganizationRole)
            .HasForeignKey(e => e.OrganizationRoleId)
            .IsRequired();
    }
}