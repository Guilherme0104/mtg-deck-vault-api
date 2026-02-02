using Microsoft.AspNetCore.Mvc;
using DeckBuilder.Infrastructure.Data;

using DeckBuilder.Domain.Entities;
using DeckBuilder.Application.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DeckBuilder.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController: ControllerBase
{
    private readonly AppDbContext _context;

    public UsersController(AppDbContext context)
    {
        _context = context;
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
        var users = await _context.Users.ToListAsync();
        return Ok(users);
    }
       


}

