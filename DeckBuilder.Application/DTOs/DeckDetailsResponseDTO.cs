namespace DeckBuilder.Application.DTOs;

public record DeckDetailsResponseDTO(
    int Id,
    string Name,
    string Description,
    string Format,
    string CardList,
    double AverageRating,
    int ReviewsCount
    );