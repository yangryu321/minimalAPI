using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using minimalAPI.DataAccess;
using minimalAPI.Models;
using minimalAPI.TokenManager;

public static class UserApiExtensions
{ 


    public static IServiceCollection UseUsersApi( this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddDbContext<DataContext>(options => {
            options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString"));
        });

        services.AddScoped<IDataAccess, SqlDataAccess>(); 


        return services;

    }

    public static WebApplication MapUsersApi(this WebApplication app)
    {

        app.MapGet("/Users", [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] 
        async (IDataAccess dataAccess, TokenManagerMiddleware middleware) =>
        {
            //check if the JWT is in blacklist      

            List<User> users = await dataAccess.GetAllUsersAsync();

            if (users == null)
                return Results.NotFound();

            return Results.Ok(users);
            
        });

        app.MapGet("/Users/{Id:int}", async (IDataAccess dataAccess, int Id) =>
        {
            User user = await dataAccess.GetUserByIdAsync(Id);

            if(user is  null)
                return Results.NotFound();

            return Results.Ok(user);


        });

        app.MapPost("/Users", async (IDataAccess dataAcces, User user) =>
        {
            await dataAcces.CreateUser(user);
        });

        app.MapPut("/Users/{Id:int}", async (IDataAccess dataAccess, int Id, User user) =>
        {
            await dataAccess.UpdateUser(Id, user);
        }
        );

        app.MapDelete("/Users/{Id:int}", async (IDataAccess dataAccess, int Id) =>
        {
           await dataAccess.DeleteUser(Id);
        });

        //add register and log in here

        app.MapPost("/Register", async (IDataAccess dataAccess, User user) =>
        {
            return dataAccess.Register(user);
            
        });



        app.MapGet("/auth", [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] () => "This endpoint requires authorization.");


        app.MapPost("/Login", async (IDataAccess dataAcess, Userlogin user) =>
        {
            return Results.Ok(await dataAcess.Login(user));
            
        });

        app.MapPost("/Logout" , async (IDataAccess dataAccess) =>
        {
            var str = dataAccess.Logout(); 
            return Results.Ok(str.Result);

        });







        return app;

    }
}
