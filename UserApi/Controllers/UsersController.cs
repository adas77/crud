using Data;
using Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
namespace csharp_crud_api.Controllers;


[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{

    private readonly UserContext _context;
    private readonly IMapper _mapper;

    public UsersController(UserContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // GET: api/users
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        return await _context.Users.ToListAsync();
    }

    // GET: api/users/5
    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(int id)
    {
        var user = await _context.Users.FindAsync(id);

        if (user == null)
        {
            return NotFound();
        }

        return user;
    }

    // POST: api/users
    [HttpPost]
    public async Task<ActionResult<User>> PostUser(UserDto userDto)
    {
        var user = _mapper.Map<User>(userDto);
        if (IsEmailInUse(user.Email))
        {
            ModelState.AddModelError("Email", "Email is already in use.");
            return BadRequest(ModelState);
        }
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
    }

    // PUT: api/users/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutUser(int id, UserDto userDto)
    {
        var existingUser = await _context.Users.FindAsync(id);

        if (existingUser == null)
        {
            return NotFound();
        }
        if (IsEmailInUse(userDto.Email))
        {
            ModelState.AddModelError("Email", "Email is already in use.");
            return BadRequest(ModelState);
        }

        _mapper.Map(userDto, existingUser);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!UserExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return Ok(existingUser);
    }


    // DELETE: api/users/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool UserExists(int id)
    {
        return _context.Users.Any(e => e.Id == id);
    }

    private bool IsEmailInUse(string email)
    {
        return _context.Users.Any(u => u.Email == email);
    }
}
