using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API;
public class AccountController:BaseApiController
{
    private readonly DataContext _context;

    private readonly ITokenService _tokenService;

    public AccountController(DataContext context, ITokenService tokenService){
        _tokenService=tokenService;
        _context = context;
    }

    [HttpPost("register")]//POST api/account/register
    public async Task<ActionResult<UserDTO>> Register(RegisterDto registerDto){
         if (await UserExists(registerDto.Username)) {return BadRequest("Username is taken");}
    

        using var hmac= new HMACSHA512();

        var user=new AppUser(){
            UserName=registerDto.Username,
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
            PasswordSalt=hmac.Key
        };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return new UserDTO{
            Username=user.UserName,
           Token=_tokenService.CreateToken(user)
        };

    }
    [HttpPost("login")]
    public async Task <ActionResult<UserDTO>> Login(LoginDto loginDto ){

        var user =await _context.Users.SingleOrDefaultAsync(x=> x.UserName==loginDto.Username);
        if(user == null) return Unauthorized("nu e bun useru");

        using var hmac = new HMACSHA512(user.PasswordSalt);

        var computedHash=hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
        for(int i=0;i< computedHash.Length;i++){
            if(computedHash[i] != user.PasswordHash[i]) return Unauthorized("nu e parola buna");
        }
        return new UserDTO{
            Username=user.UserName,
           Token=_tokenService.CreateToken(user)
        };

    }

  private async Task<bool> UserExists(string username)
    {
        return await _context.Users.AnyAsync(x => x.UserName == username.ToLower());
    }
}
