using chalk.Server.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace chalk.Server.Data.Configurations;

public class OrganizationConfiguration : IEntityTypeConfiguration<Organization>
{
    public void Configure(EntityTypeBuilder<Organization> builder)
    {
        builder.ToTable("organizations");

        builder.HasKey(x => x.Id);

        builder.Property(e => e.Id).ValueGeneratedOnAdd();
        builder.Property(e => e.Name).HasMaxLength(31).IsRequired();
        builder.Property(e => e.ProfilePictureUri);
        builder.Property(e => e.Description).HasMaxLength(255);
        builder.Property(e => e.CreatedDate).IsRequired();
        builder.Property(e => e.UpdatedDate).IsRequired();

        builder
            .HasOne(e => e.Owner)
            .WithMany()
            .HasForeignKey(e => e.OwnerId)
            .IsRequired();
        builder
            .HasMany(e => e.UserOrganizations)
            .WithOne(e => e.Organization)
            .HasForeignKey(e => e.OrganizationId)
            .IsRequired();
        builder
            .HasMany(e => e.OrganizationRoles)
            .WithOne(e => e.Organization)
            .HasForeignKey(e => e.OrganizationId)
            .IsRequired();
        builder
            .HasMany(e => e.Courses)
            .WithOne(e => e.Organization)
            .HasForeignKey(e => e.OrganizationId)
            .IsRequired();
    }
}