
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using minimalAPI.DataAccess;
using minimalAPI.Models;
using minimalAPI.TokenManager;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class SqlDataAccess : IDataAccess
{
    private readonly DataContext _dataContext;
    private readonly ITokenManager tokenManager;

    public SqlDataAccess(DataContext dataContext, ITokenManager tokenManager)
    {
        _dataContext = dataContext;
        this.tokenManager = tokenManager;
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


    public async Task<User> Register( User model)
    {
        var user = new User { Name = model.Name, Email = model.Email, Address = model.Address, Password = model.Password
            , ConfirmPassword = model.ConfirmPassword };
        _dataContext.Add(user);
        await _dataContext.SaveChangesAsync();
        return user;

    }

    public async Task<string> Login(Userlogin userlogin)
    {
        //check if the user if in the database

        var user = await _dataContext.Users.FirstOrDefaultAsync(x => x.Name == userlogin.Name && x.Password == userlogin.Password);

        //if it is not, then return errror message
        if(user == null)
            return "Username or passoword is wrong";


        //if it is, then return a JWT token
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes("My_secret_key_HAHAHAHAHHAHAHAHAHAHAHA");
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, userlogin.Name)
            }),
            Expires = DateTime.UtcNow.AddMinutes(5),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenstring = tokenHandler.WriteToken(token);


        //return Ok(new { Token = tokenstring });
        return tokenstring;

    }

    public async Task<string> Logout()
    {
        await tokenManager.RevokeCurrentJWT();
        return "You have been logged out";
        

    }
}