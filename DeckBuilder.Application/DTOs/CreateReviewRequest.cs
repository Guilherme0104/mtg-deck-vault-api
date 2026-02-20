
namespace DeckBuilder.Application.DTOs;

public record CreateReviewRequest(int Rating, string Comment, int DeckId, Guid UserId);