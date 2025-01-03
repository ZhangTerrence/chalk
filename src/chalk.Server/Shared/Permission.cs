namespace chalk.Server.Shared;

public class Permission
{
    public static class Organization
    {
        public const long ViewUsers = 1 << 0;
        public const long InviteUsers = 1 << 2;
        public const long KickUsers = 1 << 3;
        public const long BanUsers = 1 << 4;
        public const long ViewCourses = 1 << 5;
        public const long DeleteCourses = 1 << 6;
        public const long ViewRoles = 1 << 7;
        public const long ManageRoles = 1 << 8;
        public const long DeleteOrganization = 1 << 9;
    }

    public static class Course
    {
    }
}