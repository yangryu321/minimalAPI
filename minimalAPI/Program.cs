using Microsoft.EntityFrameworkCore;
using minimalAPI.DataAccess;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<DataContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString"));
});

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/Users", (DataContext dataContext) =>
{
    return dataContext.Users.ToListAsync();
});

app.Run();
