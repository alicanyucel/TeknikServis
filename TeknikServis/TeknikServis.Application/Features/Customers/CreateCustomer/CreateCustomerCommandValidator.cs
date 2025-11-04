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
            .NotEmpty().WithMessage("Telefon numarası boş olamaz.+90...........")
            .Matches(@"^\+?[0-9\s\-]{7,15}$").WithMessage("Geçerli bir telefon numarası giriniz.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email boş olamaz.")
            .EmailAddress().WithMessage("Geçerli bir email adresi giriniz.");

        RuleFor(x => x.Address)
            .NotNull().WithMessage("Adres bilgisi zorunludur.");

        RuleFor(x => x.CustomerType)
            .Must(v => v == 1 || v == 2)
            .WithMessage("Geçersiz müşteri tipi. (1 = Bireysel, 2 = Kurumsal)");
        When(x => x.CustomerType == 1, () =>
        {
            RuleFor(x => x.TcNo)
                .NotEmpty().WithMessage("Bireysel müşteri için TC Kimlik No zorunludur.")
                .Matches(@"^\d{11}$").WithMessage("TC Kimlik No 11 haneli olmalıdır.");
        });
        When(x => x.CustomerType == 2, () =>
        {
            RuleFor(x => x.VkNo)
                .NotEmpty().WithMessage("Tüzel müşteri için Vergi No zorunludur.")
                .Matches(@"^\d{10}$").WithMessage("Vergi No 10 haneli olmalıdır.");
        });

        RuleFor(x => x.CountryId)
      .NotEmpty().WithMessage("Ülke seçimi zorunludur.")
      .GreaterThan(0).WithMessage("Geçersiz ülke.");

        RuleFor(x => x.ProvinceId)
            .NotEmpty().WithMessage("İl seçimi zorunludur.")
            .GreaterThan(0).WithMessage("Geçersiz il.");

        RuleFor(x => x.DistrictId)
            .NotEmpty().WithMessage("İlçe seçimi zorunludur.")
            .GreaterThan(0).WithMessage("Geçersiz ilçe.");


    }
}
