using FluentValidation;
using DeckBuilder.Application.DTOs;
using System.Collections.Generic;

namespace DeckBuilder.Application.Validators;

public class CreateDeckRequestValidator : AbstractValidator<CreateDeckRequest>
{
    public CreateDeckRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("O nome do deck é obrigatório.")
            .MaximumLength(100).WithMessage("O nome do deck não pode exceder 100 caracteres.");

        RuleFor(x => x.Format)
            .NotEmpty().WithMessage("O formato do deck (ex: Commander, Standard) é obrigatório.");

        RuleFor(x => x.CardList)
            .Must(list => string.IsNullOrEmpty(list) || (list.Trim().StartsWith("[") && list.Trim().EndsWith("]")))
            .WithMessage("A lista de cartas deve estar em formato JSON válido ou vazia.");

        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("O ID do usuário é obrigatório.");
    }
}
