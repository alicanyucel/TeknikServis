using FluentValidation;

namespace TeknikServis.Application.Features.DocumentLinks.CreateDcumentLink;

public sealed class CreateDocumentLinkCommandValidator : AbstractValidator<CreateDocumentLinkCommand>
{
    public CreateDocumentLinkCommandValidator()
    {
        RuleFor(x => x.Url)
            .NotEmpty().WithMessage("URL boş olamaz.")
            .Must(url => Uri.IsWellFormedUriString(url, UriKind.Absolute))
            .WithMessage("Geçerli bir URL giriniz.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Açıklama boş olamaz.")
            .MaximumLength(500).WithMessage("Açıklama en fazla 500 karakter olabilir.");

        RuleFor(x => x.ServiceActionId)
            .NotEmpty().WithMessage("ServiceActionId boş olamaz.");
    }
}
