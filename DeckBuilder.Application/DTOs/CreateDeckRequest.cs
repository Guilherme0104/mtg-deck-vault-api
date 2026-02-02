
namespace DeckBuilder.Application.DTOs;

public record CreateDeckRequest(string Name, string? Description,string Format, string CardList, Guid UserId);
