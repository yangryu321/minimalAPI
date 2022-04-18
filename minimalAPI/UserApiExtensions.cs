using Microsoft.EntityFrameworkCore;
using minimalAPI.DataAccess;
using minimalAPI.Models;

public static class UserApiExtensions
{ 


    public static IServiceCollection UseUsersApi( this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddDbContext<DataContext>(options => {
            options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString"));
        });

        return services;

    }

    public static WebApplication MapUsersApi(this WebApplication app)
    {

        app.MapGet("/Users", async (IDataAccess dataAccess) =>
        {
            List<User> users = await dataAccess.GetAllUsersAsync();
            return users;
        });

        app.MapGet("/Users/{Id:int}", async (IDataAccess dataAccess, int Id) =>
        {
            User user = await dataAccess.GetUserByIdAsync(Id);
            return user;
        });

        app.MapPost("/Users", async (IDataAccess dataAcces, User user) =>
        {
            await dataAcces.CreateUser(user);
        });

        app.MapPut("/Users/{Id:int}", async (IDataAccess dataAcces, int Id, User user) =>
        {
            await dataAcces.UpdateUser(Id, user);
        }
        );

        app.MapDelete("/Users/{Id:int}", async (IDataAccess dataAcces, int Id) =>
        {
           await dataAcces.DeleteUser(Id);
        });

        return app;

    }
}
