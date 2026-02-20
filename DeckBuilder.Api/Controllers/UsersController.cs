using Microsoft.AspNetCore.Mvc;
using DeckBuilder.Infrastructure.Data;
using DeckBuilder.Domain.Entities;
using DeckBuilder.Application.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using AutoMapper;
namespace DeckBuilder.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController: ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public UsersController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody]  CreateUserRequest Request)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            UserName = Request.Username,
            Email = Request.Email,
            CreatedAt = DateTime.UtcNow
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return Ok(user);

    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _context.Users
            .Include(u => u.Decks)
            .ToListAsync();

        var response = _mapper.Map<IEnumerable<UserResponseDTO>>(users);
        return Ok(response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateUserRequest request) 
    {
        
        var user = await _context.Users.FindAsync(id);

        if (user == null) 
        {
            return NotFound("Usuario não encontrado.");
        }

        _mapper.Map(request, user);

        await _context.SaveChangesAsync();

        return Ok(user);

    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        var user = await _context.Users
            .Include(u => u.Decks)
            .FirstOrDefaultAsync(u => u.Id == id);

        if(user == null)
        {
            return NotFound("Usuario nao encontrado.");
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return NoContent();
    }


}

