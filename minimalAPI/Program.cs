using minimalAPI.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.UseUsersApi(builder);
builder.Services.AddScoped<IDataAccess, SqlDataAccess>();

var app = builder.Build();
app.MapGet("/", () => "Hello World!");
app.MapUsersApi();


app.Run();
