using AutoMapper;
using DeckBuilder.Application.DTOs;
using DeckBuilder.Domain.Entities;
using DeckBuilder.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel;
using System.Threading.Tasks;


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

		if (string.IsNullOrWhiteSpace(deck.CardList))
		{
			deck.CardList = "[]";
        }

		deck.LastUpdated = DateTime.UtcNow;

		_context.Decks.Add(deck);
		await _context.SaveChangesAsync();

		return Ok(deck);
	}

	[HttpGet("user/{userId}")]
	public async Task<IActionResult> GetUserDecks(Guid userId)
	{

		var decks = await _context.Decks
			.Where(d => d.UserId == userId)
			.Select(d => new DeckDetailsResponseDTO(
				d.Id,
				d.Name,
				d.Description,
				d.Format,
				d.CardList,
				d.Reviews.Any() ? Math.Round(d.Reviews.Average(r => r.Rating), 1) : 0.0,
				d.Reviews.Count()
				))
			.ToListAsync();

		if (!decks.Any()) 
		{
			return NotFound("Nenhum Deck encontrado para este usuario.");
		}

		return Ok(decks);
	}

	[HttpPut("{id}")]
	public async Task<IActionResult> UpdateDeck(int id, [FromBody] UpdateDeckRequest request)
	{
		var deck = await _context.Decks.FindAsync(id);

		if(deck == null)
		{
			return NotFound("Deck não encontrado.");
        }

		_mapper.Map(request, deck);
		deck.LastUpdated = DateTime.UtcNow;

		await _context.SaveChangesAsync();
		return Ok(deck);
    }

	[HttpGet("{id}")]
	public async Task<IActionResult> GetDeckById(int id)
	{
		var deck = await _context.Decks
        .Include(d => d.Reviews) // Carrega as reviews para o calculo
        .FirstOrDefaultAsync(d => d.Id == id);

        if (deck == null)
        {
			return NotFound("Deck nao encontrado.");
        }

		double average = deck.Reviews.Any()
			? deck.Reviews.Average(r => r.Rating)
			: 0.0;

		var response = new DeckDetailsResponseDTO(
			deck.Id,
			deck.Name,
			deck.Description ?? "",
			deck.Format,
			deck.CardList,
			Math.Round(average, 1),
			deck.Reviews.Count
            );

		return Ok(response);
    }

	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteDeck(int id, [FromQuery] Guid userId)
	{
		var deck = await _context.Decks.FindAsync(id);

		if(deck == null)
		{
			return NotFound("Deck nao encontrado.");
		}

		if(deck.UserId != userId)
		{
			return Forbid("Voce nao tem permissao para deletar esse deck.");
		}

		_context.Decks.Remove(deck);
		await _context.SaveChangesAsync();

		return NoContent();
    }

}
