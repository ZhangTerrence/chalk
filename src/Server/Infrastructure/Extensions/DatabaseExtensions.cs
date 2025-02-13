using Microsoft.EntityFrameworkCore;
using Server.Data;

namespace Server.Infrastructure.Extensions;

internal static class DatabaseExtensions
{
  public static void AddDatabase(this WebApplicationBuilder builder)
  {
    builder.Services.AddDbContext<DatabaseContext>(options => options
      .UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
      .UseSnakeCaseNamingConvention());
  }
}
