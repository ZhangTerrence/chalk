using Server.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.AddLogging();
builder.AddRouting();
builder.AddDatabase();
builder.AddIdentity();
builder.AddServices();
builder.AddValidation();
builder.AddExceptionHandler();

var app = builder.Build();
app.UseLogging();
app.UseIdentity();
app.UseRouting();
app.UseExceptionHandler();

app.Run();