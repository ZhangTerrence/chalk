using System.Globalization;
using System.Text.Json.Serialization;
using chalk.Server.Entities;

namespace chalk.Server.DTOs;

[Serializable]
[method: JsonConstructor]
public record UserDTO(
    [property: JsonRequired]
    [property: JsonPropertyName("id")]
    long Id,
    [property: JsonRequired]
    [property: JsonPropertyName("firstName")]
    string FirstName,
    [property: JsonRequired]
    [property: JsonPropertyName("lastName")]
    string LastName,
    [property: JsonRequired]
    [property: JsonPropertyName("displayName")]
    string DisplayName,
    [property: JsonRequired]
    [property: JsonPropertyName("joinedDate")]
    string? JoinedDate
)
{
    public UserDTO(User user, string? joinedDate) :
        this(user.Id, user.FirstName, user.LastName, user.DisplayName, joinedDate)
    {
    }
}