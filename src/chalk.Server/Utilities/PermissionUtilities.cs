namespace chalk.Server.Utilities;

public static class PermissionUtilities
{
    public const long All = long.MinValue;

    public static long Create(params long[] permissions)
    {
        return Union(permissions);
    }

    public static bool HasPermission(this long permissions, long action)
    {
        return (permissions & action) > 0;
    }

    public static long Union(params long[] permissions)
    {
        return permissions.Aggregate(0L, (aggregate, permission) => aggregate | permission);
    }
}