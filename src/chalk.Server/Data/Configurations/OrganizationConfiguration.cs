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
        builder.Property(e => e.ProfilePicture);
        builder.Property(e => e.Description).HasMaxLength(255);
        builder.Property(e => e.CreatedDate).IsRequired();
        builder.Property(e => e.UpdatedDate).IsRequired();

        builder
            .HasOne(e => e.Owner)
            .WithMany()
            .HasForeignKey(e => e.OwnerId)
            .IsRequired();
        builder
            .HasMany(e => e.Users)
            .WithOne(e => e.Organization)
            .HasForeignKey(e => e.OrganizationId)
            .IsRequired();
        builder
            .HasMany(e => e.Roles)
            .WithOne(e => e.Organization)
            .HasForeignKey(e => e.OrganizationId)
            .IsRequired();
        builder
            .HasMany(e => e.Courses)
            .WithOne(e => e.Organization)
            .HasForeignKey(e => e.OrganizationId);
        builder
            .HasMany(e => e.Courses)
            .WithOne(e => e.Organization)
            .HasForeignKey(e => e.OrganizationId);
        builder
            .HasMany(e => e.Tags)
            .WithOne()
            .HasForeignKey(e => e.OrganizationId);
    }
}