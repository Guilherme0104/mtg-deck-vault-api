using FluentValidation;
using DeckBuilder.Application.DTOs;

namespace DeckBuilder.Application.Validators;

public class CreateReviewRequestValidator : AbstractValidator<CreateReviewRequest>
{
    public CreateReviewRequestValidator()
    {
        RuleFor(x => x.Rating)
            .InclusiveBetween(1, 5).WithMessage("A nota deve estar entre 1 e 5.");

        RuleFor(x => x.Comment)
            .NotEmpty().WithMessage("O comentario nao pode estar vazio.")
            .MaximumLength(500).WithMessage("O comentario nao pode exceder 500 caracteres.");

        RuleFor(x => x.DeckId)
            .NotEmpty().WithMessage("O ID do deck e obrigatorio.");

        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("O ID do usuario e obrigatorio.");
    }
}