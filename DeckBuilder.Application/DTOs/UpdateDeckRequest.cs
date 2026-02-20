namespace DeckBuilder.Application.DTOs;

public record UpdateDeckRequest(string Name, string? Description, string Format, string CardList);