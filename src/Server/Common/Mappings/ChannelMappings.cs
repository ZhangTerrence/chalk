using System.Globalization;
using Server.Common.DTOs;
using Server.Data.Entities;

namespace Server.Common.Mappings;

internal static class ChannelMappings
{
  public static ChannelDto ToDto(this Channel channel)
  {
    return new ChannelDto(
      channel.Id,
      channel.Name,
      channel.Description,
      channel.CreatedOnUtc.ToString(CultureInfo.CurrentCulture)
    );
  }
}
