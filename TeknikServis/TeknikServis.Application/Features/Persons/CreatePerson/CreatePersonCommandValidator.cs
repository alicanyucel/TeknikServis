using FluentValidation;

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

        RuleFor(x => x.ExpertiseArea)
            .IsInEnum().WithMessage("Geçersiz uzmanlık alanı.");
    }
}
