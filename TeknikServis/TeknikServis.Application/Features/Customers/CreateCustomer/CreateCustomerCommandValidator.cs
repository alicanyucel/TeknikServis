using FluentValidation;

namespace TeknikServis.Application.Features.Customers.CreateCustomer;
public sealed class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("İsim boş olamaz.")
            .MaximumLength(100).WithMessage("İsim en fazla 100 karakter olabilir.");

        RuleFor(x => x.Surname)
            .NotEmpty().WithMessage("Soyisim boş olamaz.")
            .MaximumLength(100).WithMessage("Soyisim en fazla 100 karakter olabilir.");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Telefon numarası boş olamaz.")
            .Matches(@"^\+?[0-9\s\-]{7,15}$").WithMessage("Geçerli bir telefon numarası giriniz.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email boş olamaz.")
            .EmailAddress().WithMessage("Geçerli bir email adresi giriniz.");

        // Address checks (basic)
        RuleFor(x => x.Address)
            .NotNull().WithMessage("Adres bilgisi zorunludur.");

        // CustomerType: 1 = Bireysel, 2 = Kurumsal
        RuleFor(x => x.CustomerType)
            .Must(v => v == 1 || v == 2)
            .WithMessage("Geçersiz müşteri tipi. (1 = Bireysel, 2 = Kurumsal)");
    }
}