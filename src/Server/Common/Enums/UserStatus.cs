namespace Server.Common.Enums;

/// <summary>
/// The user's status in a course or organization.
/// </summary>
public enum UserStatus
{
  /// <summary>
  /// The user is invited.
  /// </summary>
  Invited,

  /// <summary>
  /// The user has joined.
  /// </summary>
  Joined,

  /// <summary>
  /// The user is banned.
  /// </summary>
  Banned
}
