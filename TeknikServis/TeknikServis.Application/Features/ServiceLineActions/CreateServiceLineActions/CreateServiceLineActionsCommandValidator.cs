using FluentValidation;

namespace TeknikServis.Application.Features.ServiceLineActions.CreateServiceLineActions;

public sealed class CreateServiceLineActionCommandValidator : AbstractValidator<CreateServiceLineActionsCommand>
{
    public CreateServiceLineActionCommandValidator()
    {
        RuleFor(x => x.ServiceActionId).NotEmpty();
        RuleFor(x => x.PersonId).NotEmpty();
        RuleFor(x => x.ProductId).NotEmpty();
        RuleFor(x => x.CustomerId).NotEmpty();
        RuleFor(x => x.StatusId).NotEmpty();
        RuleFor(x => x.Description).NotEmpty().WithMessage("Açıklama boş olamaz.").MaximumLength(500);
    }
}
