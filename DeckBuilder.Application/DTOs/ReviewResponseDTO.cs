namespace DeckBuilder.Application.DTOs;


// Alterado de 'record' para 'class' para garantir compatibilidade com o AutoMapper.
// O AutoMapper exige um construtor sem parâmetros (parameterless constructor) 
// para instanciar o objeto antes de mapear as propriedades.
public class ReviewResponseDTO
{
    public int Id { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public string UserName { get; set; } = string.Empty;
}