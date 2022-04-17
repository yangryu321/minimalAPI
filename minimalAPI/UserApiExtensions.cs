using Microsoft.EntityFrameworkCore;
using minimalAPI.DataAccess;
using minimalAPI.Models;

public static class UserApiExtensions
{ 

    public static WebApplication MapUsersApi(this WebApplication app)
    {

        app.MapGet("/", () => "Hello World!");

        app.MapGet("/Users", async (DataContext dataContext) =>
        {
            return await dataContext.Users.ToListAsync();
        });

        app.MapGet("/Users/{Id:int}", async (DataContext datacontext, int Id) =>
        {
            return  await datacontext.Users.FindAsync(Id);
           

        });

        app.MapPost("/Users", async (DataContext datacontext, User user) =>
        {
            await datacontext.Users.AddAsync(user);
            await datacontext.SaveChangesAsync();
            Results.Accepted();

        });

        app.MapPut("/Users/{Id:int}", async (DataContext datacontext, int Id, User user) =>
        {
            if (Id != user.Id)
                return Results.BadRequest();

            datacontext.Update(user);
            await datacontext.SaveChangesAsync();

            return Results.NoContent();
        }
        );

        app.MapDelete("/Users/{Id:int}", async (DataContext dataContext, int Id) =>
        {
            var user = await dataContext.Users.FindAsync(Id);
            if (user is null)
                return Results.NotFound();

            dataContext.Users.Remove(user);
            await dataContext.SaveChangesAsync();

            return Results.NoContent();

        });
        return app;

    }
}
