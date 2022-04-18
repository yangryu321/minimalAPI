using Microsoft.EntityFrameworkCore;
using minimalAPI.DataAccess;
using minimalAPI.Models;

public class SqlDataAccess : IDataAccess
{
    private readonly DataContext _dataContext;

    public SqlDataAccess(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<List<User>> GetAllUsersAsync()
    {
        List<User> users = await _dataContext.Users.ToListAsync();
        return users;
    }

    public async Task<User> GetUserByIdAsync(int Id)
    {
        var user = await _dataContext.Users.FirstOrDefaultAsync(x => x.Id == Id);
        return user;
    }

    public async Task<User> CreateUser(User user)
    {
        await _dataContext.Users.AddAsync(user);
        await _dataContext.SaveChangesAsync();
        return user;

    }

    public async Task<User> UpdateUser(int Id, User user)
    {
       
        _dataContext.Update(user);
        await _dataContext.SaveChangesAsync();
        return user;

    }

    public async Task<User> DeleteUser(int Id)
    {
        var user = await _dataContext.Users.FindAsync(Id);

        _dataContext.Users.Remove(user);
        await _dataContext.SaveChangesAsync();

        return user;

    }



}