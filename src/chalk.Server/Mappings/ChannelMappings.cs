using chalk.Server.DTOs;
using chalk.Server.Entities;

namespace chalk.Server.Mappings;

public static class ChannelMappings
{
    public static ChannelDTO ToDTO(this Channel channel)
    {
        return new ChannelDTO(channel.Id, channel.Name);
    }
}