using minimalAPI.Models;

public interface IDataAccess
{
    Task<User> CreateUser(User user);
    Task<User> DeleteUser(int Id);
    Task<List<User>> GetAllUsersAsync();
    Task<User> GetUserByIdAsync(int Id);
    Task<User> UpdateUser(int Id, User user);
}