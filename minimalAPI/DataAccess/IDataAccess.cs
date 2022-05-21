using Microsoft.AspNetCore.Mvc;
using minimalAPI.Models;

public interface IDataAccess
{
    Task<User> CreateUser(User user);
    Task<User> DeleteUser(int Id);
    Task<List<User>> GetAllUsersAsync();
    Task<User> GetUserByIdAsync(int Id);
    Task<User> UpdateUser(int Id, User user);
    Task<User> Register(User user);
    //return a JWT token
    Task<string> Login(Userlogin userlogin);
    //return a message saying the JWT token has been revoked
    Task<string> Logout();
}