using FluentValidation;

namespace TeknikServis.Application.Features.ServiceActions.CreateServiceActions;

public sealed class CreateServiceActionCommandValidator : AbstractValidator<CreateServiceActionCommand>
{
    public CreateServiceActionCommandValidator()
    {
        RuleFor(x => x.ActionDate)
            .NotEmpty().WithMessage("Aksiyon tarihi boş olamaz.")
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Aksiyon tarihi gelecekte olamaz.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Açıklama boş olamaz.")
            .MaximumLength(500).WithMessage("Açıklama en fazla 500 karakter olabilir.");

        RuleFor(x => x.PersonId)
            .NotEmpty().WithMessage("PersonId boş olamaz.");

        RuleFor(x => x.StatusId)
            .NotEmpty().WithMessage("StatusId boş olamaz.");

        RuleFor(x => x.CustomerId)
            .NotEmpty().WithMessage("CustomerId boş olamaz.");
    }
}
