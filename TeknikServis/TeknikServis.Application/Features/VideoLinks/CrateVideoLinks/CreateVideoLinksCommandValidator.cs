using FluentValidation;

namespace TeknikServis.Application.Features.VideoLinks.CrateVideoLinks;

public sealed class CreateVideoLinkCommandValidator : AbstractValidator<CreateVideoLinkCommand>
{
    public CreateVideoLinkCommandValidator()
    {
      
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Açıklama boş olamaz.")
            .MaximumLength(500).WithMessage("Açıklama en fazla 500 karakter olabilir.");

        RuleFor(x => x.ServiceActionId)
            .NotEmpty().WithMessage("ServiceActionId boş olamaz.");
    }
}
