using FluentValidation;
using TeknikServis.Domain.Enums;

namespace TeknikServis.Application.Features.Customers.CreateCustomers;

public sealed class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("İsim alanı boş bırakılamaz.")
            .MaximumLength(100).WithMessage("İsim en fazla 100 karakter olabilir.");

        RuleFor(x => x.Surname)
            .NotEmpty().WithMessage("Soyisim alanı boş bırakılamaz.")
            .MaximumLength(100).WithMessage("Soyisim en fazla 100 karakter olabilir.");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Telefon numarası boş bırakılamaz.")
            .Matches(@"^\+?[0-9]{10,15}$")
            .WithMessage("Telefon numarası 10 ile 15 hane arasında olmalı ve isteğe bağlı olarak '+' ile başlayabilir.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("E-posta adresi boş bırakılamaz.")
            .EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.");

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Adres alanı boş bırakılamaz.")
            .MaximumLength(250).WithMessage("Adres en fazla 250 karakter olabilir.");

        RuleFor(x => x.City)
            .NotEmpty().WithMessage("Şehir alanı boş bırakılamaz.");

        RuleFor(x => x.Country)
            .NotEmpty().WithMessage("Ülke alanı boş bırakılamaz.");

        RuleFor(x => x.ZipCode)
            .NotEmpty().WithMessage("Posta kodu boş bırakılamaz.")
            .Matches(@"^\d{4,10}$").WithMessage("Posta kodu yalnızca sayılardan oluşmalı ve 4 ile 10 hane arasında olmalıdır.");

        RuleFor(x => x.District)
            .NotEmpty().WithMessage("İlçe alanı boş bırakılamaz.");

        RuleFor(x => x.Neighborhood)
            .NotEmpty().WithMessage("Mahalle alanı boş bırakılamaz.");

        RuleFor(x => x.CustomerValue)
            .Must(value => value == 1 || value == 2)
            .WithMessage("Geçersiz müşteri tipi. (1 = Bireysel, 2 = Kurumsal)");
    }
}
