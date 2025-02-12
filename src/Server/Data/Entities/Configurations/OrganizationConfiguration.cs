using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Server.Data.Entities.Configurations;

public class OrganizationConfiguration : IEntityTypeConfiguration<Organization>
{
  public void Configure(EntityTypeBuilder<Organization> builder)
  {
    builder.ToTable("organizations");

    builder.HasKey(e => e.Id);

    builder.Property(e => e.Id).ValueGeneratedOnAdd();
    builder.Property(e => e.Name).HasMaxLength(31).IsRequired();
    builder.Property(e => e.Description).HasMaxLength(255);
    builder.Property(e => e.ImageUrl);
    builder.Property(e => e.IsPublic).IsRequired();
    builder.Property(e => e.CreatedOnUtc).IsRequired();
    builder.Property(e => e.UpdatedOnUtc).IsRequired();

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
      .WithOne()
      .HasForeignKey(e => e.OrganizationId)
      .OnDelete(DeleteBehavior.Cascade);
    builder
      .HasMany(e => e.Channels)
      .WithOne()
      .HasForeignKey(e => e.OrganizationId)
      .OnDelete(DeleteBehavior.Cascade);
    builder
      .HasMany(e => e.Courses)
      .WithOne(e => e.Organization)
      .HasForeignKey(e => e.OrganizationId)
      .OnDelete(DeleteBehavior.Cascade);
    builder
      .HasMany(e => e.Tags)
      .WithOne()
      .HasForeignKey(e => e.OrganizationId)
      .OnDelete(DeleteBehavior.Cascade);
  }
}