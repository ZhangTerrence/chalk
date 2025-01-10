using chalk.Server.Data.Configurations;
using chalk.Server.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace chalk.Server.Data;

public class DatabaseContext : IdentityDbContext<User, IdentityRole<long>, long>
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }

    public DbSet<Assignment> Assignments { get; set; }
    public DbSet<AssignmentGroup> AssignmentGroups { get; set; }
    public DbSet<Attachment> Attachments { get; set; }
    public DbSet<Channel> Channels { get; set; }
    public DbSet<ChannelRolePermission> ChannelRolePermissions { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<CourseModule> CourseModules { get; set; }
    public DbSet<CourseRole> CourseRoles { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<Organization> Organizations { get; set; }
    public DbSet<OrganizationRole> OrganizationRoles { get; set; }
    public DbSet<Submission> Submissions { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<UserChannel> UserChannels { get; set; }
    public DbSet<UserCourse> UserCourses { get; set; }
    public DbSet<UserOrganization> UserOrganizations { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<User>(e => e.ToTable("users"));
        builder.Entity<IdentityRole<long>>(e => e.ToTable("roles"));
        builder.Entity<IdentityUserClaim<long>>(e => e.ToTable("user_claims"));
        builder.Entity<IdentityUserToken<long>>(e => e.ToTable("user_tokens"));
        builder.Entity<IdentityUserLogin<long>>(e => e.ToTable("user_logins"));
        builder.Entity<IdentityRoleClaim<long>>(e => e.ToTable("role_claims"));
        builder.Entity<IdentityUserRole<long>>(e => e.ToTable("user_roles"));


        new AssignmentConfiguration().Configure(builder.Entity<Assignment>());
        new AssignmentGroupConfiguration().Configure(builder.Entity<AssignmentGroup>());
        new AttachmentConfiguration().Configure(builder.Entity<Attachment>());
        new ChannelConfiguration().Configure(builder.Entity<Channel>());
        new ChannelRolePermissionConfiguration().Configure(builder.Entity<ChannelRolePermission>());
        new CourseConfiguration().Configure(builder.Entity<Course>());
        new CourseModuleConfiguration().Configure(builder.Entity<CourseModule>());
        new CourseRoleConfiguration().Configure(builder.Entity<CourseRole>());
        new MessageConfiguration().Configure(builder.Entity<Message>());
        new OrganizationConfiguration().Configure(builder.Entity<Organization>());
        new OrganizationRoleConfiguration().Configure(builder.Entity<OrganizationRole>());
        new SubmissionConfiguration().Configure(builder.Entity<Submission>());
        new TagConfiguration().Configure(builder.Entity<Tag>());
        new UserConfiguration().Configure(builder.Entity<User>());
        new UserChannelConfiguration().Configure(builder.Entity<UserChannel>());
        new UserCourseConfiguration().Configure(builder.Entity<UserCourse>());
        new UserOrganizationConfiguration().Configure(builder.Entity<UserOrganization>());
    }
}