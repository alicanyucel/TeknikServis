using FluentValidation;

namespace TeknikServis.Application.Features.DocumentLinks.CreateDcumentLink;

public sealed class CreateDocumentLinkCommandValidator : AbstractValidator<CreateDocumentLinkCommand>
{
    public CreateDocumentLinkCommandValidator()
    {
       
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Açıklama boş olamaz.")
            .MaximumLength(500).WithMessage("Açıklama en fazla 500 karakter olabilir.");
    }
}
