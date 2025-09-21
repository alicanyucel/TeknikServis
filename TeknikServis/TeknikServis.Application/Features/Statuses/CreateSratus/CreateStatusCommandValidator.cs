using FluentValidation;

namespace TeknikServis.Application.Features.Statuses.CreateSratus;

public sealed class CreateStatusCommandValidator : AbstractValidator<CreateStatusCommand>
{
    public CreateStatusCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Durum adı boş olamaz.")
            .MaximumLength(100).WithMessage("Durum adı en fazla 100 karakter olabilir.")
            .Matches(@"^[\p{L}\p{N}\s\-]+$").WithMessage("Durum adı yalnızca harf, rakam, boşluk ve tire içerebilir.");
    }
}
