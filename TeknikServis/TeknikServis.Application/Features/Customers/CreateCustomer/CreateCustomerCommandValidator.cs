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

        RuleFor(x => x.AddressLine)
            .NotEmpty().WithMessage("Adres bilgisi boş olamaz.")
            .MaximumLength(250).WithMessage("Adres en fazla 250 karakter olabilir.");

        RuleFor(x => x.ZipCode)
            .NotEmpty().WithMessage("Posta kodu boş olamaz.")
            .MaximumLength(20).WithMessage("Posta kodu en fazla 20 karakter olabilir.");

        RuleFor(x => x.Country)
            .NotEmpty().WithMessage("Ülke bilgisi boş olamaz.")
            .MaximumLength(100).WithMessage("Ülke en fazla 100 karakter olabilir.");

        RuleFor(x => x.NeighborhoodId)
            .NotEmpty().WithMessage("Mahalle seçimi zorunludur.");

        
    }
}