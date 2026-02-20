using AutoMapper;
using DeckBuilder.Application.DTOs;
using DeckBuilder.Domain.Entities;
using DeckBuilder.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;


namespace DeckBuilder.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReviewsController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public ReviewsController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> CreateReview([FromBody] CreateReviewRequest request) 
    {

        var deck = await _context.Decks.FindAsync(request.DeckId);

        if(deck == null)
        {
            return NotFound("Deck nao encontrado.");
        }

        if(deck.UserId == request.UserId)
        {
            return BadRequest("Você não pode avaliar seu proprio deck.");
        }


        var review = _mapper.Map<Review>(request);
        review.CreatedAt = DateTime.UtcNow;

        _context.Reviews.Add(review);
        await _context.SaveChangesAsync();

        await _context.Entry(review).Reference(r => r.User).LoadAsync();

        var response = _mapper.Map<ReviewResponseDTO>(review);
        return Ok(response);
    }

    [HttpGet("{id}/reviews")]
    public async Task<IActionResult> GetDeckReviews(int id)
    {
        var deckExists = await _context.Decks.AnyAsync(d => d.Id == id);
        if(!deckExists)
        {
            return NotFound("Deck not found.");
        }

        var reviews = await _context.Reviews
        .Include(r => r.User)
        .Where(r => r.DeckId == id)
        .OrderByDescending(r => r.CreatedAt)
        .ToListAsync();

        var response = _mapper.Map<IEnumerable<ReviewResponseDTO>>(reviews);
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteReview(int id, [FromQuery] Guid userId)
    {
        var review = await _context.Reviews.FindAsync(id);

        if (review == null)
        {
            return NotFound("Review nao encontrada.");
        }

        if (review.UserId != userId)
        {
            return Forbid("Você não tem permissão para deletar essa review.");
        }

        _context.Reviews.Remove(review);
        await _context.SaveChangesAsync();

        return NoContent();
    } 
}