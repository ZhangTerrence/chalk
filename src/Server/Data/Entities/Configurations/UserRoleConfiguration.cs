using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Server.Data.Entities.Configurations;

internal class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
  public void Configure(EntityTypeBuilder<UserRole> builder)
  {
    builder.ToTable("user_roles");

    builder.HasKey(e => new { e.UserId, e.RoleId });

    builder
      .HasOne<UserCourse>()
      .WithMany(e => e.Roles)
      .HasForeignKey(e => new { e.UserId, e.CourseId })
      .OnDelete(DeleteBehavior.Cascade);
    builder
      .HasOne<UserOrganization>()
      .WithMany(e => e.Roles)
      .HasForeignKey(e => new { e.UserId, e.OrganizationId })
      .OnDelete(DeleteBehavior.Cascade);
    builder
      .HasOne(e => e.Role)
      .WithMany()
      .HasForeignKey(e => e.RoleId)
      .IsRequired();
  }
}
