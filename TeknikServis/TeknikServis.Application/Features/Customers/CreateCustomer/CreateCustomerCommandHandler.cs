using AutoMapper;
using GenericRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TeknikServis.Application.Features.Customers.CreateCustomer;
using TeknikServis.Domain.Entities;
using TeknikServis.Domain.Repositories;
using TS.Result;

namespace TeknikServis.Application.Features.Customers.CreateCustomers;

internal sealed class CreateCustomerComamndHandler(
    ICustomerRepository customerRepository,
    IProvinceRepository provinceRepository,
    IDistrictRepository districtRepository,
    ICountryRepository countryRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<CreateCustomerCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        // Country kontrol
        var countryExists = await countryRepository.GetAll()
            .AnyAsync(c => c.Id == request.CountryId, cancellationToken);
        if (!countryExists)
            return Result<string>.Failure("Geçersiz CountryId. Ülke bulunamadı.");

/*  PROVINCY-CUSTOMER ilişkisinin nasıl kurulacağına karar verilmeli. Şu anki kurguda PROVINCY ile COUNTRY arasında 1-n ilişki var. Birden fazla COUNTRY bir PROVINCY ile ilişkilendirilebiliyor. Ancak sistemde bir COUNTRY'e ait PROVINCY ve DİSTRICT seçildiğinde, farklı bir COUNTRY seçilmesi durumunda, ilk eklenen PROVINCY ve DISTRICT gelecek şekilde bir bağlılık kurulmuş oluyor. Bu durumun önüne geçmek için aşağıdaki kod blokları oluşturuldu. Comment out edilen kısım ise alternatif bir yaklaşımdır. Eğer farklı bir yaklaşım istenirse ilgili kod bloğu kullanılabilir. */

        // Province kontrol
        //var province = await provinceRepository.GetByExpressionWithTrackingAsync(p => p.Id == request.ProvinceId, cancellationToken);
        //if (province is null || province.CountryId != request.CountryId)
        //    return Result<string>.Failure("Geçersiz ProvinceId. İl bulunamadı veya ülke ile uyuşmuyor.");

        var province = await provinceRepository.GetAll().FirstOrDefaultAsync(p => p.Id == request.ProvinceId, cancellationToken);
        if (province is null || province.CountryId != request.CountryId)
            return Result<string>.Failure("Geçersiz ProvinceId. İl bulunamadı veya ülke ile uyuşmuyor.");

        // District kontrol
        //var district = await districtRepository.GetByExpressionWithTrackingAsync(d => d.Id == request.DistrictId, cancellationToken);
        //if (district is null || district.ProvinceId != request.ProvinceId)
        //    return Result<string>.Failure("Geçersiz DistrictId. İlçe bulunamadı veya il ile uyuşmuyor.");

        var district = await districtRepository.GetAll().FirstOrDefaultAsync(d => d.Id == request.DistrictId, cancellationToken);
        if (district is null || district.ProvinceId != request.ProvinceId)
            return Result<string>.Failure("Geçersiz DistrictId. İlçe bulunamadı veya il ile uyuşmuyor.");

        Customer customer = mapper.Map<Customer>(request);
        await customerRepository.AddAsync(customer, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return "Müşteri kaydı yapıldı";
    }
}