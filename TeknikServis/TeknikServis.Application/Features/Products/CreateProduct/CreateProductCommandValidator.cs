using FluentValidation;

namespace TeknikServis.Application.Features.Products.CreateProduct;

public sealed class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Brand)
            .NotEmpty().WithMessage("Marka boş olamaz.")
            .MaximumLength(100);

        RuleFor(x => x.Model)
            .NotEmpty().WithMessage("Model boş olamaz.")
            .MaximumLength(100);

        RuleFor(x => x.SerialNumber)
            .NotEmpty().WithMessage("Seri numarası boş olamaz.")
            .MaximumLength(100);

        RuleFor(x => x.Description)
            .MaximumLength(500);

        RuleFor(x => x.CustomerId)
            .NotEmpty().WithMessage("CustomerId boş olamaz.");

        RuleFor(x => x.ProductType)
            .IsInEnum().WithMessage("Geçersiz ürün tipi.");
    }
}
