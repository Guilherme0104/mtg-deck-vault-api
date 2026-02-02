using Microsoft.AspNetCore.Mvc;
using DeckBuilder.Application.DTOs;
using DeckBuilder.Infrastructure.Data;
using DeckBuilder.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.ComponentModel;
using AutoMapper;


namespace DeckBuilder.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DecksController : ControllerBase
{
	private readonly AppDbContext _context;
	private readonly IMapper _mapper;

	public DecksController(AppDbContext context, IMapper mapper)
	{
		_context = context;
        _mapper = mapper;
	}

	[HttpPost]
	public async Task<IActionResult> CreateDeck([FromBody] CreateDeckRequest request)
	{
		var deck = _mapper.Map<Deck>(request);
		deck.LastUpdated = DateTime.UtcNow;

		_context.Decks.Add(deck);
		await _context.SaveChangesAsync();

		return Ok(deck);
	}

	[HttpGet("user /{userId}")]
	public async Task<IActionResult> GetUserDecks(Guid userId)
	{

		var decks = await _context.Decks
			.Where(d => d.UserId == userId)
			.ToListAsync();

		return Ok(decks);
	}
}
