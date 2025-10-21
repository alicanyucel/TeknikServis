using FluentValidation;
using TeknikServis.Domain.Enums;

namespace TeknikServis.Application.Features.Persons.CreatePerson;

public sealed class CreatePersonCommandValidator : AbstractValidator<CreatePersonCommand>
{
    public CreatePersonCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Ad boş olamaz.")
            .MaximumLength(100);

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Soyad boş olamaz.")
            .MaximumLength(100);

        // Validate ExpertiseArea value against defined SmartEnum values
        RuleFor(x => x.ExpertiseArea)
            .Must(v => ExpertiseArea.List.Any(e => e.Value == v))
            .WithMessage("Geçersiz uzmanlık alanı.");
    }
}
