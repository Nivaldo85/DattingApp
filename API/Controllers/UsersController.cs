using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]//api/users in fata
public class UsersController : ControllerBase
{
    private readonly DataContext _context;

    public UsersController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>>GetUsers(){
        var users=await _context.Users.ToListAsync(); 
        return users;
  }
  [HttpGet("{id}")]//api/users/2 
  public async Task<ActionResult<AppUser>> GetUser(int id){
    var user=  _context.Users.Find(id);
    return user;
  }

    
}
